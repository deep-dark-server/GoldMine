using GoldMine.ServerBase.Settings;
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
            if (!DBConnect.Default.UseRedis)
                return;

            Redis = ConnectionMultiplexer.Connect(hostAndPort);
            UpdateConnection();

            Redis.ConnectionRestored += OnConnectionRestored;
            Redis.ConnectionFailed += OnConnectionFailed;
        }

        private bool _disposed;

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

        private void UpdateConnection()
        {
            IsConnected = Redis.IsConnected;
            RedisDb = IsConnected ? Redis.GetDatabase() : null;
        }

        private void OnConnectionRestored(object obj, ConnectionFailedEventArgs args)
        {
            // TODO LOGGING
            UpdateConnection();
        }

        private void OnConnectionFailed(object obj, ConnectionFailedEventArgs args)
        {
            // TODO LOGGING
            UpdateConnection();
        }
    }
}