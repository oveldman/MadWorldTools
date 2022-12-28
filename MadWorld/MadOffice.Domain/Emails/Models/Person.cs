using System.Text.Json.Serialization;

namespace MadOffice.Domain.Emails.Models;

public class Person
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public EmailType EmailType { get; set; }

    [JsonIgnore]
    public int EmailTypeId
    {
        get => EmailType == EmailType.All ? (int) EmailType.None : (int) EmailType;
        set => EmailType = (EmailType) value;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}