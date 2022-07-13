using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolReligiousEducationTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolReligiousEducation___PropertyCheck___Success()
	{
		// arrange
		string faithDioceseName = Fixture.Create<string>();

		var applicationComponent = new SchoolReligiousEducation(faithDioceseName)
		{
			Id = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.FaithDioceseName, Is.EqualTo(faithDioceseName));
	}
}