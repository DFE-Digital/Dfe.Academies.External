using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationTests
{
    [Test]
    public void ConversionApplication___PropertyCheck___Success()
    {
        // arrange
        var conversionApplication = new ConversionApplication
        {
            Id = int.MaxValue,
            ApplicationType = ApplicationTypes.FormNewMat,
            UserEmail = "mark.robinson@education.gov.uk",
            Application = "test",
            TrustName = "Pudsey School",
            SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(),
            ConversionApplicationComponents = new List<ConversionApplicationComponent>() ,
            ConversionApplicationContributors = new List<ConversionApplicationContributor>() 
        };

        // act
        // nothing!

        // assert
        Assert.That(conversionApplication, Is.Not.Null);
        Assert.That(conversionApplication.Id, Is.EqualTo(int.MaxValue));
        Assert.That(conversionApplication.ApplicationType, Is.EqualTo(ApplicationTypes.FormNewMat));
        Assert.That(conversionApplication.UserEmail, Is.EqualTo("mark.robinson@education.gov.uk"));
        Assert.That(conversionApplication.Application, Is.EqualTo("test"));
        Assert.That(conversionApplication.TrustName, Is.EqualTo("Pudsey School"));
        Assert.That(conversionApplication.SchoolOrSchoolsApplyingToConvert.Count, Is.EqualTo(0));
        Assert.That(conversionApplication.ConversionApplicationComponents.Count, Is.EqualTo(0));
        Assert.That(conversionApplication.ConversionApplicationContributors.Count, Is.EqualTo(0));
    }
}