using System.Collections.Generic;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
public class ConversionApplicationTests
{
	[Test]
    public void ConversionApplication_IsMapped()
    {
        // arrange
        var conversionApplication = new ConversionApplication
        {
            Id = int.MaxValue,
            ApplicationType = ApplicationTypes.FormNewMat,
            UserEmail = "mark.robinson@education.gov.uk",
            Application = "test",
            TrustName = "Pudsey School",
            SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>()
        };

        // assert
        Assert.That(conversionApplication, Is.Not.Null);
        Assert.That(conversionApplication.Id, Is.EqualTo(int.MaxValue));
        Assert.That(conversionApplication.ApplicationType, Is.EqualTo(ApplicationTypes.FormNewMat));
        Assert.That(conversionApplication.UserEmail, Is.EqualTo("mark.robinson@education.gov.uk"));
        Assert.That(conversionApplication.Application, Is.EqualTo("test"));
        Assert.That(conversionApplication.TrustName, Is.EqualTo("Pudsey School"));
        Assert.That(conversionApplication.SchoolOrSchoolsApplyingToConvert.Count, Is.EqualTo(0));
    }
}