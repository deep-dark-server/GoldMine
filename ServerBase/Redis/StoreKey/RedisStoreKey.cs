using System.Dynamic;
using GoldMine.ServerBase.Redis.StoreKey.RedisKeyFactory;
using StackExchange.Redis;

namespace GoldMine.ServerBase.Redis.StoreKey
{
    public class RedisStoreKey<T> : IRedisKey
    {
        public T Data { get; }

        protected RedisStoreKey(T data)
        {
            Data = data;
        }

        public static implicit operator T(RedisStoreKey<T> storeKey)
        {
            return storeKey.Data;
        }

        public static implicit operator RedisKey(RedisStoreKey<T> storeKey)
        {
            return Factory.Make(storeKey.Data);
        }

        public static implicit operator RedisStoreKey<T>(T data)
        {
            return new RedisStoreKey<T>(data);
        }

        public RedisKey RedisKey => Factory.Make(Data);
    }
}