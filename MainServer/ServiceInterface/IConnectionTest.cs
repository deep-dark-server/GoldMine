using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer.ServiceInterface
{
    [ServiceContract]
    public interface IConnectionTest
    {
        [WebGet]
        string HelloWorld();
    }
}