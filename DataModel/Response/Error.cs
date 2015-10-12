using System.Runtime.Serialization;

namespace GoldMine.DataModel.Response
{
    [DataContract]
    public enum ErrorCode : short
    {
        [EnumMember]
        UndefinedError = 500,
    }

    [DataContract]
    public class ResponseError : Response
    {
        public ResponseError(ErrorCode error)
        {
            this.error = error;
        }

        [DataMember]
        public ErrorCode error;
    }
}
