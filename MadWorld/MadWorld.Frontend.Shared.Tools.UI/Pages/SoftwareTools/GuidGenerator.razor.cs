using MadWorld.Monaco;

namespace MadWorld.Frontend.Shared.Tools.UI.Pages.SoftwareTools;

public partial class GuidGenerator
{
    private MonacoEditor _editor = new();

    private int _amountOfGuids = 0;

    private async Task GenerateGuid()
    {
        ValidateAmountOfGuids();

        var newEditorValue = string.Empty;
        var isFirstGuid = true;

        for (var i = 0; i < _amountOfGuids; i++)
        {
            newEditorValue = AddNewGuidToString(newEditorValue, isFirstGuid);
            isFirstGuid = false;
        }

        await _editor.SetBody(newEditorValue);
    }

    private void ValidateAmountOfGuids()
    {
        if (_amountOfGuids < 0)
        {
            _amountOfGuids = 0;
        }

        if (_amountOfGuids > 10000)
        {
            _amountOfGuids = 10000;
        }
    }

    private static string AddNewGuidToString(string value, bool isFirstGuid)
    {
        if (!isFirstGuid)
        {
            value += Environment.NewLine;
        }

        value += Guid.NewGuid().ToString();

        return value;
    }
}