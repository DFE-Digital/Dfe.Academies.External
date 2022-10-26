namespace Dfe.Academies.External.Web.Models;

/// <summary>
/// Object to represent data capture required when Forming a new MAT / FormNewSingleAcademyTrust
/// </summary>
public class NewTrust
{
	// TODO MR:- amend this obj = record and to be same as following json
	// {
//"applicationId": 0,
//"formTrustOpeningDate": "2022-10-26T10:30:04.046Z",
//"formTrustProposedNameOfTrust": "string",
//"trustApproverName": "string",
//"trustApproverEmail": "string",
//"formTrustReasonApprovaltoConvertasSAT": true,
//"formTrustReasonApprovedPerson": "string",
//"formTrustReasonForming": "string",
//"formTrustReasonVision": "string",
//"formTrustReasonGeoAreas": "string",
//"formTrustReasonFreedom": "string",
//"formTrustReasonImproveTeaching": "string",
//"formTrustPlanForGrowth": "string",
//"formTrustPlansForNoGrowth": "string",
//"formTrustGrowthPlansYesNo": true,
//"formTrustImprovementSupport": "string",
//"formTrustImprovementStrategy": "string",
//"formTrustImprovementApprovedSponsor": "string"
//}

public NewTrust(string proposedTrustName)
	{
		ProposedTrustName = proposedTrustName;
	}

	public int Id { get; set; }

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

	//public List<NewTrustKeyPerson> NewTrustKeyPersons { get; set; } = new();
}
