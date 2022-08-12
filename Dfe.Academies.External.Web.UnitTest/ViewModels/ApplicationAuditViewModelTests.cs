using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;
using System;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ApplicationAuditViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string what = Fixture.Create<string>();
		DateTime when = Fixture.Create<DateTime>();
		string who = Fixture.Create<string>();


		var conversionApplicationAuditEntry = new ApplicationAuditViewModel(who, what)
		{
			When = when
		};

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.What, Is.EqualTo(what));
		Assert.That(conversionApplicationAuditEntry.When, Is.EqualTo(when));
		Assert.That(conversionApplicationAuditEntry.Who, Is.EqualTo(who));
	}
}