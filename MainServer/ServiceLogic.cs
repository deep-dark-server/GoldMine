using GoldMine.DataModel;
using GoldMine.DataModel.Request.Impl;
using GoldMine.DataModel.Response.Impl;
using GoldMine.MainServer.ExceptionMessage;
using GoldMine.MainServer.Interface;
using GoldMine.ServerBase;
using GoldMine.ServerBase.Exceptions;
using GoldMine.ServerBase.Util;
using System;
using System.Linq;

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

            var inst = DynamoDbClientWithCache.Instance;
            /*
            DynamoDbClientWithCache.Instance.FromKey(request.userId).Get<User>();
            */
            var userRegistered = inst.Get<User>(inst.Key(request.userId));
            if (userRegistered == null)
                throw new UnauthorizedUserException(RegisterExceptionMessage.ForUnauthorized("ID Not Issued"));

            if (userRegistered.accesskey != request.AccessKey)
                throw new UnauthorizedAccessException(RegisterExceptionMessage.ForUnauthorized("Access Key Incorrect"));

            var user = new User()
            {
                id = request.userId,
                server_host = hostAddress,
                protocol = request.protocol
            };
            DynamoDbClientWithCache.Instance.Set<short, User>(user.id, user);

            return new ResponseResult<bool>(true);
        }

        public ResponseResult<decimal> Issue(RequestIssue request)
        {
            var problem = PrimeNumberTable.PickN(3).Select(pn => (decimal)pn).Aggregate((lhs, rhs) => lhs * rhs);
            return new ResponseResult<decimal>(problem);
        }

        public void OnException(Exception ex)
        {
            ex.WriteLog();
        }
    }
}