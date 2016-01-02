using System;

namespace GoldMine.DataModel.Attribute
{
    /// <summary>
    /// Attribute to mark data model having this attribute uses string type as (hash)key
    /// implying no key type conversion is needed when storing the model into redis
    /// GetKeyFromStringAttribute will be ignored when this attribute is attached
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class UseStringKeyAttribute : System.Attribute
    {
    }
}