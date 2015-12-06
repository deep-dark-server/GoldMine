using System.Net;
using System.Runtime.InteropServices;

namespace GoldMine.ServerBase.Exceptions
{
    public abstract class CustomException : ExternalException
    {
        /// <summary>
        /// Custom Exceptions: Http header status code is marked as OK
        /// But need to see content to see what error occured
        /// </summary>
        public const int StatusCode = (int)HttpStatusCode.OK;

        public CustomException(string message, int errorCode)
            : base(message, errorCode)
        {
        }
    }
}