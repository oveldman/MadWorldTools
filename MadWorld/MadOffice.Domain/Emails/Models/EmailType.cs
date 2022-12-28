using System.ComponentModel.DataAnnotations;

namespace MadOffice.Domain.Emails.Models;

public enum EmailType
{
    [Display(Name = "None")]
    None = -1,
    
    [Display(Name = "All emails")]
    All = 0,
    
    [Display(Name = "General Info")]
    GeneralInfo = 1,
    
    [Display(Name = "Mail Magazine")]
    MailMagazine = 2
}