using GoldMine.ServerBase.Exceptions;
using GoldMine.ServerBase.Init;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GoldMine.ServerBase.Util
{
    [PostAppInit]
    public static class LogException
    {
        /// <summary>
        /// System Exception(not custom exceptions) will be handled as Level.Fatal when in this set
        /// else, Level.Error will be used for logging
        /// </summary>
        private static readonly HashSet<Type> FatalSystemExceptions = new HashSet<Type>();

        private static readonly Type CustomExceptionType = typeof(CustomException);
        private static readonly Type LoggedCustomExceptionType = typeof(LoggedCustomException);

        public static void PostAppInit()
        {
            Type[] fatalErrors = { typeof(OutOfMemoryException) };

            // TODO load fatal error handled types from app.config
            foreach (var errorType in fatalErrors)
                FatalSystemExceptions.Add(errorType);
        }

        /// <summary>
        /// Log exception using suitable logger. Exception which is CustomException but not LoggedCustomException
        /// will be ignored
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(this Exception ex)
        {
            WriteLog(ex, false);
        }

        public static void WriteLogWithStackTrace(this Exception ex)
        {
            WriteLog(ex, true);
        }

        private static void WriteLog(Exception ex, bool logStackTrace)
        {
            Level errorLevel = null;

            var exceptionType = ex.GetType();
            if (exceptionType.IsSubclassOf(CustomExceptionType))
            {
                if (exceptionType.IsSubclassOf(LoggedCustomExceptionType))
                {
                    LoggedCustomException logEx = (LoggedCustomException)ex;
                    errorLevel = logEx.ErrorLevel;
                }
                // DO NOTHING for not-logged custom exceptions
            }
            else
            {
                errorLevel = FatalSystemExceptions.Contains(exceptionType) ? Level.Fatal : Level.Error;
            }

            Debug.Assert(errorLevel != null, "errorLevel != null");
            var logger = LogManager.GetLogger(errorLevel.Name).Logger;
            logger.Log(logger.GetType(), errorLevel, ex.Message, logStackTrace ? ex : null);
        }
    }
}