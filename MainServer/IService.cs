using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer
{
    [ServiceContract]
    public interface IConnectionTest
    {
        [WebGet]
        string HelloWorld();
    }
}