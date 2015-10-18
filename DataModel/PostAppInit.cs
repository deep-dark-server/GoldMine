using System;

namespace GoldMine.DataModel
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class PostAppInitAttribute : Attribute
    {
        public PostAppInitAttribute()
        {
        }
    }
}