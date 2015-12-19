using Amazon.DynamoDBv2;
using GoldMine.ServerBase.Init;
using GoldMine.ServerBase.Settings;
using GoldMine.ServerBase.Util;
using System;

namespace GoldMine.ServerBase
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