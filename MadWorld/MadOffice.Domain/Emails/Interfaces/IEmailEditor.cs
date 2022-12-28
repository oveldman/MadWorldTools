using MadOffice.Domain.Emails.Models;

namespace MadOffice.Domain.Emails.Interfaces;

public interface IEmailEditor
{
    bool AddNew(Person person);
    bool Update(Person person);
    bool Delete(int id);
}