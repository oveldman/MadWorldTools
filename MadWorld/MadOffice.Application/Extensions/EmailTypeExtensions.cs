using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MadOffice.Domain.Emails.Models;

namespace MadOffice.Application.Extensions;

public static class EmailTypeExtensions
{
    public static string GetDisplayName(this EmailType emailType)
    {
        return typeof(EmailType).GetMember(emailType.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? string.Empty;
    }
}