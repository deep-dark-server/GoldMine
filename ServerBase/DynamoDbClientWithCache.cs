using GoldMine.ServerBase.Init;
using GoldMine.ServerBase.Redis;
using GoldMine.ServerBase.Util;
using System;

namespace GoldMine.ServerBase
{
    [PostAppInit]
    public class DynamoDbClientWithCache : Singleton<RedisClientWithDynamoDbSync>
    {
        public static void PostAppInit()
        {
            Console.WriteLine("WarmUp: " + Instance.ToString());
        }
    }
}