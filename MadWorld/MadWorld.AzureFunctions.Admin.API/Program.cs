using MadWorld.Backend.Shared.Infrastructure.Configurations;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .AddMadWorldLogging()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();