using Amazon.DynamoDBv2.DataModel;
using System;
using System.Threading.Tasks;
using GoldMine.ServerBase.Redis.StoreKey;

namespace GoldMine.ServerBase.Redis
{
    public class RedisClientWithDynamoDbSync : RedisClientWithDbSync
    {
        public RedisClientWithDynamoDbSync(string hostAndPort)
            : base(hostAndPort)
        {
        }

        protected override bool DbSet<TValue>(TValue value)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                dynamoCtx.Save(value);
            }

            return true;
        }

        protected override TValue DbGet<TKey, TValue>(RedisStoreKey<TKey> key)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                return dynamoCtx.Load<TValue>(key.Data);

            }
        }

        protected override async Task<bool> DbSetAsync<TValue>(TValue value)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                await dynamoCtx.SaveAsync(value);
            }
            return true;
        }

        protected override async Task<TValue> DbGetAsync<TKey, TValue>(RedisStoreKey<TKey> key)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                return await dynamoCtx.LoadAsync<TValue>(key.Data);
            }
        }
    }
}