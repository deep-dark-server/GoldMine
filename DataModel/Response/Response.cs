using System.Runtime.Serialization;

namespace GoldMine.DataModel.Response
{
    [DataContract]
    [KnownType(typeof(ResponseError))]
    [KnownType(typeof(ResponseResult<bool>))]
    public abstract class Response
    {
    }
}