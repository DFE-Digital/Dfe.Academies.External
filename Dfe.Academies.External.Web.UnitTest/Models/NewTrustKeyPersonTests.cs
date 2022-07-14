using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class NewTrustKeyPersonTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void NewTrustKeyPerson___PropertyCheck___Success()
	{
		// arrange
		string firstName = Fixture.Create<string>();
		string surName = Fixture.Create<string>();
		KeyPersonRole role = KeyPersonRole.Chair;

		var applicationComponent = new NewTrustKeyPerson(firstName, surName, role)
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
		Assert.That(applicationComponent.Role, Is.EqualTo(role));
	}
}