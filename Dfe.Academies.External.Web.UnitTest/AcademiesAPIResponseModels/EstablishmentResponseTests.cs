using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.AcademiesAPIResponseModels;

[Parallelizable(ParallelScope.All)]
internal sealed class EstablishmentResponseTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void EstablishmentResponse___PropertyCheck___Success()
	{
		// arrange
		int urn = Fixture.Create<int>();
		string schoolName = Fixture.Create<string>();
		string ukprn = Fixture.Create<string>();
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();
		
		var establishmentResponse = new EstablishmentResponse(schoolName, urn, ukprn, street, town, fullUkPostcode);

		// act
		// nothing!

		// assert
		Assert.That(establishmentResponse, Is.Not.Null);
		Assert.That(establishmentResponse.Name, Is.EqualTo(schoolName));
		Assert.That(establishmentResponse.Urn, Is.EqualTo(urn));
		Assert.That(establishmentResponse.Ukprn, Is.EqualTo(ukprn));
		Assert.That(establishmentResponse.Address.Street, Is.EqualTo(street));
		Assert.That(establishmentResponse.Address.Town, Is.EqualTo(town));
		Assert.That(establishmentResponse.Address.Postcode, Is.EqualTo(fullUkPostcode));
	}
}