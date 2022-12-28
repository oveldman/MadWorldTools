using System.IO.Abstractions;
using System.Text.Json;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using MadOffice.Domain.General;

namespace MadOffice.Infrastructure.Emails;

public class EmailReader : IEmailReader
{
    private string EmailFile => Path.Combine(_defaultFolder, "MadOffice.Emails.json");
    private readonly string _defaultFolder;
    private readonly IFileSystem _fileSystem;
    
    public EmailReader(IDefaultFolder defaultFolder, IFileSystem fileSystem)
    {
        _defaultFolder = defaultFolder.GetAppdataFolder();
        _fileSystem = fileSystem;
    }

    public List<Person> Get()
    {
        CreateSaveFileIfNotExists();
        var emailsJson = _fileSystem.File.ReadAllText(EmailFile);
        return JsonSerializer.Deserialize<List<Person>>(emailsJson) ?? new List<Person>();
    }

    public bool Save(List<Person> persons)
    {
        var personsJson = JsonSerializer.Serialize(persons);
        _fileSystem.File.WriteAllText(EmailFile, personsJson);
        return true;
    }

    private void CreateSaveFileIfNotExists()
    {
        if (!_fileSystem.Directory.Exists(_defaultFolder))
        {
            _fileSystem.Directory.CreateDirectory(_defaultFolder);
        }
        
        if (!_fileSystem.File.Exists(EmailFile))
        {
            CreateSaveFile();
        }
    }

    private void CreateSaveFile()
    {
        const string personJson = "[]";
        _fileSystem.File.WriteAllText(EmailFile, personJson);
    }   
}