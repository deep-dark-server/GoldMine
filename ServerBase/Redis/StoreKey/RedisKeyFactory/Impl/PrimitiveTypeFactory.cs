using StackExchange.Redis;

namespace GoldMine.ServerBase.Redis.StoreKey.RedisKeyFactory.Impl
{
    public class ByteKeyFactory : FactoryBase
    {
        public override RedisKey Make<T>(T key)
        {
            return (RedisKey)(key.ToString());
        }
    }

    public class ShortKeyFactory : FactoryBase
    {
        public override RedisKey Make<T>(T key)
        {
            return (RedisKey)(key.ToString());
        }
    }

    public class IntKeyFactory : FactoryBase
    {
        public override RedisKey Make<T>(T key)
        {
            return (RedisKey)(key.ToString());
        }
    }

    public class LongKeyFactory : FactoryBase
    {
        public override RedisKey Make<T>(T key)
        {
            return (RedisKey)(key.ToString());
        }
    }

    public class FloatKeyFactory : FactoryBase
    {
        public override RedisKey Make<T>(T key)
        {
            return (RedisKey)(key.ToString());
        }
    }

    public class DoubleKeyFactory : FactoryBase
    {
        public override RedisKey Make<T>(T key)
        {
            return (RedisKey)(key.ToString());
        }
    }

}