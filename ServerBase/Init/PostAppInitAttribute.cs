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
        private string methodName;

        public string MethodName
        {
            get { return methodName; }
        }

        public PostAppInitAttribute()
        {
            methodName = "PostAppInit";
        }

        public PostAppInitAttribute(string methodName)
        {
            this.methodName = methodName;
        }
    }
}