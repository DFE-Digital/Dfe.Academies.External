namespace Dfe.Academies.External.Web.Dtos;

public class PartnershipsAndAffliations
{
	public bool? IsPartOfFederation { get; set; }
	public bool? IsSupportedByFoundation { get; set; }
	public string? SupportedFoundationName { get; set; }
	public string? SupportedFoundationEvidenceDocumentLink { get; set; }
	public string? FeederSchools { get; set; }
}