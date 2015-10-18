using System.Runtime.Serialization;

namespace GoldMine.DataModel.Response
{
    [DataContract]
    public class ResponseResult<ValueT>
    {
        public ResponseResult(ValueT result)
        {
            this.result = result;
        }

        [DataMember]
        public ValueT result;
    }
}