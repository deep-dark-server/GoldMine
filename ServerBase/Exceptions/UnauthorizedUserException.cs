using System.Net;

namespace GoldMine.ServerBase.Exceptions
{
    public class UnauthorizedUserException : CustomException
    {
        public UnauthorizedUserException(string message)
            : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}