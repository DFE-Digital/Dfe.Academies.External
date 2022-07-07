using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;
public class ConversionApplication
{
    public ConversionApplication()
    {
        SchoolOrSchoolsApplyingToConvert = new();
        SchoolOrSchoolsApplyingToConvert = new();
        ConversionApplicationComponents = new();
        ConversionApplicationContributors = new();
    }

    public long Id { get; set; }

    public ApplicationTypes ApplicationType { get; set; }

    public string? UserEmail { get; set; }
    public string? Application { get; set; }
    public string? TrustName { get; set; }

    public List<SchoolOrSchoolsApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; set; }

    public List<ConversionApplicationComponent> ConversionApplicationComponents { get; set; }

    public List<ConversionApplicationContributor> ConversionApplicationContributors { get; set; }

    public SchoolRoles? SchoolRole { get; set; }

    public string? OtherRoleNotListed { get; set; }

    public Status ApplicationStatus {
        get
        {
            if (ConversionApplicationComponents.Count == 0)
            {
                return Status.NotStarted;
            }
            else
            {
                // all components = 'Not Started' so overall status = 'Not Started'
                if (ConversionApplicationComponents.Count == ConversionApplicationComponents.Count(c => c.Status == Status.NotStarted))
                {
                    return Status.NotStarted;
                }
                else
                {
                    // check component statuses to work out whether application 'InProgress' OR 'Completed'
                    return ConversionApplicationComponents.Any(c => c.Status != Status.Completed) 
                        ? Status.InProgress : Status.Completed;
                }
            }
        }
    }
}