using GoldMine.ServerBase.Redis.StoreKey.RedisKeyFactory.Impl;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace GoldMine.ServerBase.Redis.StoreKey.RedisKeyFactory
{
    public static class Factory
    {
        private static readonly Dictionary<Type, FactoryBase> KeyFactoryDict;

        static Factory()
        {
            // TODO apply PostAppInit

            KeyFactoryDict = new Dictionary<Type, FactoryBase> { { typeof(int), new IntKeyFactory() } };
            // TODO Auto factory registration
        }

        public static RedisKey Make<T>(T key)
        {
            return KeyFactoryDict[typeof(T)].Make(key);
        }
    }
}