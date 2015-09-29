using System;
using System.ServiceModel;

namespace GoldMine.MainServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(GoldMineService)))
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
        }
    }
}