using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer.Interface
{
    [ServiceContract]
    public interface IService
    {
        [WebGet(UriTemplate = "register",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(ResponseError))]
        ResponseResult<bool> Register(RequestRegister request);
    }
}