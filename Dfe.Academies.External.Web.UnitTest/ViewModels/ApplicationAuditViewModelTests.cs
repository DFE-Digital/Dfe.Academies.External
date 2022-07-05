using System;
using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationAuditViewModelTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void ApplicationAuditViewModel___PropertyCheck___Success()
    {
        // arrange
        string what = Fixture.Create<string>();
        string who = Fixture.Create<string>();
        DateTime when = Fixture.Create<DateTime>();

        var applicationAuditViewModel = new ApplicationAuditViewModel
        {
            What = what,
            Who = who,
            When = when
        };

        // assert
        Assert.That(applicationAuditViewModel, Is.Not.Null);
        Assert.That(applicationAuditViewModel.What, Is.EqualTo(what));
        Assert.That(applicationAuditViewModel.Who, Is.EqualTo(who));
        Assert.That(applicationAuditViewModel.When, Is.EqualTo(when));
    }
}