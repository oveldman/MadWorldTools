using MadOffice.Domain.Emails.Models;

namespace MadOffice.Domain.Emails.Interfaces;

public interface IEmailFinder
{
    IEnumerable<Person> Get(EmailType emailType);
}