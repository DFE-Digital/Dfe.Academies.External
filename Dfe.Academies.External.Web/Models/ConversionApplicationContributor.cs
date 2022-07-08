using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string name, SchoolRoles role, string? otherRoleNotListed)
    {
        Name = name;
        Role = role;
        OtherRoleNotListed = otherRoleNotListed;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public SchoolRoles Role { get; set; }

    public string? OtherRoleNotListed { get; set; }

    // TODO:- other props, maybe UserId from auth / user email from auth?
}