using AutoFixture;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class ExistingTrustTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet___MandatoryOnly()
	{
		// arrange
		int applicationId = 99;
		string trustName = Fixture.Create<string>();
		int ukprn = Fixture.Create<int>();

		var applicationComponent = new ExistingTrust(applicationId, trustName, ukprn);

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(applicationComponent.TrustName, Is.EqualTo(trustName));
		Assert.That(applicationComponent.ukprn, Is.EqualTo(ukprn));
	}

	[Test]
	public void Constructor___PropertiesSet___All()
	{
		// arrange
		int applicationId = 99;
		string trustName = Fixture.Create<string>();
		int ukprn = Fixture.Create<int>();
		TrustChange? changesToTrust = Fixture.Create<TrustChange>();
		string? changesToTrustExplained = null;
		bool? changesToLaGovernance = Fixture.Create<bool>();
		string? changesToLaGovernanceExplained = null;

		var applicationComponent = new ExistingTrust(applicationId, trustName, ukprn, 
						changesToTrust, changesToTrustExplained, changesToLaGovernance, changesToLaGovernanceExplained);

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(applicationComponent.TrustName, Is.EqualTo(trustName));
		Assert.That(applicationComponent.ukprn, Is.EqualTo(ukprn));
		Assert.That(applicationComponent.ChangesToTrust, Is.EqualTo(changesToTrust));
		Assert.That(applicationComponent.ChangesToTrustExplained, Is.EqualTo(changesToTrustExplained));
		Assert.That(applicationComponent.ChangesToLaGovernance, Is.EqualTo(changesToLaGovernance));
		Assert.That(applicationComponent.ChangesToLaGovernanceExplained, Is.EqualTo(changesToLaGovernanceExplained));
	}
}
