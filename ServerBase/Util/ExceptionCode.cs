using System.Net;
using GoldMine.ServerBase.Exceptions;
using System.Runtime.InteropServices;
using System;

namespace GoldMine.ServerBase.Util
{
    public class ExceptionCode
    {
        private const int DefaultErrorCode = (int)HttpStatusCode.InternalServerError;

        public int httpStatusCode { get; private set; }
        public int errorCode { get; private set; }

        public static ExceptionCode GetFromException(System.Exception ex)
        {
            ExceptionCode code = new ExceptionCode()
            {
                httpStatusCode = DefaultErrorCode,
                errorCode = DefaultErrorCode
            };

            if (ex is CustomException)
                code.httpStatusCode = CustomException.StatusCode;

            if (ex is ExternalException)
                code.errorCode = ((ExternalException)ex).ErrorCode;

            return code;
        }
    }
}