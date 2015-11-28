using Amazon.DynamoDBv2.DataModel;
using GoldMine.DataModel.Request;

namespace GoldMine.DataModel
{
    [DynamoDBTable("user")]
    public class User
    {
        [DynamoDBHashKey]
        public short id { get; set; }

        public string server_host { get; set; }
        public ProtocolType protocol { get; set; }
    }
}