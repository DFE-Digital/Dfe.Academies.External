using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationContributorViewModelTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void ConversionApplicationContributorViewModel___PropertyCheck___Success()
    {
        // arrange
        string name = Fixture.Create<string>();

        var applicationAuditViewModel = new ConversionApplicationContributorViewModel
        {
            Name = name
        };

        // assert
        Assert.That(applicationAuditViewModel, Is.Not.Null);
        Assert.That(applicationAuditViewModel.Name, Is.EqualTo(name));
    }
}