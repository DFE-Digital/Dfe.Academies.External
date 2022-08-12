using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolApplyingToConvertTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void Constructor___PropertiesSet()
    {
        // arrange
        int applicationId = Fixture.Create<int>();
        int urn = Fixture.Create<int>();
        string schoolName = Fixture.Create<string>();

        var conversionApplication = new SchoolApplyingToConvert(schoolName, urn, applicationId, null);

        // act
        // nothing!

        // assert
        Assert.That(conversionApplication, Is.Not.Null);
        Assert.That(conversionApplication.URN, Is.EqualTo(urn));
        Assert.That(conversionApplication.SchoolName, Is.EqualTo(schoolName));
    }
}