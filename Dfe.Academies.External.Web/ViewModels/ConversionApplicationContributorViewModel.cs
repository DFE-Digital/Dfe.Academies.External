using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public class ConversionApplicationContributorViewModel
{
    public string Name { get; set; }

    public SchoolRoles Role { get; set; }

    public string? OtherRoleNotListed { get; set; }
}