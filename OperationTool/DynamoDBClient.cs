using Amazon.DynamoDBv2;
using GoldMine.ServerBase.Init;
using GoldMine.ServerBase.Util;
using System;

namespace GoldMine.OperationTool
{
    [PostAppInit]
    public class DynamoDBClient : Singleton<AmazonDynamoDBClient>
    {
        public void PostAppInit()
        {
            Console.WriteLine("WarmUp: " + Instance.ToString());
            if (DBConnect.Default.UseLocalDB)
            {
                Instance.Config.ServiceURL = DBConnect.Default.LocalDBAddress;
            }
        }
    }
}