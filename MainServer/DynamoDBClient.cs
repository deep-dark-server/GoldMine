using Amazon.DynamoDBv2;
using GoldMine.MainServer.Settings;
using GoldMine.ServerBase.Init;
using GoldMine.ServerBase.Util;
using System;

namespace GoldMine.MainServer
{
    [PostAppInit]
    public class DynamoDBClient : Singleton<AmazonDynamoDBClient>
    {
        public static void PostAppInit()
        {
            Console.WriteLine("WarmUp: " + Instance.ToString());
            if (DBConnect.Default.UseLocalDB)
            {
                Instance.Config.ServiceURL = DBConnect.Default.LocalDBAddress;
            }
        }
    }
}