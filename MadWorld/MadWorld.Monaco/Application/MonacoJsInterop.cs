using MadWorld.Monaco.Models;
using Microsoft.JSInterop;

namespace MadWorld.Monaco.Application;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class MonacoJsInterop : IMonacoJsInterop, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public MonacoJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/MadWorld.Monaco/monacoJsInterop.js").AsTask());
    }

    public async ValueTask<string> Init(Action<MonacoSettings> settings, string body)
    {
        MonacoSettings monacoSettings = new();
        settings.Invoke(monacoSettings);
        
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("init", monacoSettings.ContainerId, body, monacoSettings.Language);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}