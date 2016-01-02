using Amazon.DynamoDBv2.DataModel;
using GoldMine.ServerBase.Init;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoldMine.ServerBase.Redis
{
    [PostAppInit]
    public class RedisClientWithDynamoDbSync : RedisClientWithDbSync
    {
        public RedisClientWithDynamoDbSync(string hostAndPort)
            : base(hostAndPort)
        {
        }

        public static void PostAppInit()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes().Where(
                    type => type.GetCustomAttribute<DynamoDBTableAttribute>() != null))
                {
                    var members =
                        (type.GetMembers()
                            .Where(member => member.GetCustomAttribute<DynamoDBHashKeyAttribute>() != null)).ToList();
                    if (members.Count > 1)
                        throw new Exception(
                            $"Type {type.Name} has multiple DynamoDbHashKeyAttribute, which is not allowed");

                    var hashKeyMember = members.FirstOrDefault();
                    if (hashKeyMember == null) continue;

                    var keyType = hashKeyMember.GetType();
                    GetKeyFromString func;
                    if (KeyFromStrDict.TryGetValue(keyType, out func))
                        KeyFromStrDict.Add(keyType, func);
                }
            }
        }

        protected override bool DbSet<TValue>(string key, TValue value)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                dynamoCtx.Save(value);
            }

            return true;
        }

        protected override TValue DbGet<TValue>(string key)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                var valueType = typeof(TValue);
                GetKeyFromString func;
                if (KeyFromStrDict.TryGetValue(valueType, out func))
                    return dynamoCtx.Load<TValue>(func(key));

                throw new NotImplementedException(GetKeyForTypeFailMsg(valueType.Name));
            }
        }

        protected override async Task<bool> DbSetAsync<TValue>(string key, TValue value)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                await dynamoCtx.SaveAsync(value);
            }
            return true;
        }

        protected override async Task<TValue> DbGetAsync<TValue>(string key)
        {
            using (var dynamoCtx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                var valueType = typeof(TValue);
                GetKeyFromString func;
                if (KeyFromStrDict.TryGetValue(valueType, out func))
                    return await dynamoCtx.LoadAsync<TValue>(func(key));

                throw new NotImplementedException(GetKeyForTypeFailMsg(valueType.Name));
            }
        }

        private static string GetKeyForTypeFailMsg(string typeName)
        {
            return $"Cannot get key for type {typeName} with string. " +
                   "Consider implementing GetKeyFromString() or using primitive type key for data model.";
        }
    }
}