using System.Net;

namespace GoldMine.ServerBase.Exceptions
{
    public class UnauthorizedUserException : LoggedCustomException
    {
        public UnauthorizedUserException(string message)
            : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}