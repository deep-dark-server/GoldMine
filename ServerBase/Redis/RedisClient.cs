using StackExchange.Redis;
using System;

namespace GoldMine.ServerBase.Redis
{
    /// <summary>
    /// use with singleton recommended
    /// </summary>
    public class RedisClient : IDisposable
    {
        public IDatabase RedisDb { get; private set; }
        protected bool IsConnected { get; private set; }
        private ConnectionMultiplexer Redis { get; }

        public RedisClient(string hostAndPort)
        {
            Redis = ConnectionMultiplexer.Connect(hostAndPort);
            IsConnected = Redis.IsConnected;
            RedisDb = IsConnected ? Redis.GetDatabase() : null;
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