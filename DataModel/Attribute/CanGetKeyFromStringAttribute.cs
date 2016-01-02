using System;

namespace GoldMine.DataModel.Attribute
{
    /// <summary>
    /// Attribute to mark that class has
    /// public static KeyType GetKeyFromString(string)
    /// which will be invoked when performing DB Get from RedisClientWithDbSync
    /// to convert string key which is redis-compliant into its original form
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class CanGetKeyFromStringAttribute : System.Attribute
    {
        public string MethodName { get; }

        public CanGetKeyFromStringAttribute()
        {
            MethodName = "GetKeyFromString";
        }
    }
}