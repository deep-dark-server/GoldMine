using Amazon.DynamoDBv2.DataModel;
using GoldMine.DataModel;
using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using GoldMine.MainServer.Interface;
using GoldMine.ServerBase.Exceptions;
using System;

namespace GoldMine.MainServer
{
    public class ServiceLogic : IConnectionTest, IService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public ResponseResult<bool> Register(RequestRegister request, string hostAddress)
        {
            Console.WriteLine("Entered Register with Params");
            Console.WriteLine(request.userId);
            Console.WriteLine(request.protocol);
            Console.WriteLine(hostAddress);
            Console.WriteLine("End Register");

            using (var ctx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                User userRegistered = ctx.Load<User>(request.userId);
                if (userRegistered == null)
                    throw new UnauthorizedUserException("Register host fail: Unauthorized User(ID Not Issued)");

                User user = new User()
                {
                   id = request.userId,
                   server_host = hostAddress,
                   protocol = request.protocol
                };
                ctx.Save(user);
            }

            return new ResponseResult<bool>(true);
        }

        public void OnException(Exception ex)
        {

        }
    }
}