using MadOffice.Domain.General;

namespace MadOffice.UI.MauiLogic;

public class DefaultFolder : IDefaultFolder
{
    public string GetAppdataFolder() => Path.Combine(FileSystem.Current.AppDataDirectory, "Mad-World");
}