using MadOffice.Application.Extensions;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;

namespace MadOffice.Application.Emails;

public class EmailFinder : IEmailFinder
{
    private readonly IEmailReader _reader;
    
    public EmailFinder(IEmailReader reader)
    {
        _reader = reader;
    }
    
    public IEnumerable<Person> Get(EmailType emailType)
    {
        var persons = _reader.Get();
        return persons.Filter(emailType);
    }
}