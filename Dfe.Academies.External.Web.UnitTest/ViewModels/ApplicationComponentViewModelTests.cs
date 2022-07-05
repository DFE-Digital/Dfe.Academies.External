using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationComponentViewModelTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void ApplicationComponentViewModel___PropertyCheck___Success()
    {
        // arrange
        ApplicationComponentsStatus applicationComponentStatus = ApplicationComponentsStatus.InProgress;
        string name = Fixture.Create<string>();
        string URI = Fixture.Create<string>();
        
        var applicationAuditViewModel = new ApplicationComponentViewModel
        {
            ApplicationComponentStatus = applicationComponentStatus,
            Name = name,
            URI = URI
        };

        // assert
        Assert.That(applicationAuditViewModel, Is.Not.Null);
        Assert.That(applicationAuditViewModel.ApplicationComponentStatus, Is.EqualTo(applicationComponentStatus));
        Assert.That(applicationAuditViewModel.Name, Is.EqualTo(name));
        Assert.That(applicationAuditViewModel.URI, Is.EqualTo(URI));
    }
}