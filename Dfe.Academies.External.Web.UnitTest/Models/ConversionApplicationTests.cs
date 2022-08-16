using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationTests
{
    [Test]
    public void Constructor___PropertiesSet()
    {
        // arrange
        var conversionApplication = new ConversionApplication
        {
            ApplicationId = int.MaxValue,
            ApplicationType = ApplicationTypes.FormNewMat,
            UserEmail = "mark.robinson@education.gov.uk",
            Application = "test",
            ConversionStatus = 1
        };

        // act
        // nothing!

        // assert
        Assert.That(conversionApplication, Is.Not.Null);
        Assert.That(conversionApplication.ApplicationId, Is.EqualTo(int.MaxValue));
        Assert.That(conversionApplication.ApplicationType, Is.EqualTo(ApplicationTypes.FormNewMat));
        Assert.That(conversionApplication.UserEmail, Is.EqualTo("mark.robinson@education.gov.uk"));
        Assert.That(conversionApplication.Application, Is.EqualTo("test"));
        Assert.That(conversionApplication.ConversionStatus, Is.EqualTo(1));
        Assert.That(conversionApplication.Schools.Count, Is.EqualTo(0));
        Assert.That(conversionApplication.Contributors.Count, Is.EqualTo(0));
    }

    //[Test]
    //public void ConversionApplication___ApplicationStatus___NotStarted___PropertyCheck()
    //{
    //    // arrange
    //    var conversionApplication = new ConversionApplication
    //    {
    //        ApplicationId = int.MaxValue,
    //        ApplicationType = ApplicationTypes.FormNewMat,
    //        UserEmail = "mark.robinson@education.gov.uk",
    //        Application = "test",
    //        TrustName = "Pudsey School"
    //    };

    //    // act
    //    var calculatedApplicationStatus = conversionApplication.ApplicationStatusCalculated;

    //    // assert
    //    Assert.That(calculatedApplicationStatus, Is.EqualTo(Status.NotStarted));
    //}

    //[Test]
    //public void ConversionApplication___ApplicationStatus___NotStarted2___PropertyCheck()
    //{
    //    // arrange
    //    var conversionApplication = new ConversionApplication
    //    {
    //        ApplicationId = int.MaxValue,
    //        ApplicationType = ApplicationTypes.FormNewMat,
    //        UserEmail = "mark.robinson@education.gov.uk",
    //        Application = "test",
    //        TrustName = "Pudsey School"
    //    };

    //    conversionApplication.SchoolApplicationComponents.AddRange(new List<ConversionApplicationComponent>
    //    {
    //        new(name:"Contact details") {ApplicationId = 1, Status = Status.NotStarted},
    //        new(name:"Performance and safeguarding") {ApplicationId = 2, Status = Status.NotStarted},
    //        new(name:"Pupil numbers") {ApplicationId = 3, Status = Status.NotStarted},
    //        new(name:"Finances") {ApplicationId = 4, Status = Status.NotStarted},
    //        new(name:"Partnerships and affiliations") {ApplicationId = 5, Status = Status.NotStarted},
    //        new(name:"Religious education") {ApplicationId = 6, Status = Status.NotStarted},
    //        new(name:"Land and buildings") {ApplicationId = 7, Status = Status.NotStarted},
    //        new(name:"Local authority") {ApplicationId = 8, Status = Status.NotStarted}
    //    });

    //    // act
    //    var calculatedApplicationStatus = conversionApplication.ApplicationStatusCalculated;

    //    // assert
    //    Assert.That(calculatedApplicationStatus, Is.EqualTo(Status.NotStarted));
    //}

    //[Test]
    //public void ConversionApplication___ApplicationStatus___InProgress___PropertyCheck()
    //{
    //    // arrange
    //    var conversionApplication = new ConversionApplication
    //    {
    //        ApplicationId = int.MaxValue,
    //        ApplicationType = ApplicationTypes.FormNewMat,
    //        UserEmail = "mark.robinson@education.gov.uk",
    //        Application = "test",
    //        TrustName = "Pudsey School"
    //    };

    //    conversionApplication.SchoolApplicationComponents.AddRange(new List<ConversionApplicationComponent>
    //    {
    //        new(name:"Contact details") {ApplicationId = 1, Status = Status.Completed},
    //        new(name:"Performance and safeguarding") {ApplicationId = 2, Status = Status.InProgress},
    //        new(name:"Pupil numbers") {ApplicationId = 3, Status = Status.NotStarted},
    //        new(name:"Finances") {ApplicationId = 4, Status = Status.NotStarted},
    //        new(name:"Partnerships and affiliations") {ApplicationId = 5, Status = Status.NotStarted},
    //        new(name:"Religious education") {ApplicationId = 6, Status = Status.NotStarted},
    //        new(name:"Land and buildings") {ApplicationId = 7, Status = Status.NotStarted},
    //        new(name:"Local authority") {ApplicationId = 8, Status = Status.NotStarted}
    //    });

    //    // act
    //    var calculatedApplicationStatus = conversionApplication.ApplicationStatusCalculated;

    //    // assert
    //    Assert.That(calculatedApplicationStatus, Is.EqualTo(Status.InProgress));
    //}

    //[Test]
    //public void ConversionApplication___ApplicationStatus___Completed___PropertyCheck()
    //{
    //    // arrange
    //    var conversionApplication = new ConversionApplication
    //    {
    //        ApplicationId = int.MaxValue,
    //        ApplicationType = ApplicationTypes.FormNewMat,
    //        UserEmail = "mark.robinson@education.gov.uk",
    //        Application = "test",
    //        TrustName = "Pudsey School"
    //    };

    //    conversionApplication.SchoolApplicationComponents.AddRange(new List<ConversionApplicationComponent>
    //    {
    //        new(name:"Contact details") {ApplicationId = 1, Status = Status.Completed},
    //        new(name:"Performance and safeguarding") {ApplicationId = 2, Status = Status.Completed},
    //        new(name:"Pupil numbers") {ApplicationId = 3, Status = Status.Completed},
    //        new(name:"Finances") {ApplicationId = 4, Status = Status.Completed},
    //        new(name:"Partnerships and affiliations") {ApplicationId = 5, Status = Status.Completed},
    //        new(name:"Religious education") {ApplicationId = 6, Status = Status.Completed},
    //        new(name:"Land and buildings") {ApplicationId = 7, Status = Status.Completed},
    //        new(name:"Local authority") {ApplicationId = 8, Status = Status.Completed}
    //    });

    //    // act
    //    var calculatedApplicationStatus = conversionApplication.ApplicationStatusCalculated;

    //    // assert
    //    Assert.That(calculatedApplicationStatus, Is.EqualTo(Status.Completed));
    //}
}