using System;
using System.Reflection;

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
            get
            {
                return methodName;
            }
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

    public static class PostAppInit
    {
        /// <summary>
        /// Invoke Post App Init methods implemented by classes with PostAppInitAttribute
        /// </summary>
        public static void Do()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    var postAppInitAttr = type.GetCustomAttribute<PostAppInitAttribute>();
                    if (postAppInitAttr != null)
                    {
                        type.GetMethod(postAppInitAttr.MethodName, 
                            BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
                    }
                }
            }
        }
    }
}