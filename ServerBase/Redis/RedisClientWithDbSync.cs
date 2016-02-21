using GoldMine.DataModel;
using GoldMine.ServerBase.Redis.StoreKey;
using System.Threading.Tasks;

namespace GoldMine.ServerBase.Redis
{
    public abstract class RedisClientWithDbSync : RedisClient
    {
        protected RedisClientWithDbSync(string hostAndPort)
            : base(hostAndPort)
        {
        }

        protected abstract bool DbSet<TValue>(TValue value)
            where TValue : IRedisStorable, new();

        protected abstract TValue DbGet<TKey, TValue>(RedisStoreKey<TKey> key);

        protected abstract Task<bool> DbSetAsync<TValue>(TValue value)
            where TValue : IRedisStorable, new();

        protected abstract Task<TValue> DbGetAsync<TKey, TValue>(RedisStoreKey<TKey> key);

        public void Set<TKey, TValue>(RedisStoreKey<TKey> key, TValue value)
            where TValue : IRedisStorable, new()
        {
            if (DbSet(value) && IsConnected)
                RedisDb.StringSet(key, value.ToRedisValue());
        }

        public async Task<bool> SetAsync<TKey, TValue>(RedisStoreKey<TKey> key, TValue value)
            where TValue : IRedisStorable, new()
        {
            if (await DbSetAsync(value))
                return !IsConnected || await RedisDb.StringSetAsync(key, value.ToRedisValue());

            return false;
        }

        public TValue Get<TKey, TValue>(RedisStoreKey<TKey> key)
            where TValue : IRedisStorable, new()
        {
            if (IsConnected)
            {
                var valueFromRedis = RedisDb.StringGet(key);
                if (!valueFromRedis.IsNull)
                {
                    var ret = new TValue();
                    ret.LoadFrom(valueFromRedis);
                    return ret;
                }
            }

            var valueFromDb = DbGet<TKey, TValue>(key);
            if (valueFromDb == null)
                return default(TValue);

            if (!IsConnected)
                return valueFromDb;

            var valueForRedis = valueFromDb.ToRedisValue();
            RedisDb.StringSet(key, valueForRedis);
            return valueFromDb;
        }

        public async Task<TValue> GetAsync<TKey, TValue>(RedisStoreKey<TKey> key)
            where TValue : IRedisStorable, new()
        {
            if (IsConnected)
            {
                var valueFromRedis = await RedisDb.StringGetAsync(key);
                if (!valueFromRedis.IsNull)
                {
                    var ret = new TValue();
                    ret.LoadFrom(valueFromRedis);
                    return ret;
                }
            }

            var valueFromDb = await DbGetAsync<TKey, TValue>(key);
            if (valueFromDb == null)
                return default(TValue);

            if (!IsConnected)
                return valueFromDb;

            var valueForRedis = valueFromDb.ToRedisValue();
#pragma warning disable CS4014
            RedisDb.StringSetAsync(key, valueForRedis);
#pragma warning restore CS4014
            return valueFromDb;
        }
    }
}