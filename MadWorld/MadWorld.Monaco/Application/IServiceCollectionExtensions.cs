using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Monaco.Application;

public static class IServiceCollectionExtensions
{
    public static void AddMonacoEditor(this IServiceCollection services)
    {
        services.AddScoped<IMonacoJsInterop, MonacoJsInterop>();
    }
}