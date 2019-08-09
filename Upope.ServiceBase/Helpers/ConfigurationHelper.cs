using Microsoft.Extensions.Configuration;
using System;

namespace Upope.ServiceBase.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        public static bool IsDevelopment()
        {
            var settings = GetConfig();

            return (settings["Environment"] != null && settings["Environment"] == "Development") ? true : false;
        }
    }
}
