using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolsSearchDtoTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolsSearchDto___PropertyCheck___Success()
	{
		// arrange
		string schoolName = Fixture.Create<string>();
		int urn = Fixture.Create<int>();
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();

		var schoolSearch = new SchoolsSearchDto(schoolName, urn, street, town, fullUkPostcode);

		// act
		// nothing!

		// assert
		Assert.That(schoolSearch, Is.Not.Null);
		Assert.That(schoolSearch.SchoolName, Is.EqualTo(schoolName));
		Assert.That(schoolSearch.Urn, Is.EqualTo(urn));
		Assert.That(schoolSearch.Street, Is.EqualTo(street));
		Assert.That(schoolSearch.Town, Is.EqualTo(town));
		Assert.That(schoolSearch.FullUkPostcode, Is.EqualTo(fullUkPostcode));
	}
}