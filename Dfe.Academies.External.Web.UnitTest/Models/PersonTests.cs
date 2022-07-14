using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class PersonTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Person___PropertyCheck___Success()
	{
		// arrange
		string firstName = Fixture.Create<string>();
		string surName = Fixture.Create<string>();

		var applicationComponent = new Person(firstName, surName);

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.FirstName, Is.EqualTo(firstName));
		Assert.That(applicationComponent.Surname, Is.EqualTo(surName));
	}
}