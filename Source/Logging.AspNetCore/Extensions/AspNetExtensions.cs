using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.AspNetCore
{
    public static class AspNetExtensions
    {
        public static IWebHostBuilder UseEnterpriseLibraryLog(this IWebHostBuilder builder)
        {
            return UseEnterpriseLibraryLog(builder, () => null);
        }

        public static IWebHostBuilder UseEnterpriseLibraryLog(this IWebHostBuilder builder, Func<LoggerOptions> optionsFunc)
        {
            return UseEnterpriseLibraryLog(builder, optionsFunc());
        }

        public static IWebHostBuilder UseEnterpriseLibraryLog(this IWebHostBuilder builder, LoggerOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices(services => ConfigureEnterpriseLibraryLog(options, services, serviceProvider => serviceProvider.GetService<IConfiguration>()));
            return builder;
        }

        private static void ConfigureEnterpriseLibraryLog(LoggerOptions options, IServiceCollection services, Func<IServiceProvider, IConfiguration> lookupConfiguration)
        {
            services.AddSingleton<ILoggerProvider>(serviceProvider =>
            {
                LoggerProvider provider = new LoggerProvider(options ?? new LoggerOptions());
                IConfiguration configuration = lookupConfiguration(serviceProvider);
                return provider;
            });
        }
    }
}
