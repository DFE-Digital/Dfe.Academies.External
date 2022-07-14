using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolContactTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolContact___PropertyCheck___Success()
	{
		// arrange
		string firstName = Fixture.Create<string>();
		string surName = Fixture.Create<string>();
		
		var applicationComponent = new SchoolContact(int.MaxValue, firstName, surName)
		{
			Id = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.Person.FirstName, Is.EqualTo(firstName));
		Assert.That(applicationComponent.Person.Surname, Is.EqualTo(surName));
		Assert.That(applicationComponent.SchoolId, Is.EqualTo(int.MaxValue));
	}
}