using StackExchange.Redis;
using System.Threading.Tasks;

namespace GoldMine.ServerBase.Redis
{
    public abstract class RedisClientWithDbSync : RedisClient
    {
        protected RedisClientWithDbSync(string hostAndPort)
            : base(hostAndPort)
        {
        }

        protected abstract bool DbSet<TValue>(string key, TValue value)
            where TValue : IRedisStorable, new();

        protected abstract TValue DbGet<TValue>(string key);

        protected abstract Task<bool> DbSetAsync<TValue>(string key, TValue value)
            where TValue : IRedisStorable, new();

        protected abstract Task<TValue> DbGetAsync<TValue>(string key);

        public void Set<TValue>(string key, TValue value)
            where TValue : IRedisStorable, new()
        {
            if (DbSet(key, value))
                RedisDb.StringSet(key, value.ToRedisValue());
        }

        public async Task<bool> SetAsync<TValue>(string key, TValue value)
            where TValue : IRedisStorable, new()
        {
            if (await DbSetAsync(key, value))
                return await RedisDb.StringSetAsync(key, value.ToRedisValue());

            return false;
        }

        public TValue Get<TValue>(string key)
            where TValue : IRedisStorable, new()
        {
            var valueFromRedis = RedisDb.StringGet(key);
            if (!valueFromRedis.IsNull)
            {
                var ret = new TValue();
                ret.SetValueFromRedisValue(valueFromRedis);
                return ret;
            }

            var valueFromDb = DbGet<TValue>(key);
            if (valueFromDb == null)
                return default(TValue);

            var valueForRedis = valueFromDb.ToRedisValue();
            RedisDb.StringSet(key, valueForRedis);
            return valueFromDb;
        }

        public async Task<TValue> GetAsync<TValue>(string key)
            where TValue : IRedisStorable, new()
        {
            var valueFromRedis = await RedisDb.StringGetAsync(key);
            if (!valueFromRedis.IsNull)
            {
                var ret = new TValue();
                ret.SetValueFromRedisValue(valueFromRedis);
                return ret;
            }

            var valueFromDb = await DbGetAsync<TValue>(key);
            if (valueFromDb == null)
                return default(TValue);

            var valueForRedis = valueFromDb.ToRedisValue();

#pragma warning disable CS4014
            RedisDb.StringSetAsync(key, valueForRedis);
#pragma warning restore CS4014

            return valueFromDb;
        }
    }
}