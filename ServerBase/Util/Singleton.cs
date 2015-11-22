using System;

namespace GoldMine.ServerBase.Util
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