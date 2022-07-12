using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;
public class ConversionApplication
{
    public int Id { get; set; }

    /// <summary>
    /// e.g. 'A2B_xxx'
    /// </summary>
    public string ApplicationReference { get; set; }

    public ApplicationTypes ApplicationType { get; set; }

    public string? UserEmail { get; set; }

    public string? Application { get; set; }

    public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; set; } = new();
    
    public List<ConversionApplicationContributor> ConversionApplicationContributors { get; set; } = new();

    public SchoolRoles? SchoolRole { get; set; }

    public string? OtherRoleNotListed { get; set; }

    public int ConversionStatus { get; set; }

    public NewTrust? FormATrust { get; set; }

    public ExistingTrust? ExistingTrust { get; set; }

    public string TrustName => (ApplicationType == ApplicationTypes.JoinMat
        ? ExistingTrust?.TrustName
        : FormATrust?.ProposedTrustName) ?? string.Empty;

    public bool? ChangesToLocalAuthorityGovernance { get; set; }

    public string? ChangesToLocalAuthorityGovernanceExplained { get; set; }

    // TODO MR:- contact main contact

    //public Status ConversionStatusCalculated
    //{
    //    get
    //    {
    //        if (ConversionApplicationComponents.Count == 0)
    //        {
    //            return Status.CannotStartYet;
    //        }
    //        else
    //        {
    //            // all components = 'Not Started' so overall status = 'Not Started'
    //            if (ConversionApplicationComponents.Count == ConversionApplicationComponents.Count(c => c.Status == Status.NotStarted))
    //            {
    //                return Status.NotStarted;
    //            }
    //            else
    //            {
    //                // check component statuses to work out whether application 'InProgress' OR 'Completed'
    //                return ConversionApplicationComponents.Any(c => c.Status != Status.Completed) 
    //                    ? Status.InProgress : Status.Completed;
    //            }
    //        }
    //    }
    //}
}