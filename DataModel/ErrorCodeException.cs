using System.Runtime.InteropServices;

namespace GoldMine.DataModel
{
    public class ErrorCodeException : ExternalException
    {
        public ErrorCodeException(string message, int errorCode)
            : base(message, errorCode)
        {
        }
    }
}