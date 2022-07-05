using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplication
{
    public ConversionApplication()
    {
        SchoolOrSchoolsApplyingToConvert = new();
        ConversionApplicationComponents = new();
    }

    public long Id { get; set; }

    public ApplicationTypes ApplicationType { get; set; }

    public string? UserEmail { get; set; }
    public string? Application { get; set; }
    public string? TrustName { get; set; }

    public List<SchoolOrSchoolsApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; set; }

    public List<ConversionApplicationComponent> ConversionApplicationComponents { get; set; }

    public List<ConversionApplicationContributor> ConversionApplicationContributors { get; set; }
}