using System;
using StackExchange.Redis;

namespace GoldMine.ServerBase.Redis
{
    public class RedisClient : IDisposable
    {
        public IDatabase RedisDb { get; }
        private ConnectionMultiplexer Redis { get; }

        public RedisClient(string hostAndPort)
        {
            Redis = ConnectionMultiplexer.Connect(hostAndPort);
            RedisDb = Redis.GetDatabase();
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Redis.Close();
            Redis.Dispose();

            _disposed = true;
        }

        ~RedisClient()
        {
            Dispose(false);
        }
    }
}