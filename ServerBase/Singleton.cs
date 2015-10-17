using System;

namespace ServerBase
{
    public abstract class Singleton<T>
    {
        private static Lazy<T> instance = null;

        public static bool SetInitFunc(Func<T> func)
        {
            if (instance == null)
            {
                instance = new Lazy<T>(func);
                return true;
            }
            else
                return false;
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new Lazy<T>();
                return instance.Value;
            }
        }
    }
}