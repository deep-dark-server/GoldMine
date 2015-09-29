using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer.ServiceInterface
{
    [ServiceContract]
    public interface IRegister
    {
        [WebGet(UriTemplate = "register/{userId}/{type}")]
        void Register(string userId, string type);
    }
}