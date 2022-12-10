using MadWorld.Monaco.Models;

namespace MadWorld.Monaco.Application;

public interface IMonacoJsInterop
{
    ValueTask<string> Init(Action<MonacoSettings> settings, string body);
    ValueTask DisposeAsync();
}