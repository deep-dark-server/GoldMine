using log4net.Core;
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

        private static readonly Level DefaultErrorLevel = Level.Warn;

        /// <summary>
        /// Error Level(used for selecting logger)
        /// </summary>
        public Level ErrorLevel { get; private set; }

        public CustomException(string message, int errorCode)
            : base(message, errorCode)
        {
            ErrorLevel = DefaultErrorLevel;
        }

        public CustomException(string message, int errorCode, Level errorLevel)
            : this(message, errorCode)
        {
            ErrorLevel = errorLevel;
        }
    }
}