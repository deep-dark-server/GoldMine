using GoldMine.DataModel.Response;

namespace GoldMine.MainServer
{
    public interface IHandleError
    {
        ResponseError RespondOnException(System.Exception e);
        ResponseError RespondOnError(ErrorCode code);
    }
}