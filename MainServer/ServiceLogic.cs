using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using GoldMine.MainServer.Interface;
using System;
using System.Net;
using System.ServiceModel.Web;

namespace GoldMine.MainServer
{
    public class ServiceLogic : IConnectionTest, IService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public ResponseResult<bool> Register(RequestRegister request)
        {
            // TODO implement validation, db write logic..
            Console.WriteLine("Entered Register with Params");
            Console.WriteLine(request.userId);
            Console.WriteLine(request.protocol);
            Console.WriteLine("End Register");
            return new ResponseResult<bool>(true);
        }

        public T ProcessWebRequest<T>(Func<T> Process)
        {
            try
            {
                return Process();
            }
            catch (Exception ex)
            {
                HttpStatusCode code = HttpStatusCode.InternalServerError;
                Console.WriteLine(ex.ToString());
                throw new WebFaultException<ResponseError>(new ResponseError(code), code);
            }
        }
    }
}