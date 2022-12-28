using MadOffice.Domain.Emails.Models;

namespace MadOffice.Application.Extensions;

public static class PersonsExtensions
{
    public static void AddIds(this IEnumerable<Person> persons)
    {
        var currentId = 1;
        foreach (var person in persons)
        {
            person.Id = currentId++;
        }
    }
    
    public static bool EmailExists(this IEnumerable<Person> persons, string email, int excludeId = 0)
    {
        return persons.Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && p.Id != excludeId);
    }

    public static IEnumerable<Person> Filter(this IEnumerable<Person> persons, EmailType emailType)
    {
        if (emailType is EmailType.All or EmailType.None)
        {
            return persons;
        }

        return persons.Where(p => p.EmailType == emailType).ToList();
    }

    public static int NextId(this List<Person> persons)
    {
        return persons.Any() ? persons.Max(p => p.Id) + 1 : 1;
    }

    public static void Set(this IEnumerable<Person> persons, EmailType emailType)
    {
        foreach (var person in persons)
        {
            person.EmailType = emailType;
        }
    }

    public static void Update(this List<Person> persons, Person person)
    {
        var index = persons.FindIndex(p => p.Id == person.Id);
        
        if (index != -1)
        {
            persons[index] = person;
        }
    }
}