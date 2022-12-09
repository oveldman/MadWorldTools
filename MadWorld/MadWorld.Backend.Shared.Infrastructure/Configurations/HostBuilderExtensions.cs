using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Shared.Infrastructure.Configurations;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddMadWorldLogging(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureLogging((context, builder) =>
        {
            var applicationInsightConnectionString = context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];

            if (string.IsNullOrEmpty(applicationInsightConnectionString))
            {
                builder.AddConsole();
            }
            else
            {
                builder.AddApplicationInsights(
                    configureTelemetryConfiguration: (config) => config.ConnectionString = applicationInsightConnectionString,
                    configureApplicationInsightsLoggerOptions: (options) => { }
                );
            }
        });
    }
}