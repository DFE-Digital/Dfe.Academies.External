﻿namespace Dfe.Academies.External.Web.Dtos;

/// <summary>
/// Object to represent data capture required when Forming a new MAT / FormNewSingleAcademyTrust
/// </summary>
public sealed class NewTrust
{
	/// <summary>
	/// need empty ctor for JSON de-serialization !!
	/// </summary>
	public NewTrust()
	{
	}

	public NewTrust(int applicationId, string proposedTrustName, string applicationReference)
	{
		ApplicationId = applicationId;
		FormTrustProposedNameOfTrust = proposedTrustName;
		ApplicationReference = applicationReference;
	}

	/// <summary>
	/// generated by DB
	/// </summary>
	public int Id { get; set; }

	public int ApplicationId { get; set; }
	
	public string ApplicationReference { get; set; }

	public string FormTrustProposedNameOfTrust { get; set; }

	public DateTime? FormTrustOpeningDate { get; set; }
	
	public string? TrustApproverName { get; set; }
	public string? TrustApproverEmail { get; set; }
	public bool? FormTrustReasonApprovaltoConvertasSAT { get; set; }
	public string? FormTrustReasonApprovedPerson { get; set; }
	public string? FormTrustReasonForming { get; set; }
	public string? FormTrustReasonVision { get; set; }
	public string? FormTrustReasonGeoAreas { get; set; }
	public string? FormTrustReasonFreedom { get; set; }
	public string? FormTrustReasonImproveTeaching { get; set; }
	public string? FormTrustPlanForGrowth { get; set; }
	public string? FormTrustPlansForNoGrowth { get; set; }
	public bool? FormTrustGrowthPlansYesNo { get; set; }
	public string? FormTrustImprovementSupport { get; set; }
	public string? FormTrustImprovementStrategy { get; set; }
	public string? FormTrustImprovementApprovedSponsor { get; set; }

	public List<NewTrustKeyPerson> KeyPeople { get; set; } = new();
}