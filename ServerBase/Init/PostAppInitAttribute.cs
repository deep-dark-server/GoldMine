using System;

namespace GoldMine.ServerBase.Init
{
    /// <summary>
    /// Attribute to mark classes implementing static void PostAppInit() - if method not specified
    /// or specified method name
    /// to perform some post init process after program init
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PostAppInitAttribute : Attribute
    {
        public string MethodName { get; }

        /// <summary>
        /// MethodName of class with lower order value is invoked first(higher priority)
        /// </summary>
        public int ProcessOrder { get; set; } = 0;

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