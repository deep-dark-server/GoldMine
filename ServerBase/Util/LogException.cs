using GoldMine.ServerBase.Exceptions;
using GoldMine.ServerBase.Init;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoldMine.ServerBase.Util
{
    [PostAppInit]
    public static class LogException
    {
        private class LogActionInfo
        {
            public Action<object> Log
            {
                get; private set;
            }

            public Action<object, Exception> LogWithException
            {
                get; private set;
            }

            public LogActionInfo(ILog logger)
            {
                this.logger = logger;

                var logParam = Expression.Parameter(typeof(object), "objToLog");
                var expParam = Expression.Parameter(typeof(Exception), "expParam");

                var firstChar = this.logger.Logger.Name[0];
                var methodName = Char.ToUpper(firstChar) + this.logger.Logger.Name.TrimStart(firstChar).ToLower();

                var logMethodInfo = typeof(ILog).GetMethod(methodName, new Type[] { typeof(object) });
                var logWithExceptionMethodInfo = typeof(ILog).GetMethod(methodName, new Type[] { typeof(object), typeof(Exception) });

                Log = Expression.Lambda<Action<object>>(
                    Expression.Call(
                        Expression.Constant(this.logger, typeof(ILog)),
                        logMethodInfo,
                        logParam),
                    logParam).Compile();

                LogWithException = Expression.Lambda<Action<object, Exception>>(
                    Expression.Call(
                        Expression.Constant(this.logger, typeof(ILog)),
                        logWithExceptionMethodInfo,
                        logParam, expParam),
                    logParam, expParam).Compile();
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
        private static readonly Dictionary<Level, LogActionInfo> LogActionDict = new Dictionary<Level, LogActionInfo>();

        private static readonly Type CustomExceptionType = typeof(CustomException);
        private static readonly Type LoggedCustomExceptionType = typeof(LoggedCustomException);

        public static void PostAppInit()
        {
            Level[] usedLogLevels = { Level.Info, Level.Warn, Level.Error, Level.Fatal };
            Type[] fatalErrors = { typeof(OutOfMemoryException) };

            foreach (var level in usedLogLevels)
                LogActionDict[level] = new LogActionInfo(LogManager.GetLogger(level.Name));

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
            LogActionInfo logAction = null;

            var exceptionType = ex.GetType();
            if (exceptionType.IsSubclassOf(CustomExceptionType))
            {
                if (exceptionType.IsSubclassOf(LoggedCustomExceptionType))
                {
                    LoggedCustomException logEx = (LoggedCustomException)ex;
                    logAction = LogActionDict[logEx.ErrorLevel];
                }
                // DO NOTHING for not-logged custom exceptions
            }
            else
            {
                if (FatalSystemExceptions.Contains(exceptionType))
                    logAction = LogActionDict[Level.Fatal];
                else
                    logAction = LogActionDict[Level.Error];
            }

            if (logAction == null)
                return;

            if (logStackTrace)
                logAction.LogWithException(ex.Message, ex);
            else
                logAction.Log(ex.Message);
        }
    }
}