using GoldMine.DataModel;
using GoldMine.MainServer.Settings;
using Nancy.Hosting.Wcf;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PostAppInit.Do();

            using (WebServiceHost host =
                new WebServiceHost(
                    new NancyWcfGenericService(),
                    new Uri(HostSetting.Default.WebHostAddress)))
            {
                host.AddServiceEndpoint(
                    typeof(NancyWcfGenericService),
                    new WebHttpBinding(),
                    "");
                host.Open();
                Console.ReadLine();
                host.Close();
            }
        }
    }
}