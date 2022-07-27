using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class TrustSearchDto
{
	[JsonPropertyName("ukprn")]
	public string UkPrn { get; }

	[JsonPropertyName("urn")]
	public string Urn { get; }

	[JsonPropertyName("groupName")]
	public string GroupName { get; }

	[JsonPropertyName("companiesHouseNumber")]
	public string CompaniesHouseNumber { get; }

	[JsonPropertyName("trustType")]
	public string TrustType { get; }

	[JsonPropertyName("trustAddress")]
	public GroupContactAddressDto GroupContactAddress { get; }

	[JsonPropertyName("establishments")]
	public List<EstablishmentSummaryDto> Establishments { get; }

	//[JsonConstructor]
	//public TrustSearchDto(string ukprn, string urn, string groupName,
	//	string companiesHouseNumber, string trustType, GroupContactAddressDto groupContactAddress,
	//	List<EstablishmentSummaryDto> establishments) =>
	//	(UkPrn, Urn, GroupName, CompaniesHouseNumber, TrustType, GroupContactAddress, Establishments) =
	//	(ukprn, urn, groupName, companiesHouseNumber, trustType, groupContactAddress, establishments);
}
