using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using GoldMine.MainServer.ServiceInterface;
using System;

namespace GoldMine.MainServer
{
    public class GoldMineService : IConnectionTest, IRegister, IHandleError
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public Response Register(RequestRegister request)
        {
            try
            {
                // TODO implement validation, db write logic..
                Console.WriteLine("Entered Register with Params");
                Console.WriteLine(request.userId);
                Console.WriteLine(request.protocol);
                Console.WriteLine("End Register");

                return new ResponseResult<bool>(true);
            }
            catch (Exception e)
            {
                return RespondOnException(e);
            }
        }

        public ResponseError RespondOnError(ErrorCode code)
        {
            return new ResponseError(code);
        }

        public ResponseError RespondOnException(Exception e)
        {
            // TODO exception logging
            return new ResponseError(ErrorCode.UndefinedError);
        }
    }
}