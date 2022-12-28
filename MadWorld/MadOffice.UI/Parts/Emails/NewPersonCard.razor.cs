using MadOffice.Application.Extensions;
using MadOffice.Application.Gui;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadOffice.UI.Parts.Emails;

public partial class NewPersonCard : ComponentBase
{
    [Parameter]
    public IEnumerable<Person> Persons { get; set; } = default!;
    
    [Parameter]
    public Action<EmailType> GetEmails { get; set; } = default!;
    
    [Parameter]
    public EmailType SelectedEmailType { get; set; }

    [Parameter] 
    public RenderFragment NewPersonDialog { get; set; } = default!;

    private List<DropdownOption> _emailTypes = new();
    private List<DropdownOption> _newEmailTypes = new();

    private string _errorMessagePopup = string.Empty;
    
    private Person _newPerson = new();
    private int _newSelectedEmailType;
    
    [Inject]
    private DialogService DialogService { get; set; } = default!;
    
    [Inject]
    private IEmailEditor EmailEditor { get; set; } = default!;

    protected override void OnInitialized()
    {
        InitEmail();
        base.OnInitialized();
    }

    private void InitEmail()
    {
        _newEmailTypes.Add(
            new DropdownOption(EmailType.None.GetDisplayName(), (int)EmailType.None));
        _newEmailTypes.Add(
            new DropdownOption(EmailType.GeneralInfo.GetDisplayName(), (int)EmailType.GeneralInfo));
        _newEmailTypes.Add(
            new DropdownOption(EmailType.MailMagazine.GetDisplayName(), (int)EmailType.MailMagazine));

        _newSelectedEmailType = _newEmailTypes.First().Value;
    }

    private void SaveNewPersonAndCloseDialog()
    {
        if (Persons.EmailExists(_newPerson.Email))
        {
            _errorMessagePopup = $"Email {_newPerson.Email} already exists";
            return;
        }

        _newPerson.EmailType = (EmailType)_newSelectedEmailType;
        EmailEditor.AddNew(_newPerson);
        _newPerson = new Person();
        DialogService.Close();
        GetEmails(SelectedEmailType);
    }
    
    private void OnInvalidSubmitNewPerson()
    {
    }
}