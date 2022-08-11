using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationContributorTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void ConversionApplicationContributor___PropertyCheck___Success()
    {
        // arrange
        string firstName = Fixture.Create<string>();
        string surName = Fixture.Create<string>();
        string? role = null;
        SchoolRoles schoolRole = SchoolRoles.Chair;

        var applicationComponent = new ConversionApplicationContributor(firstName, surName, schoolRole, role)
        {
            ContributorId = int.MaxValue
        };

        // act
        // nothing!

        // assert
        Assert.That(applicationComponent, Is.Not.Null);
        Assert.That(applicationComponent.ContributorId, Is.EqualTo(int.MaxValue));
        Assert.That(applicationComponent.FirstName, Is.EqualTo(firstName));
        Assert.That(applicationComponent.Surname, Is.EqualTo(surName));
        Assert.That(applicationComponent.Role, Is.EqualTo(schoolRole));
    }
}