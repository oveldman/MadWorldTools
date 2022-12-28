using MadOffice.Domain.Emails.Models;

namespace MadOffice.Domain.Emails.Interfaces;

public interface IEmailHelper
{
    string CopyAllEmails(IEnumerable<Person> persons);
}