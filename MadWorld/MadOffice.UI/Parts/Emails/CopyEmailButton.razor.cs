using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using Microsoft.AspNetCore.Components;

namespace MadOffice.UI.Parts.Emails;

public partial class CopyEmailButton
{
    [Parameter] 
    public IEnumerable<Person> _persons { get; set; } = new List<Person>();

    [Inject]
    private IEmailHelper EmailHelper { get; set; } = default!;
    
    private async Task CopyEmailsToClipboard()
    {
        var emails = EmailHelper.CopyAllEmails(_persons);
        await Clipboard.Default.SetTextAsync(emails);
    }
}