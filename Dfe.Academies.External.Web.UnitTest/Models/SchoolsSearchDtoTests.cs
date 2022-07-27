using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
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
		string urn = Fixture.Create<int>().ToString();

		var schoolSearch = new SchoolsSearchDto(schoolName, urn,string.Empty);

		// act
		// nothing!

		// assert
		Assert.That(schoolSearch, Is.Not.Null);
		Assert.That(schoolSearch.Name, Is.EqualTo(schoolName));
		Assert.That(schoolSearch.Urn, Is.EqualTo(urn));
	}
}