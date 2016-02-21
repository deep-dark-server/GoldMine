namespace GoldMine.DataModel.Response.Impl
{
    public class ResponseResult<ValueT> : Response
    {
        public ResponseResult(ValueT result)
        {
            this.result = result;
        }

        public ValueT result;
    }
}