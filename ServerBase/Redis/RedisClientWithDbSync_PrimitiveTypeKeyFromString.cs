namespace GoldMine.ServerBase.Redis
{
    public abstract partial class RedisClientWithDbSync
    {
        private static void RegisterGetKeyFuncForPrimitiveTypes()
        {
            KeyFromStrDict.Add(typeof (short), keyStr => short.Parse(keyStr));
            KeyFromStrDict.Add(typeof (int), keyStr => int.Parse(keyStr));
            KeyFromStrDict.Add(typeof (long), keyStr => long.Parse(keyStr));
            KeyFromStrDict.Add(typeof (float), keyStr => float.Parse(keyStr));
            KeyFromStrDict.Add(typeof (double), keyStr => double.Parse(keyStr));
        }
    }
}