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

        public void Register(string userId, string type)
        {
            short uid = short.Parse(userId); 
            ProtocolType pType = (ProtocolType)(int.Parse(type));

            // TODO implement this
            Console.WriteLine("Entered Register with Params");
            Console.WriteLine(uid);
            Console.WriteLine(pType);
            Console.WriteLine("End Register");
        }
    }
}