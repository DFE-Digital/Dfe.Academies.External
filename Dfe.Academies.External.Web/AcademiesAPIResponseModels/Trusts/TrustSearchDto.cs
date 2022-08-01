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
}
