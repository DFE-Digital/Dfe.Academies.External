using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class ExistingTrustTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void ExistingTrust___PropertyCheck___Success()
	{
		// arrange
		string trustName = Fixture.Create<string>();

		var applicationComponent = new ExistingTrust(trustName)
		{
			Id = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.TrustName, Is.EqualTo(trustName));
	}
}