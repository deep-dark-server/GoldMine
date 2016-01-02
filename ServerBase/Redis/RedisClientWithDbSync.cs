using GoldMine.DataModel;
using GoldMine.DataModel.Attribute;
using GoldMine.ServerBase.Init;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace GoldMine.ServerBase.Redis
{
    [PostAppInit]
    public abstract partial class RedisClientWithDbSync : RedisClient
    {
        protected delegate dynamic GetKeyFromString(string strKey);

        protected static Dictionary<Type, GetKeyFromString> KeyFromStrDict { get; set; }
        protected static HashSet<Type> TypesWithStringKeySet { get; set; }

        protected RedisClientWithDbSync(string hostAndPort)
            : base(hostAndPort)
        {
        }

        public static void PostAppInit()
        {
            TypesWithStringKeySet = new HashSet<Type>();
            KeyFromStrDict = new Dictionary<Type, GetKeyFromString>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    var useStringKeyAttr = type.GetCustomAttribute<UseStringKeyAttribute>();
                    if (useStringKeyAttr != null)
                    {
                        TypesWithStringKeySet.Add(type);
                        continue;
                    }

                    var getKeyFromStringAttr = type.GetCustomAttribute<CanGetKeyFromStringAttribute>();
                    if (getKeyFromStringAttr == null)
                        continue;

                    var keyArg = Expression.Parameter(typeof(string), "strKey");
                    var getKeyFunc = Expression.Lambda<GetKeyFromString>(
                        Expression.Call(
                            type.GetMethod(getKeyFromStringAttr.MethodName,
                                BindingFlags.Public | BindingFlags.Static), keyArg),
                        keyArg).Compile();
                    KeyFromStrDict.Add(type, getKeyFunc);
                }
            }
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
            if (DbSet(key, value) && IsConnected)
                RedisDb.StringSet(key, value.ToRedisValue());
        }

        public async Task<bool> SetAsync<TValue>(string key, TValue value)
            where TValue : IRedisStorable, new()
        {
            if (await DbSetAsync(key, value) && IsConnected)
                return await RedisDb.StringSetAsync(key, value.ToRedisValue());

            return false;
        }

        public TValue Get<TValue>(string key)
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

            var valueFromDb = DbGet<TValue>(key);
            if (valueFromDb == null)
                return default(TValue);

            if (!IsConnected)
                return valueFromDb;

            var valueForRedis = valueFromDb.ToRedisValue();
            RedisDb.StringSet(key, valueForRedis);
            return valueFromDb;
        }

        public async Task<TValue> GetAsync<TValue>(string key)
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

            var valueFromDb = await DbGetAsync<TValue>(key);
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