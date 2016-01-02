using System;

namespace GoldMine.ServerBase.Init
{
    /// <summary>
    /// Attribute to mark classes implementing static void PostAppInit() - if method not specified
    /// or specified method name
    /// to perform some post init process after program init
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class PostAppInitAttribute : Attribute
    {
        public string MethodName { get; }

        public PostAppInitAttribute()
        {
            MethodName = "PostAppInit";
        }

        public PostAppInitAttribute(string methodName)
        {
            this.MethodName = methodName;
        }
    }
}