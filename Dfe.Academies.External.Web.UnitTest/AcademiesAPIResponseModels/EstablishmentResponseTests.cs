using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.AcademiesAPIResponseModels;

[Parallelizable(ParallelScope.All)]
internal sealed class EstablishmentResponseTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void EstablishmentResponse___Constructor______PropertiesSet()
	{
		// arrange
		string urn = Fixture.Create<string>();
		string establishmentNumber = Fixture.Create<string>();
		string schoolName = Fixture.Create<string>();
		string ukprn = Fixture.Create<string>();
		string uprn = Fixture.Create<string>();
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();
		
		AddressResponse address = new (street, town, fullUkPostcode);

		var establishmentResponse = new EstablishmentResponse(urn,establishmentNumber, schoolName, ukprn, uprn, address);

		// act
		// nothing!

		// assert
		Assert.That(establishmentResponse, Is.Not.Null);
		Assert.That(establishmentResponse.Urn, Is.EqualTo(urn));
		Assert.That(establishmentResponse.EstablishmentNumber, Is.EqualTo(establishmentNumber));
		Assert.That(establishmentResponse.EstablishmentName, Is.EqualTo(schoolName));
		Assert.That(establishmentResponse.Address.Street, Is.EqualTo(street));
		Assert.That(establishmentResponse.Address.Town, Is.EqualTo(town));
		Assert.That(establishmentResponse.Address.Postcode, Is.EqualTo(fullUkPostcode));
		Assert.That(establishmentResponse.Ukprn, Is.EqualTo(ukprn));
		Assert.That(establishmentResponse.UPRN, Is.EqualTo(uprn));
	}
}