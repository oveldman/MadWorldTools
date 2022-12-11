using MadWorld.Monaco.Models;

namespace MadWorld.Monaco.Application;

public interface IMonacoJsInterop
{
    ValueTask Init(Action<MonacoSettings> settings, string body);
    ValueTask SetBody(string body);
    ValueTask DisposeAsync();
}