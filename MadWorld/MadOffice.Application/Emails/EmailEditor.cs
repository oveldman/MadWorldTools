using MadOffice.Application.Extensions;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;

namespace MadOffice.Application.Emails;

public class EmailEditor : IEmailEditor
{
    private readonly IEmailReader _emailReader;
    
    public EmailEditor(IEmailReader emailReader)
    {
        _emailReader = emailReader;
    }
    
    public bool AddNew(Person person)
    {
        var persons = _emailReader.Get();
        person.Id = persons.NextId();
        persons.Add(person);
        return _emailReader.Save(persons);
    }

    public bool Update(Person person)
    {
        var persons = _emailReader.Get();
        persons.Update(person);
        return _emailReader.Save(persons);
    }

    public bool Delete(int id)
    {
        var persons = _emailReader.Get();
        var totalDeleted = persons.RemoveAll(p => p.Id == id);
        _emailReader.Save(persons);
        return totalDeleted > 0;
    }
}