using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class NewTrustTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		int applicationId = Fixture.Create<int>();
		string proposedTrustName = Fixture.Create<string>();

		var applicationComponent = new NewTrust(applicationId, proposedTrustName)
		{
			Id = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(applicationComponent.FormTrustProposedNameOfTrust, Is.EqualTo(proposedTrustName));
	}
}
