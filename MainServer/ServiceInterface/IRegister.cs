using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer.ServiceInterface
{
    [ServiceContract]
    public interface IRegister
    {
        [WebGet(UriTemplate = "register",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Response Register(RequestRegister request);
    }
}