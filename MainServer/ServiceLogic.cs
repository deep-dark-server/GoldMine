using Amazon.DynamoDBv2.DataModel;
using GoldMine.DataModel;
using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using GoldMine.MainServer.Interface;
using GoldMine.ServerBase;
using GoldMine.ServerBase.Exceptions;
using System;
using System.Linq;
using GoldMine.ServerBase.Util;

namespace GoldMine.MainServer
{
    public partial class ServiceLogic : IConnectionTest, IService
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

            var userRegistered = DynamoDbClientWithCache.Instance.Get<User>(request.userId.ToString());
            if (userRegistered == null)
                throw new UnauthorizedUserException(RegisterException.GetUnauthorizedMessage("ID Not Issued"));

            if (userRegistered.accesskey != request.AccessKey)
                throw new UnauthorizedAccessException(RegisterException.GetUnauthorizedMessage("Access Key Incorrect"));

            var user = new User()
            {
                id = request.userId,
                server_host = hostAddress,
                protocol = request.protocol
            };
            DynamoDbClientWithCache.Instance.Set(user.id.ToString(), user);

            return new ResponseResult<bool>(true);
        }

        public ResponseResult<decimal> Issue(RequestIssue request)
        {
            var problem = PrimeNumberTable.PickN(3).Select(pn => (decimal) pn).Aggregate((lhs, rhs) => lhs*rhs);
            return new ResponseResult<decimal>(problem);
        }

        public void OnException(Exception ex)
        {
            ex.WriteLog();
        }
    }
}