using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolLoanTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolLoan___PropertyCheck___Success()
	{
		// arrange
		string purpose = Fixture.Create<string>();
		string provider = Fixture.Create<string>();

		var applicationComponent = new SchoolLoan(purpose, provider)
		{
			Id = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.Purpose, Is.EqualTo(purpose));
		Assert.That(applicationComponent.Provider, Is.EqualTo(provider));
	}
}