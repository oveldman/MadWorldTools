using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadOffice.UI.Parts.Emails;

public partial class DeletePersonCard : ComponentBase
{
    [Parameter]
    public Action<EmailType> GetEmails { get; set; } = default!;
    
    [Parameter]
    public EmailType SelectedEmailType { get; set; }
    
    [Parameter] 
    public RenderFragment DeletePersonDialog { get; set; } = default!;

    [Parameter] public Person DeletePerson { get; set; } = default!;
    private string _errorMessagePopup = string.Empty;

    [Inject]
    private DialogService DialogService { get; set; } = default!;
    
    [Inject]
    private IEmailEditor EmailEditor { get; set; } = default!;

    private void DeletePersonAndCloseDialog()
    {
        EmailEditor.Delete(DeletePerson.Id);
        DeletePerson = new Person();
        DialogService.Close();
        GetEmails(SelectedEmailType);
    }
}