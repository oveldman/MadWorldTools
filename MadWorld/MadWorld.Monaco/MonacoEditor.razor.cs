using MadWorld.Monaco.Application;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Monaco;

public partial class MonacoEditor
{
    [Parameter]
    public string HeightPixels { get; set; } = "500px";
    
    [Parameter]
    public string SoftwareLanguage { get; set; } = "csharp";
    
    private string _editorId = $"editor_{Guid.NewGuid()}";
    private string _editorBody = string.Empty;
    
    [Inject] private IMonacoJsInterop MonacoJsInterop { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await MonacoJsInterop.Init(settings =>
            {
                settings.ContainerId = _editorId;
                settings.Language = SoftwareLanguage;
            },
            _editorBody
            );
        
        await base.OnInitializedAsync();
    }
    
    public async Task SetBody(string body)
    {
        await MonacoJsInterop.SetBody(body);
    }
}