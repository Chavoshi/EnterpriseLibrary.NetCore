using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.AspNetCore
{
    internal class AspNetLogger : ILogger
    {
        private readonly LoggerOptions _options;
        private readonly ICollection<string> _categories;
        public AspNetLogger(ICollection<string> categories, LoggerOptions options)
        {
            _categories = categories;
            _options = options;
        }

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;        

        public bool IsEnabled(LogLevel logLevel) => Logger.Writer != null && Logger.IsLoggingEnabled();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            TraceEventType eventType = ConvertLogLevel(logLevel);
            ICollection<string> categories = _categories.Where(x => Logger.Writer.TraceSources.ContainsKey(x)).ToList();
            if (categories.Count == 0)
            {
                return;
            }

            LogEntry entry = new LogEntry()
            {
                Categories = categories,
                EventId = eventId.Id,
                Message = formatter(state, exception),
                Severity = eventType
            };

            Logger.Write(entry);
        }

        private TraceEventType ConvertLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return TraceEventType.Critical;
                case LogLevel.Debug:
                    return TraceEventType.Verbose;
                case LogLevel.Error:
                    return TraceEventType.Error;
                case LogLevel.Information:
                    return TraceEventType.Information;
                case LogLevel.Warning:
                    return TraceEventType.Warning;
                default:
                    return TraceEventType.Verbose;
            }
        }

        private sealed class NullScope : IDisposable
        {
            public static NullScope Instance => new NullScope();

            private NullScope()
            {
            }

            public void Dispose()
            {

            }
        }
    }
}
