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
        string schoolName = Fixture.Create<string>();
        
        var conversionApplication = new SchoolApplyingToConvert(schoolName)
        {
            Id = int.MaxValue
        };

        // act
        // nothing!

        // assert
        Assert.That(conversionApplication, Is.Not.Null);
        Assert.That(conversionApplication.Id, Is.EqualTo(int.MaxValue));
        Assert.That(conversionApplication.SchoolName, Is.EqualTo(schoolName));
    }
}