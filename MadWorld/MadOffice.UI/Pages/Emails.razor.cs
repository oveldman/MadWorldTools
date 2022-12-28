using MadOffice.Application.Extensions;
using MadOffice.Application.Gui;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using MadOffice.UI.Parts.Emails;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace MadOffice.UI.Pages;

public partial class Emails
{
    private RadzenDataGrid<Person> _personGrid = default!;
    private RenderFragment _deletePersonDialog = default!;
    private RenderFragment _newPersonDialog = default!;

    private string _errorMessage = string.Empty;

    private IEnumerable<Person> _persons = new List<Person>();
    private Person _editPersonOldValue = new();
    private RadzenRequiredValidator _editRequiredValidator = default!;
    private RadzenEmailValidator _editEmailValidator = default!;
    
    private readonly List<DropdownOption> _emailTypes = new();
    private readonly List<DropdownOption> _editEmailTypes = new();
    private EmailType _selectedEmailType = EmailType.All;

    private Person _deletePerson = new();

    private bool _readMode = true;

    [Inject]
    private DialogService DialogService { get; set; } = default!;
    [Inject]
    private IEmailEditor EmailEditor { get; set; } = default!;
    [Inject]
    private IEmailFinder EmailFinder { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        InitEmails();
        GetEmails(_selectedEmailType);
    }

    private void InitEmails()
    {
        _emailTypes.Add(
            new DropdownOption(EmailType.All.GetDisplayName(), (int)EmailType.All));
        _emailTypes.Add(
            new DropdownOption(EmailType.GeneralInfo.GetDisplayName(), (int)EmailType.GeneralInfo));
        _emailTypes.Add(
            new DropdownOption(EmailType.MailMagazine.GetDisplayName(), (int)EmailType.MailMagazine));
        
        _editEmailTypes.Add(
            new DropdownOption(EmailType.None.GetDisplayName(), (int)EmailType.None));
        _editEmailTypes.Add(
            new DropdownOption(EmailType.GeneralInfo.GetDisplayName(), (int)EmailType.GeneralInfo));
        _editEmailTypes.Add(
            new DropdownOption(EmailType.MailMagazine.GetDisplayName(), (int)EmailType.MailMagazine));
    }

    private async Task OpenDialogNewPerson()
    {
        await DialogService.OpenAsync(
            "Add a new Person", 
            ds => _newPersonDialog, 
            new DialogOptions()
            {
                ShowTitle = true, Style = "min-height:auto;min-width:auto;width:auto", 
                CloseDialogOnEsc = true
            });
    }

    private async Task OpenDialogConfirmDelete(Person person)
    {
        _deletePerson = person;
        
        await DialogService.OpenAsync(
            $"Delete Person: {person.FirstName} {person.LastName}", 
            ds => _deletePersonDialog, 
            new DialogOptions()
            {
                ShowTitle = true, Style = "min-height:auto;min-width:auto;width:auto", 
                CloseDialogOnEsc = true,
            });
    }
    
    private async Task EditRow(Person person)
    {
        _readMode = false;
        
        _editPersonOldValue.Id = person.Id;
        _editPersonOldValue.Email = person.Email;
        _editPersonOldValue.FirstName = person.FirstName;
        _editPersonOldValue.LastName = person.LastName;
            
        await _personGrid.EditRow(person);
    }

    private void FilterPersons(object filterValue)
    {
        if (filterValue is not int emailType) return;

        _selectedEmailType = (EmailType) emailType;
        GetEmails(_selectedEmailType);
    }
    
    private async Task SaveRow(Person person)
    {
        _errorMessage = string.Empty;
        var emailAlreadyExists = _persons.EmailExists(person.Email, person.Id);
        if (!_editRequiredValidator.IsValid || !_editEmailValidator.IsValid || emailAlreadyExists)
        {
            if (emailAlreadyExists)
            {
                _errorMessage = $"Email {person.Email} already exists";
            }
            
            return;
        }

        _readMode = true;
        EmailEditor.Update(person);
        await _personGrid.UpdateRow(person);
        _editPersonOldValue = new Person();
        GetEmails(_selectedEmailType);
    }

    private void CancelEdit(Person person)
    {
        _readMode = true;
        
        person.Id = _editPersonOldValue.Id;
        person.Email = _editPersonOldValue.Email;
        person.FirstName = _editPersonOldValue.FirstName;
        person.LastName = _editPersonOldValue.LastName;
        
        _personGrid.CancelEditRow(person);
        _editPersonOldValue = new Person();
        _errorMessage = string.Empty;
    }

    private void GetEmails(EmailType emailType)
    {
        _persons = EmailFinder.Get(emailType);
        StateHasChanged();
    }
}