using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.Common
{
    public static class LoggerExtensions
    {
        public static void DebugEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Debug);
        }
        public static void InfoEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Info);
        }
        public static void ErrorEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Error);
        }
        //public static void ErrorEx(this ILogger logger,Exception data,string description)
        //{
        //    logger.LogEx(description, data,  LogLevel.Error);
        //}
        public static void FatalEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Fatal);
        }
        public static void WarnEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Warn);
        }
        public static void TraceEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Trace);
        }

        private static bool IsPrimitiveType<T>() => IsPrimitiveType(typeof(T));

        private static bool IsPrimitiveType(Type type)
        {
            if (type == typeof(int)) return true;
            if (type == typeof(int?)) return true;

            if (type == typeof(short)) return true;
            if (type == typeof(short?)) return true;

            if (type == typeof(long)) return true;
            if (type == typeof(long?)) return true;


            if (type == typeof(uint)) return true;
            if (type == typeof(uint?)) return true;

            if (type == typeof(byte)) return true;
            if (type == typeof(byte?)) return true;

            if (type == typeof(decimal)) return true;
            if (type == typeof(decimal?)) return true;

            if (type == typeof(DateTime)) return true;
            if (type == typeof(DateTime?)) return true;

            if (type == typeof(Guid)) return true;
            if (type == typeof(Guid?)) return true;

            if (type == typeof(string)) return true;

            return false;
        }


        public static void LogEx<T>(this ILogger logger, string description, T data, LogLevel level = null)
        {
            logger = logger ?? LogManager.GetCurrentClassLogger();
            if (logger == null) return;

            string ToStringEx(object x)
            {
                if (x == null) return string.Empty;
                var type = x.GetType();
                if (IsPrimitiveType(type))
                {
                    return x + string.Empty;
                }

                return JsonConvert.SerializeObject(x);
            }

            if (data is Exception exception)
            {
                logger.Error(exception, description);
                return;
            }
            try
            {
                if (level == null) level = LogLevel.Debug;
                var result = NLog.Fluent.LoggerExtensions
                    .Info(logger)
                    .Level(level);

                var properties = typeof(T).GetProperties();
                if (IsPrimitiveType<T>())
                {
                    result = result.Property(typeof(T).Name + ".Value", ToStringEx(data));
                }
                else
                {
                    foreach (var property in properties)
                    {
                        try
                        {
                            var propertyValue = property.GetValue(data);
                            result = result.Property(property.Name, ToStringEx(propertyValue));
                        }
                        catch (Exception e)
                        {
                            result = result.Property(property.Name, e.Message);
                        }
                    }
                }

                result = result.Property("Description", description)
                    .LoggerName("LogEx");
                result.Write();
            }
            catch
            {
                //Buna deymeyin loq yaza bilmir cəhənnəmə yazsın
                //logger.Fatal(e, "LoggerExtensions.Exception.data:" + Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
        }
    }
}
