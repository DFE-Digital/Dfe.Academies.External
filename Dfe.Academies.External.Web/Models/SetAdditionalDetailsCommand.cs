using System.Runtime.Serialization;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class SetAdditionalDetailsCommand
{
	[DataMember]
	public int ApplicationId { get; set; }
	
	[DataMember]
	public int SchoolId { get; set; }
	
	[DataMember]
	public string TrustBenefitDetails  { get; set; }
	
	[DataMember]
	public string? OfstedInspectionDetails  { get; set; }
	
	[DataMember]
	public string? SafeguardingDetails  { get; set; }
	
	[DataMember]
	public string? LocalAuthorityReorganisationDetails  { get; set; }
	
	[DataMember]
	public string? LocalAuthorityClosurePlanDetails  { get; set; }
	
	[DataMember]
	public string? DioceseName  { get; set; }
	
	[DataMember]
	public string? DioceseFolderIdentifier  { get; set; }
	
	[DataMember]
	public bool PartOfFederation { get; set; }
	
	[DataMember]
	public string? FoundationTrustOrBodyName { get; set; }

	[DataMember]
	public string? FoundationConsentFolderIdentifier { get; set; }
	
	[DataMember]
	public DateTimeOffset? ExemptionEndDate { get; set; }
	
	[DataMember]
	public string MainFeederSchools { get; set; }
	
	[DataMember]
	public string ResolutionConsentFolderIdentifier { get; set; }

	[DataMember]
	public SchoolEqualitiesProtectedCharacteristics? ProtectedCharacteristics { get; set; }
	
	[DataMember]
	public string? FurtherInformation { get; set; }
}
