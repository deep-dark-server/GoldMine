using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer.Interface
{
    public interface IService
    {
        ResponseResult<bool> Register(RequestRegister request);
    }
}