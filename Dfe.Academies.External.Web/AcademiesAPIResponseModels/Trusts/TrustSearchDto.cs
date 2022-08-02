using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class TrustSearchDto
{
	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	[JsonPropertyName("ukprn")]
	public string UkPrn { get; set; }

	/// <summary>
	/// Unique identifier for a school.
	/// </summary>
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
