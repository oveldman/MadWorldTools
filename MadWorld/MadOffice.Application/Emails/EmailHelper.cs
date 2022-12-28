using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;

namespace MadOffice.Application.Emails;

public class EmailHelper : IEmailHelper
{
    public string CopyAllEmails(IEnumerable<Person> persons)
    {
        var emails = persons.Select(p => p.Email);
        return string.Join("; ", emails);
    }
}