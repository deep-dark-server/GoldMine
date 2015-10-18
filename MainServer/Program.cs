using System;
using System.ServiceModel;
using System.Threading;

namespace GoldMine.MainServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(GoldMineWebService)))
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
        }
    }
}