using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolApplyingToConvertTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void SchoolOrSchoolsApplyingToConvert___PropertyCheck___Success()
    {
        // arrange
        int urn = Fixture.Create<int>();
        string? ukprn = null;
        string schoolName = Fixture.Create<string>();
        string street = Fixture.Create<string>();
        string town = Fixture.Create<string>();
        string fullUkPostcode = Fixture.Create<string>();

        // 
        var conversionApplication = new SchoolApplyingToConvert(schoolName, urn, ukprn, street, town, fullUkPostcode)
        {
	        SchoolId = int.MaxValue
        };

        // act
        // nothing!

        // assert
        Assert.That(conversionApplication, Is.Not.Null);
        Assert.That(conversionApplication.URN, Is.EqualTo(urn));
        Assert.That(conversionApplication.UKPRN, Is.EqualTo(ukprn));
        Assert.That(conversionApplication.SchoolName, Is.EqualTo(schoolName));
        Assert.That(conversionApplication.Street, Is.EqualTo(street));
        Assert.That(conversionApplication.Town, Is.EqualTo(town));
        Assert.That(conversionApplication.FullUkPostcode, Is.EqualTo(fullUkPostcode));
        Assert.That(conversionApplication.SchoolId, Is.EqualTo(int.MaxValue));
    }
}