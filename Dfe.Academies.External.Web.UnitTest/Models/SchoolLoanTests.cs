using AutoFixture;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolLoanTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		int id = Fixture.Create<int>();
		decimal amount = Fixture.Create<decimal>();
		string purpose = Fixture.Create<string>();
		string provider = Fixture.Create<string>();
		decimal interestRate = Fixture.Create<decimal>();
		string schedule = Fixture.Create<string>();

		var applicationComponent = new SchoolLoan(id, amount, purpose, provider, interestRate, schedule)
		{
			LoanId = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.LoanId, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.Amount, Is.EqualTo(amount));
		Assert.That(applicationComponent.Purpose, Is.EqualTo(purpose));
		Assert.That(applicationComponent.Provider, Is.EqualTo(provider));
		Assert.That(applicationComponent.InterestRate, Is.EqualTo(interestRate));
		Assert.That(applicationComponent.Schedule, Is.EqualTo(schedule));
	}
}
