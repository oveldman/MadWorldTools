using FluentAssertions;
using MadOffice.Application.Emails;
using MadOffice.Domain.Emails.Models;

namespace MadOffice.Tests.Unittests.Application.Emails;

public class EmailHelperTests
{
    [Theory]
    [AutoDomainData]
    public void CopyAllEmails_GivenAListOfPersons_BccOfEmails(
        EmailHelper emailHelper,
        List<Person> persons)
    {
        // Arrange
        persons[0].Email = "test@test.nl";
        persons[1].Email = "random@random.nl";
        persons[2].Email = "mad@office.nl";

        // Act
        var emailBcc = emailHelper.CopyAllEmails(persons);

        // Assert
        emailBcc.Should().Be("test@test.nl; random@random.nl; mad@office.nl");
    }
}