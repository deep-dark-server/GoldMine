using System.Net;

namespace GoldMine.DataModel.Response
{
    public class ResponseError : Response
    {
        public ResponseError()
        {
            this.error = (int)HttpStatusCode.InternalServerError;
        }

        public ResponseError(int error)
        {
            this.error = error;
        }

        public ResponseError(HttpStatusCode code)
        {
            this.error = (int)code;
        }

        public int error;
    }
}