using System;
using System.Linq;
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
            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                        from type in assembly.GetTypes()
                        let postAppInitAttr = type.GetCustomAttribute<PostAppInitAttribute>()
                        where postAppInitAttr != null
                        orderby postAppInitAttr.ProcessOrder
                        select type;

            foreach (var type in types)
                type.GetMethod(type.GetCustomAttribute<PostAppInitAttribute>().MethodName,
                    BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
        }
    }
}