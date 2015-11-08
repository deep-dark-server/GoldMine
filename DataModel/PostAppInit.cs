using System;
using System.Reflection;

namespace GoldMine.DataModel
{
    /// <summary>
    /// Attribute to mark classes implementing static void PostAppInit()
    /// to perform some post init process after program init
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class PostAppInitAttribute : Attribute
    {
        public PostAppInitAttribute()
        {
        }
    }

    public static class PostAppInit
    {
        public static void Do()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in Assembly.GetEntryAssembly().GetTypes())
                {
                    var postAppInitAttr = type.GetCustomAttribute<PostAppInitAttribute>();
                    if (postAppInitAttr != null)
                    {

                    }
                }
            }
        }
    }
}