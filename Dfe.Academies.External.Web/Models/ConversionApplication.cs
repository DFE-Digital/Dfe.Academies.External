using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;
public class ConversionApplication
{
    public int ApplicationId { get; set; }

    /// <summary>
    /// e.g. 'A2B_xxx'
    /// </summary>
    public string ApplicationReference => $"A2B_{ApplicationId}";

    public ApplicationTypes ApplicationType { get; set; }

	// TODO MR:- convert this to an enum ????
    public string ApplicationStatus { get; set; }

	public string? UserEmail { get; set; }

    public string? Application { get; set; }

    public List<SchoolApplyingToConvert> Schools { get; set; } = new();
    
    public List<ConversionApplicationContributor> Contributors { get; set; } = new();

    public int ConversionStatus { get; set; }

    public NewTrust? FormATrust { get; set; }

    public ExistingTrust? ExistingTrust { get; set; }

    public string TrustName => (ApplicationType == ApplicationTypes.JoinMat
        ? ExistingTrust?.TrustName
        : FormATrust?.ProposedTrustName) ?? string.Empty;
}
