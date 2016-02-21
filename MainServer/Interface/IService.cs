using GoldMine.DataModel.Request;
using GoldMine.DataModel.Request.Impl;
using GoldMine.DataModel.Response;
using GoldMine.DataModel.Response.Impl;

namespace GoldMine.MainServer.Interface
{
    public interface IService
    {
        ResponseResult<bool> Register(RequestRegister request, string hostAddress);
        ResponseResult<decimal> Issue(RequestIssue request);
    }
}