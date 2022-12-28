using MadOffice.Domain.Emails.Models;

namespace MadOffice.Domain.Emails.Interfaces;

public interface IEmailImporter
{
    bool Import(MemoryStream file, EmailType setEmailType);
}