using FluentValidation;

namespace Dfe.Academies.External.Web.Validators;

public sealed class EmailValidator : AbstractValidator<EmailAddress>
{
	public EmailValidator()
	{
		RuleFor(x => x.Email).EmailAddress();
	}
}

public record EmailAddress(string Email);
