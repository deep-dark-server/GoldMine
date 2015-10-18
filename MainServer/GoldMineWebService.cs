using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using GoldMine.MainServer.Interface;

namespace GoldMine.MainServer
{
    public class GoldMineWebService : IConnectionTest, IService
    {
        private ServiceLogic logic = new ServiceLogic();

        public string HelloWorld()
        {
            return logic.ProcessWebRequest(() => logic.HelloWorld());
        }

        public ResponseResult<bool> Register(RequestRegister request)
        {
            return logic.ProcessWebRequest(() => logic.Register(request));
        }
    }
}