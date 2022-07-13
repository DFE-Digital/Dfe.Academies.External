using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolFinancesTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolFinances___PropertyCheck___Success()
	{
		// arrange
		string proposedTrustName = Fixture.Create<string>();
		decimal capitalCarryForwardAtEndMarchCurrentYear = new decimal(90000);

		var applicationComponent = new SchoolFinances
		{
			Id = int.MaxValue,
			CapitalCarryForwardAtEndMarchCurrentYear = capitalCarryForwardAtEndMarchCurrentYear
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.CapitalCarryForwardAtEndMarchCurrentYear, Is.EqualTo(capitalCarryForwardAtEndMarchCurrentYear));
	}
}