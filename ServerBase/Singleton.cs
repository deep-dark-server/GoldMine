using System;

namespace ServerBase
{
    public class Singleton<T>
    {
        private static Lazy<T> instance = new Lazy<T>();

        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}