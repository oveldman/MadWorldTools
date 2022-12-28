using System.Reflection;
using MadOffice.Domain.General;
using Microsoft.AspNetCore.Components;

namespace MadOffice.UI.Pages;

public partial class Info
{
    private string _defaultFolder = string.Empty;
    private string _version = string.Empty;

    [Inject] 
    private IDefaultFolder DefaultFolder { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        _defaultFolder = DefaultFolder.GetAppdataFolder();
        _version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        base.OnInitialized();
    }
}