using System;
using System.Net;
using System.Runtime.Serialization;

namespace GoldMine.DataModel.Response
{
    [DataContract]
    public class ResponseError
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
        
        [DataMember]
        public int error;
    }
}
