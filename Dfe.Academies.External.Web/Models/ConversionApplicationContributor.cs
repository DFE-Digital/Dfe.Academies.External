using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string firstName, string surname, SchoolRoles role, string? otherRoleName)
    {
	    FirstName = firstName;
        Surname = surname;
		Role = role;
        OtherRoleName = otherRoleName;
    }
    public int ContributorId { get; set; }

    public int ApplicationId { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public string? EmailAddress { get; set; }

	public SchoolRoles Role { get; set; }

    public string? OtherRoleName { get; set; }
}