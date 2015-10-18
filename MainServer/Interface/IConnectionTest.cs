using GoldMine.DataModel.Response;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoldMine.MainServer.Interface
{
    [ServiceContract]
    public interface IConnectionTest
    {
        [WebGet]
        [FaultContract(typeof(ResponseError))]
        string HelloWorld();
    }
}