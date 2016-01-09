using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;

namespace GoldMine.MainServer.Interface
{
    public interface IService
    {
        ResponseResult<bool> Register(RequestRegister request, string hostAddress);
        ResponseResult<string> Issue(RequestIssue request);
    }
}