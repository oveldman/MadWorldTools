using System.Reflection;
using MadWorld.Frontend.Shared.Tools.UI;

namespace MadWorld.Blazor.Tools.UI;

public partial class App
{
    private readonly List<Assembly> _loadedAssemblies = new()
    {
        AssembleInfoBlazorShared.Info
    };
}