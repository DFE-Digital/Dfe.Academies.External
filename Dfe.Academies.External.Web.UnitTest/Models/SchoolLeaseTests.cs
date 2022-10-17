using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolLeaseTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		int id = Fixture.Create<int>();
		int leaseTerm = Fixture.Create<int>();
		decimal repaymentAmount = Fixture.Create<decimal>(); 
		decimal interestRate = Fixture.Create<decimal>();
		decimal paymentsToDate = Fixture.Create<decimal>();
		string purpose = Fixture.Create<string>();
		string valueOfAssets = Fixture.Create<string>();
		string responsibleForAssets = Fixture.Create<string>();
		
		var applicationComponent = new SchoolLease(id, leaseTerm, repaymentAmount, interestRate, paymentsToDate, purpose, valueOfAssets, responsibleForAssets)
		{
			LeaseId = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.LeaseId, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.LeaseTerm, Is.EqualTo(leaseTerm));
		Assert.That(applicationComponent.RepaymentAmount, Is.EqualTo(repaymentAmount));
		Assert.That(applicationComponent.InterestRate, Is.EqualTo(interestRate));
		Assert.That(applicationComponent.PaymentsToDate, Is.EqualTo(paymentsToDate));
		Assert.That(applicationComponent.Purpose, Is.EqualTo(purpose));
		Assert.That(applicationComponent.ValueOfAssets, Is.EqualTo(valueOfAssets));
	}
}
