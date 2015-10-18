using Amazon.DynamoDBv2;
using GoldMine.DataModel;
using ServerBase;
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