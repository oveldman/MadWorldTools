using MadOffice.Domain.Emails.Models;

namespace MadOffice.Domain.Emails.Interfaces;

public interface IEmailReader
{
    List<Person> Get();
    bool Save(List<Person> persons);
}