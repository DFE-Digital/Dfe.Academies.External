namespace Dfe.Academies.External.Web.Models;

/// <summary>
/// Object to represent data capture required when Forming a new MAT / FormNewSingleAcademyTrust
/// </summary>
public class NewTrust
{
	public NewTrust(string proposedTrustName)
	{
		ProposedTrustName = proposedTrustName;
	}

	public int Id { get; set; }

	// TODO MR:- what to do about trust id ????

	public string ProposedTrustName { get; set; }

	public DateTime? ProposedTrustOpeningDate { get; set; }

	//// MR:- below props from A2C-SIP - Application object
	public string? FormTrustReasonForming { get; set; }

	public string? FormTrustReasonVision { get; set; }

	public string? FormTrustReasonGeoAreas { get; set; }

	public string? FormTrustReasonFreedom { get; set; }

	public string? FormTrustReasonImproveTeaching { get; set; }

	public string? FormTrustReasonPlanForGrowth { get; set; }

	public string? FormTrustReasonPlanForNoGrowth { get; set; }

	public bool? FormTrustGrowthPlansYesNo { get; set; }

	public string? FormTrustImprovementSupport { get; set; }

	public string? FormTrustImprovementStrategy { get; set; }

	public List<NewTrustKeyPerson> NewTrustKeyPersons { get; set; } = new();
}