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
            SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>()
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

    [Test]
    public void ConversionApplication___ApplicationStatus___NotStarted___PropertyCheck()
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

        // act
        var calculatedApplicationStatus = conversionApplication.ApplicationStatus;

        // assert
        Assert.That(calculatedApplicationStatus, Is.EqualTo(ApplicationComponentsStatus.NotStarted));
    }

    [Test]
    public void ConversionApplication___ApplicationStatus___NotStarted2___PropertyCheck()
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

        conversionApplication.ConversionApplicationComponents.AddRange(new List<ConversionApplicationComponent>
        {
            new(name:"Contact details") {Id = 1, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Performance and safeguarding") {Id = 2, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Pupil numbers") {Id = 3, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Finances") {Id = 4, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Partnerships and affiliations") {Id = 5, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Religious education") {Id = 6, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Land and buildings") {Id = 7, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Local authority") {Id = 8, Status = ApplicationComponentsStatus.NotStarted}
        });

        // act
        var calculatedApplicationStatus = conversionApplication.ApplicationStatus;

        // assert
        Assert.That(calculatedApplicationStatus, Is.EqualTo(ApplicationComponentsStatus.NotStarted));
    }

    [Test]
    public void ConversionApplication___ApplicationStatus___InProgress___PropertyCheck()
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

        conversionApplication.ConversionApplicationComponents.AddRange(new List<ConversionApplicationComponent>
        {
            new(name:"Contact details") {Id = 1, Status = ApplicationComponentsStatus.Completed},
            new(name:"Performance and safeguarding") {Id = 2, Status = ApplicationComponentsStatus.InProgress},
            new(name:"Pupil numbers") {Id = 3, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Finances") {Id = 4, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Partnerships and affiliations") {Id = 5, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Religious education") {Id = 6, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Land and buildings") {Id = 7, Status = ApplicationComponentsStatus.NotStarted},
            new(name:"Local authority") {Id = 8, Status = ApplicationComponentsStatus.NotStarted}
        });

        // act
        var calculatedApplicationStatus = conversionApplication.ApplicationStatus;

        // assert
        Assert.That(calculatedApplicationStatus, Is.EqualTo(ApplicationComponentsStatus.InProgress));
    }

    [Test]
    public void ConversionApplication___ApplicationStatus___Completed___PropertyCheck()
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

        conversionApplication.ConversionApplicationComponents.AddRange(new List<ConversionApplicationComponent>
        {
            new(name:"Contact details") {Id = 1, Status = ApplicationComponentsStatus.Completed},
            new(name:"Performance and safeguarding") {Id = 2, Status = ApplicationComponentsStatus.Completed},
            new(name:"Pupil numbers") {Id = 3, Status = ApplicationComponentsStatus.Completed},
            new(name:"Finances") {Id = 4, Status = ApplicationComponentsStatus.Completed},
            new(name:"Partnerships and affiliations") {Id = 5, Status = ApplicationComponentsStatus.Completed},
            new(name:"Religious education") {Id = 6, Status = ApplicationComponentsStatus.Completed},
            new(name:"Land and buildings") {Id = 7, Status = ApplicationComponentsStatus.Completed},
            new(name:"Local authority") {Id = 8, Status = ApplicationComponentsStatus.Completed}
        });

        // act
        var calculatedApplicationStatus = conversionApplication.ApplicationStatus;

        // assert
        Assert.That(calculatedApplicationStatus, Is.EqualTo(ApplicationComponentsStatus.Completed));
    }
}