using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class TrustSearchDto
{
	[JsonPropertyName("ukprn")]
	public string UkPrn { get; set; }

	[JsonPropertyName("urn")]
	public string Urn { get; set; }

	[JsonPropertyName("groupName")]
	public string GroupName { get; set; }

	[JsonPropertyName("companiesHouseNumber")]
	public string CompaniesHouseNumber { get; set; }

	[JsonPropertyName("trustType")]
	public string TrustType { get; set; }

	[JsonPropertyName("trustAddress")]
	public GroupContactAddressDto GroupContactAddress { get; set; }

	[JsonPropertyName("establishments")]
	public List<EstablishmentSummaryDto> Establishments { get; set; }

	//[JsonConstructor]
	//public TrustSearchDto(string ukprn, string urn, string groupName,
	//	string companiesHouseNumber, string trustType, GroupContactAddressDto groupContactAddress,
	//	List<EstablishmentSummaryDto> establishments) =>
	//	(UkPrn, Urn, GroupName, CompaniesHouseNumber, TrustType, GroupContactAddress, Establishments) =
	//	(ukprn, urn, groupName, companiesHouseNumber, trustType, groupContactAddress, establishments);
}
