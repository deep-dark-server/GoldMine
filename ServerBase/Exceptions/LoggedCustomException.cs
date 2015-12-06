using log4net.Core;

namespace GoldMine.ServerBase.Exceptions
{
    public abstract class LoggedCustomException : CustomException
    {
        private static readonly Level DefaultErrorLevel = Level.Info;

        /// <summary>
        /// Error Level(used for selecting logger)
        /// </summary>
        public Level ErrorLevel { get; private set; }

        public LoggedCustomException(string message, int errorCode)
            : base(message, errorCode)
        {
            ErrorLevel = DefaultErrorLevel;
        }

        public LoggedCustomException(string message, int errorCode, Level errorLevel)
            : this(message, errorCode)
        {
            ErrorLevel = errorLevel;
        }
    }
}