using System.Threading.Tasks;
using AutoFixture;
using Bogus;
using Dfe.Academies.External.Web.Validators;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Validators;

[Parallelizable(ParallelScope.All)]
internal sealed class EmailValidatorTests
{
	private static readonly Fixture Fixture = new();
	private static readonly Faker Faker = new();
	
	[Test]
	public async Task EmailValidator___ValidEmail___IsValidTrue()
	{
		// arrange
		var emailAddress = new EmailAddress(Faker.Internet.Email());

		var emailValidator = new EmailValidator();

		// act
		var validationResult = await emailValidator.ValidateAsync(emailAddress);

		// assert
		Assert.That(validationResult.IsValid, Is.True);
	}

	[Test]
	public async Task EmailValidator___InValidEmail___IsValidFalse()
	{
		// arrange
		var emailAddress = new EmailAddress(Fixture.Create<string>());

		var emailValidator = new EmailValidator();

		// act
		var validationResult = await emailValidator.ValidateAsync(emailAddress);

		// assert
		Assert.That(validationResult.IsValid, Is.False);
	}
}
