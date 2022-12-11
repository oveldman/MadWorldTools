using System.Reflection;

namespace MadWorld.Frontend.Shared.Tools.UI;

public static class AssembleInfoBlazorShared
{
    public static readonly Assembly Info = typeof(_Imports).Assembly;
}