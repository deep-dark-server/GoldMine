using Amazon.DynamoDBv2.DataModel;

namespace GoldMine.DataModel
{
    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey]
        public int id { get; set; }
        public string server_host { get; set; }
        public byte protocol { get; set; }
    }
}