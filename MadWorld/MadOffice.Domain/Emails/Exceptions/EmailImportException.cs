using System.Runtime.Serialization;

namespace MadOffice.Domain.Emails.Exceptions;

[Serializable]
public class EmailImportException : Exception
{
    public EmailImportException(string message) : base(message)
    {
    }
    
    protected EmailImportException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}