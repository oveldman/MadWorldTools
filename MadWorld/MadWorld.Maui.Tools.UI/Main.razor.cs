using System.Reflection;
using MadWorld.Frontend.Shared.Tools.UI;

namespace MadWorld.Maui.Tools.UI;

public partial class Main
{
    private readonly List<Assembly> _loadedAssemblies = new()
    {
        AssembleInfoBlazorShared.Info
    };
}