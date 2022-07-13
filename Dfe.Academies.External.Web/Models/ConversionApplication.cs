using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;
public class ConversionApplication
{
    public int Id { get; set; }

    /// <summary>
    /// e.g. 'A2B_xxx'
    /// </summary>
    public string ApplicationReference => $"A2B_{Id}";

    public ApplicationTypes ApplicationType { get; set; }

    public string? UserEmail { get; set; }
    public string? Application { get; set; }
    public string? TrustName { get; set; }

    public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; set; } = new();

    public List<ConversionApplicationContributor> ConversionApplicationContributors { get; set; } = new();

    public SchoolRoles? SchoolRole { get; set; }

    public string? OtherRoleNotListed { get; set; }

    public int ConversionStatus { get; set; }
}