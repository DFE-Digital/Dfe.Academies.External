using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
	public ConversionApplicationContributor(string firstName, string lastName, string emailAddress, SchoolRoles role, string? otherRoleName)
	{
		FirstName = firstName;
		LastName = lastName;
		EmailAddress = emailAddress;
		Role = role;
		OtherRoleName = otherRoleName;
	}
	public int ContributorId { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string EmailAddress { get; set; }

	public SchoolRoles Role { get; set; }

	public string? OtherRoleName { get; set; }

	public string FullName
	{
		get
		{
			return $"{FirstName} {LastName}";
		}
	}
}
