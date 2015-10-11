using GoldMine.MainServer.ServiceInterface;
using GoldMine.DataModel.Request;
using System;

namespace GoldMine.MainServer
{
    public class GoldMineService : IConnectionTest, IRegister
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public void Register(RequestRegister request)
        {
            // TODO implement this
            Console.WriteLine("Entered Register with Params");
			Console.WriteLine(request.userId);
			Console.WriteLine(request.protocol);
            Console.WriteLine("End Register");
        }
    }
}