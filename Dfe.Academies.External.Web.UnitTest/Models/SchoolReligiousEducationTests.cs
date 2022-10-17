using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolReligiousEducationTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string faithDioceseName = Fixture.Create<string>();

		var applicationComponent = new SchoolReligiousEducation(true,
			faithDioceseName, null, null, null);

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.FaithSchoolDioceseName, Is.EqualTo(faithDioceseName));
	}
}
