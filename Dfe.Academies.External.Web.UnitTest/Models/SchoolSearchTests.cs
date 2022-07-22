using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolSearchTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolSearch___PropertyCheck___Success()
	{
		// arrange
		string schoolName = Fixture.Create<string>();
		int urn = Fixture.Create<int>();

		var schoolSearch = new SchoolSearch(schoolName, urn.ToString());

		// act
		// nothing!

		// assert
		Assert.That(schoolSearch, Is.Not.Null);
		Assert.That(schoolSearch.SchoolName, Is.EqualTo(schoolName));
		Assert.That(schoolSearch.Urn, Is.EqualTo(urn.ToString()));
	}
}