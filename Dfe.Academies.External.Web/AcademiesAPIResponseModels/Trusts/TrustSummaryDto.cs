using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

/// <summary>
/// To de-serialize response from trust search && GetTrustByUkPrn :-
/// {{api-host}}/trusts?ukprn=10058464&api-version=V1
/// </summary>
public record TrustSummaryDto
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
	public string Name { get; set; }

	[JsonPropertyName("companiesHouseNumber")]
	public string CompaniesHouseNumber { get; set; }

	[JsonPropertyName("trustType")]
	public string TypeDescription { get; set; }

	[JsonPropertyName("trustAddress")]
	public AddressResponse TrustAddress { get; set; }
}