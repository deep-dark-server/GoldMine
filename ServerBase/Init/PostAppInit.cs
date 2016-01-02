using System;
using System.Reflection;

namespace GoldMine.ServerBase.Init
{
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