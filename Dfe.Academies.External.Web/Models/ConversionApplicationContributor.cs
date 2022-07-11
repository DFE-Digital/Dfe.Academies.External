using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor : Person
{
    public ConversionApplicationContributor(string firstName, string surname, SchoolRoles role, string? otherRoleNotListed)
    {
        FirstName = firstName;
        Surname = surname;
        Role = role;
        OtherRoleNotListed = otherRoleNotListed;
    }

    public int Id { get; set; }

    public SchoolRoles Role { get; set; }

    public string? OtherRoleNotListed { get; set; }

    // TODO:- other props, maybe UserId from auth / user email from auth?
}