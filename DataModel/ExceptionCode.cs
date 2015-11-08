using System;
using System.Runtime.InteropServices;

namespace GoldMine.DataModel
{
    public static class ExceptionCode
    {
        private const int DefaultErrorCode = 500;

        public static int GetExceptionCode(this Exception ex)
        {
            if (ex is ExternalException)
                return (ex as ExternalException).ErrorCode;

            return DefaultErrorCode;
        }
    }
}