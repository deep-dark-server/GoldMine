using System.Runtime.Serialization;

namespace GoldMine.DataModel.Response
{
    public class ResponseResult<ValueT>
    {
        public ResponseResult(ValueT result)
        {
            this.result = result;
        }

        public ValueT result;
    }
}