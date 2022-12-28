using AutoFixture.Xunit2;
using FluentAssertions;
using MadOffice.Application.Emails;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using Moq;

namespace MadOffice.Tests.Unittests.Application.Emails;

public class EmailEditorTests
{
        [Theory]
    [AutoDomainData]
    public void AddNew_GivenANewPerson_TotalOfFourPersons(
            [Frozen] Mock<IEmailReader> emailReaderMock,
            EmailEditor emailEditor,
            List<Person> persons)
    {
        // Arrange
        persons[0].Id = 1;
        persons[1].Id = 2;
        persons[2].Id = 3;
        
        emailReaderMock.Setup(x => x.Get()).Returns(persons);
        
        var newPerson = new Person()
        {
            Id = 0,
            FirstName = "John",
            LastName = "Doe",
            Email = "test@test.nl"
        };

        // Act
        emailEditor.AddNew(newPerson);

        // Assert
        newPerson.Id.Should().Be(4);
        persons.Should().HaveCount(4);
        persons[3].Should().BeEquivalentTo(newPerson);
    }
    
    [Theory]
    [AutoDomainData]
    public void Delete_GivenAId_TotalOfTwoPersons(
        [Frozen] Mock<IEmailReader> emailReaderMock,
        EmailEditor emailEditor,
        List<Person> persons)
    {
        // Arrange
        persons[0].Id = 1;
        persons[1].Id = 2;
        persons[2].Id = 3;
        
        emailReaderMock.Setup(x => x.Get()).Returns(persons);
        
        const int idToDelete = 2;

        // Act
        var deleted = emailEditor.Delete(idToDelete);

        // Assert
        persons.Should().HaveCount(2);
        deleted.Should().BeTrue();
    }
    
    [Theory]
    [AutoDomainData]
    public void Delete_GivenANotValidId_TotalOfThreePersons(
        [Frozen] Mock<IEmailReader> emailReaderMock,
        EmailEditor emailEditor,
        List<Person> persons)
    {
        // Arrange
        persons[0].Id = 1;
        persons[1].Id = 2;
        persons[2].Id = 3;
        
        emailReaderMock.Setup(x => x.Get()).Returns(persons);
        
        const int idToDelete = 4;

        // Act
        var deleted = emailEditor.Delete(idToDelete);

        // Assert
        persons.Should().HaveCount(3);
        deleted.Should().BeFalse();
    }
    
    [Theory]
    [AutoDomainData]
    public void Update_NewPersonInfo_PersonUpdated(
        [Frozen] Mock<IEmailReader> emailReaderMock,
        EmailEditor emailEditor,
        List<Person> persons)
    {
        // Arrange
        persons[0].Id = 1;
        persons[1].Id = 2;
        persons[2].Id = 3;
        
        emailReaderMock.Setup(x => x.Get()).Returns(persons);

        var personUpdated = new Person()
        {
            Id = 2,
            FirstName = "John",
            LastName = "Doe",
            Email = "test@test.nl"
        };

        // Act
        emailEditor.Update(personUpdated);

        // Assert
        persons.Should().HaveCount(3);
        persons[1].Should().BeEquivalentTo(personUpdated);
    }
    
    [Theory]
    [AutoDomainData]
    public void Update_NotValidPersonID_PersonNotUpdated(
        [Frozen] Mock<IEmailReader> emailReaderMock,
        EmailEditor emailEditor,
        List<Person> persons)
    {
        // Arrange
        persons[0].Id = 1;
        persons[1].Id = 2;
        persons[2].Id = 3;
        
        emailReaderMock.Setup(x => x.Get()).Returns(persons);

        var personUpdated = new Person()
        {
            Id = 4,
            FirstName = "John",
            LastName = "Doe",
            Email = "test@test.nl"
        };

        // Act
        emailEditor.Update(personUpdated);

        // Assert
        persons.Should().HaveCount(3);
        persons[0].Should().NotBeEquivalentTo(personUpdated);
        persons[1].Should().NotBeEquivalentTo(personUpdated);
        persons[2].Should().NotBeEquivalentTo(personUpdated);
    }
}