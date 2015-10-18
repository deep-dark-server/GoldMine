using Amazon.DynamoDBv2;
using GoldMine.DataModel;
using GoldMine.MainServer;
using System;

namespace ServerBase
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