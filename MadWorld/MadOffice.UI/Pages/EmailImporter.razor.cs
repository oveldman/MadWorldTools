using MadOffice.Application.Extensions;
using MadOffice.Application.Gui;
using MadOffice.Domain.Emails.Exceptions;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MadOffice.UI.Pages;

public partial class EmailImporter
{
    private bool HasError { get; set; }
    private bool Succeed { get; set; }
    private bool Uploaded { get; set; }
    private MemoryStream _uploadBody = new();

    private int _selectedEmailType = -1;
    private readonly List<DropdownOption> _emailTypes = new();
    
    [Inject]
    private IEmailImporter Importer { get; set; } = default!;

    protected override void OnInitialized()
    {
        InitEmails();
        base.OnInitialized();
    }
    
    private void InitEmails()
    {
        _emailTypes.Add(
            new DropdownOption(EmailType.None.GetDisplayName(), (int)EmailType.None));
        _emailTypes.Add(
            new DropdownOption(EmailType.GeneralInfo.GetDisplayName(), (int)EmailType.GeneralInfo));
        _emailTypes.Add(
            new DropdownOption(EmailType.MailMagazine.GetDisplayName(), (int)EmailType.MailMagazine));
    }

    private async Task SingleUpload(InputFileChangeEventArgs e)
    {
        _uploadBody = new MemoryStream();
        await e.File.OpenReadStream(maxAllowedSize: 10000 * 1024).CopyToAsync(_uploadBody);
        Uploaded = true;
    }

    private void ProcessExcelFile()
    {
        var emailType = (EmailType)_selectedEmailType;

        try
        {
            Succeed = Importer.Import(_uploadBody, emailType);
        }
        catch (EmailImportException)
        {
            HasError = true;
            Succeed = false;
        }
    }
}