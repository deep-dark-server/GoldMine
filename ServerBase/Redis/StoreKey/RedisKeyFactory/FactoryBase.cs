using StackExchange.Redis;

namespace GoldMine.ServerBase.Redis.StoreKey.RedisKeyFactory
{
    /// <summary>
    /// To make arbitrary type convertible to RedisKey, make a class deriving this class
    /// and implement the body. Thus RedisStoreKey will also support the type,
    /// letting the type usable as a key to store some value into Redis
    /// </summary>
    public abstract class FactoryBase
    {
        public abstract RedisKey Make<T>(T key);
    }
}