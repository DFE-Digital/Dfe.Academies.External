namespace Dfe.Academies.External.Web.Models;

public record ApplicationContributorApiModel(string FirstName,
	string LastName, string EmailAddress, string Role, string? OtherRoleName);
