using GoldMine.ServerBase.Exceptions;
using GoldMine.ServerBase.Init;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;

namespace GoldMine.ServerBase.Util
{
    [PostAppInit]
    public static class LogException
    {
        private class LogInterfaceMethodPair
        {
            public Action<object> Log
            {
                get; private set;
            }

            public Action<object, Exception> LogWithException
            {
                get; private set;
            }

            public LogInterfaceMethodPair(ILog logger)
            {
                this.logger = logger;
                // TODO compile Logging Method Invocation (.Info / .Error etc) with Expressions api
                // and save to Log/LogWithException
            }

            private ILog logger;
        }

        /// <summary>
        /// System Exception(not custom exceptions) will be handled as Level.Fatal when in this set
        /// else, Level.Error will be used for logging
        /// </summary>
        private static readonly HashSet<Type> FatalSystemExceptions = new HashSet<Type>();

        /// <summary>
        /// ErrorLevel to Logger Mapping
        /// </summary>
        private static readonly Dictionary<Level, LogInterfaceMethodPair> LoggerDictionary = new Dictionary<Level, LogInterfaceMethodPair>();

        private static readonly Type CustomExceptionType = typeof(CustomException);
        private static readonly Type LoggedCustomExceptionType = typeof(LoggedCustomException);

        public static void PostAppInit()
        {
            Level[] usedLogLevels = { Level.Info, Level.Warn, Level.Error, Level.Fatal };
            foreach (var level in usedLogLevels)
            {
                LoggerDictionary[level] = new LogInterfaceMethodPair(LogManager.GetLogger(level.Name));
            }
            // TODO load fatal error handled types from app.config
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
            LogInterfaceMethodPair mthdPair = null;

            var exceptionType = ex.GetType();
            if (exceptionType.IsSubclassOf(CustomExceptionType))
            {
                if (exceptionType.IsSubclassOf(LoggedCustomExceptionType))
                {
                    LoggedCustomException logEx = (LoggedCustomException)ex;
                    mthdPair = LoggerDictionary[logEx.ErrorLevel];
                }
                // DO NOTHING for not-logged custom exceptions
            }
            else
            {
                if (FatalSystemExceptions.Contains(exceptionType))
                    mthdPair = LoggerDictionary[Level.Fatal];
                else
                    mthdPair = LoggerDictionary[Level.Error];
            }

            if (mthdPair == null)
                return;

            if (logStackTrace)
                mthdPair.LogWithException(ex.Message, ex);
            else
                mthdPair.Log(ex.Message);
        }
    }
}