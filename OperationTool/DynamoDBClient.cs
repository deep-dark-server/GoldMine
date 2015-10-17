using Amazon.DynamoDBv2;
using GoldMine.OperationTool;
using ServerBase;

namespace GoldMine.OperationTool
{
    public class DynamoDBClient : Singleton<AmazonDynamoDBClient>
    {
        static DynamoDBClient()
        {
            if (DBConnect.Default.UseLocalDB)
            {
                DynamoDBClient.SetInitFunc(() =>
                {
                    var config = new AmazonDynamoDBConfig();
                    config.ServiceURL = DBConnect.Default.LocalDBAddress;
                    AmazonDynamoDBClient client = new AmazonDynamoDBClient(config);
                    return client;
                });
            }
        }
    }
}