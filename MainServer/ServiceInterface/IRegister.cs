using GoldMine.DataModel.Request;
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
		void Register(RequestRegister request);
    }
}