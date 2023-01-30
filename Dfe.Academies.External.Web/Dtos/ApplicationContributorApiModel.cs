namespace Dfe.Academies.External.Web.Dtos;

public record ApplicationContributorApiModel(string FirstName,
	string LastName, string EmailAddress, string Role, string? OtherRoleName);
