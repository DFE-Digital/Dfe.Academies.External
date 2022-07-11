using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;
public class ConversionApplication
{
    public int Id { get; set; }

    public ApplicationTypes ApplicationType { get; set; }

    public string? UserEmail { get; set; }
    public string? Application { get; set; }
    public string? TrustName { get; set; }

    public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; set; } = new();

    public List<ConversionApplicationComponent> ConversionApplicationComponents { get; set; } = new();

    public List<ConversionApplicationContributor> ConversionApplicationContributors { get; set; } = new();

    public SchoolRoles? SchoolRole { get; set; }

    public string? OtherRoleNotListed { get; set; }

    public int ConversionStatus { get; set; }

    public Status ConversionStatusCalculated {
        get
        {
            if (ConversionApplicationComponents.Count == 0)
            {
                return Status.CannotStartYet;
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