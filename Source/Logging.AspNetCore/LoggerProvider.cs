using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System;
using System.Collections.Generic;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.AspNetCore
{
    internal class LoggerProvider : ILoggerProvider
    {
        public LoggerOptions Options { get; set; }

        public LoggerProvider()
            : this(null)
        {

        }

        public LoggerProvider(LoggerOptions options)
        {
            Options = options;
            InitializeLogger(options);
        }        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new AspNetLogger(BuildCategories(categoryName), Options);
        }

        private ICollection<string> BuildCategories(string categoryName) => new List<string>() { categoryName };

        private void InitializeLogger(LoggerOptions options)
        {
            string configurationFilepath = options?.ConfigurationFilepath;
            IConfigurationSource configurationSource;
            if (!string.IsNullOrEmpty(configurationFilepath))
            {
                configurationSource = new FileConfigurationSource(configurationFilepath);
            }
            else
            {
                configurationSource = new SystemConfigurationSource();
            }
            
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());
        }
    }
}
