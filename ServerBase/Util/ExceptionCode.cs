using System.Runtime.InteropServices;

namespace GoldMine.ServerBase.Util
{
    public static class ExceptionCode
    {
        private const int DefaultErrorCode = 500;

        public static int GetExceptionCode(this System.Exception ex)
        {
            if (ex is ExternalException)
                return (ex as ExternalException).ErrorCode;

            return DefaultErrorCode;
        }
    }
}