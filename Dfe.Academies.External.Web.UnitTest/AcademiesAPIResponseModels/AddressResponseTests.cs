using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.AcademiesAPIResponseModels;

[Parallelizable(ParallelScope.All)]
internal sealed class AddressResponseTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void AddressResponse___PropertyCheck___Success()
	{
		// arrange
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();
		string additionalLine = Fixture.Create<string>();
		string county = Fixture.Create<string>();
		string locality = Fixture.Create<string>();

		var addressResponse = new AddressResponse(street, town, fullUkPostcode)
		{
			AdditionalLine = additionalLine,
			County = county,
			Locality = locality
		};

		// act
		// nothing!

		// assert
		Assert.That(addressResponse, Is.Not.Null);
		Assert.That(addressResponse.Street, Is.EqualTo(street));
		Assert.That(addressResponse.Town, Is.EqualTo(town));
		Assert.That(addressResponse.Postcode, Is.EqualTo(fullUkPostcode));
		Assert.That(addressResponse.AdditionalLine, Is.EqualTo(additionalLine));
		Assert.That(addressResponse.County, Is.EqualTo(county));
		Assert.That(addressResponse.Locality, Is.EqualTo(locality));
	}
}