using Nancy;
using System.Runtime.InteropServices;

namespace GoldMine.MainServer.Exceptions
{
    public class UnauthorizedUserException : ExternalException
    {
        public UnauthorizedUserException(string message)
            : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}