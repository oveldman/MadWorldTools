using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using MadOffice.Domain.Emails.Models;
using MadOffice.Domain.General;
using MadOffice.Infrastructure.Emails;
using Moq;

namespace MadOffice.Tests.Unittests.Infrastructure.Emails;

public class EmailReaderTests
{
        private const string EmailPath = "MadOffice.Emails.json";
    
    [Fact]
    public void Get_GivenAPersonJson_OneCorrectPerson()
    {
        const string testJson = """
        [
            {
                "Id": 1,
                "Email": "test@test.nl",
                "FirstName": "Bert",
                "LastName": "Smith"
            }
        ]
        """;
        
        // Arrange
        var defaultFolder = new Mock<IDefaultFolder>();
        defaultFolder.Setup(df => df.GetAppdataFolder()).Returns("/");
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { EmailPath, new MockFileData(testJson) }
        });
        var reader = new EmailReader(defaultFolder.Object, fileSystem);
        
        // Act
        var persons = reader.Get();

        // Assert
        persons.Should().ContainSingle();
    }
    
    [Fact]
    public void Get_GivenNotASaveFile_EmptyPersonList()
    {
        // Arrange
        var defaultFolder = new Mock<IDefaultFolder>();
        defaultFolder.Setup(df => df.GetAppdataFolder()).Returns("/");
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        var reader = new EmailReader(defaultFolder.Object, fileSystem);
        
        // Act
        var persons = reader.Get();

        // Assert
        persons.Should().BeEmpty();
    }

    [Fact]
    public void Save_GivenAPersonList_SavesCorrectJson()
    {
        // Arrange
        const string testJson = """
        [{"Id":1,"Email":"test@test.nl","EmailType":1,"FirstName":"Bert","LastName":"Smith"}]
        """;

        var defaultFolder = new Mock<IDefaultFolder>();
        defaultFolder.Setup(df => df.GetAppdataFolder()).Returns("/");
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        var reader = new EmailReader(defaultFolder.Object, fileSystem);
        var persons = new List<Person>
        {
            new()
            {
                Id = 1,
                Email = "test@test.nl",
                FirstName = "Bert",
                LastName = "Smith",
                EmailType = EmailType.GeneralInfo
            }
        };
        
        // act
        reader.Save(persons);
        
        // assert
        var emailFile = fileSystem.GetFile(EmailPath);
        emailFile.TextContents.Should().Be(testJson);
    }
}