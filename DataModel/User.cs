using Amazon.DynamoDBv2.DataModel;
using GoldMine.DataModel.Request;
using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GoldMine.DataModel
{
    [DynamoDBTable("user")]
    [Serializable]
    public class User : IRedisStorable
    {
        [DynamoDBHashKey]
        public short id { get; set; }

        public string server_host { get; set; }
        public ProtocolType protocol { get; set; }

        public void LoadFrom(RedisValue value)
        {
            byte[] bytes = value;
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                var deserializedObj = (User)formatter.Deserialize(ms);
                id = deserializedObj.id;
                server_host = deserializedObj.server_host;
                protocol = deserializedObj.protocol;
            }
        }

        public RedisValue ToRedisValue()
        {
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }
    }
}