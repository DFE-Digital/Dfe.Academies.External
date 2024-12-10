using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web.Dtos;
public class ConversionApplication
{
	public int ApplicationId { get; set; }

	/// <summary>
	/// e.g. 'A2B_xxx'
	/// </summary>
	public string ApplicationReference { get; set; }

	public ApplicationTypes ApplicationType { get; set; }

	public ApplicationStatus ApplicationStatus { get; set; }

	public string? UserEmail { get; set; }
	
	public Guid EntityId { get; set; }

	/// <summary>
	/// In format:- "Form a new single academy trust A2B_2"
	/// </summary>
	public string ApplicationTitle
	{
		get
		{
			return $"{ApplicationType.GetDescription()} {ApplicationReference}";
		}
	}

	public List<SchoolApplyingToConvert> Schools { get; set; } = new();

	public List<ConversionApplicationContributor> Contributors { get; set; } = new();

	public int ConversionStatus { get; set; }

	public NewTrust? FormTrustDetails { get; set; }

	public ExistingTrust? JoinTrustDetails { get; set; }

	public DateTime? DeletedAt { get; set; }
	public DateTime? CreatedOn { get; set; }

	public string TrustName => (ApplicationType == ApplicationTypes.JoinAMat
		? JoinTrustDetails?.TrustName
		: FormTrustDetails?.FormTrustProposedNameOfTrust) ?? string.Empty;
		
}
