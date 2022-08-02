using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class EstablishmentSummaryDto
{
	[JsonPropertyName("urn")]
	public string Urn { get; }

	[JsonPropertyName("name")]
	public string Name { get; }

	[JsonPropertyName("ukprn")]
	public string UkPrn { get; }

	[JsonConstructor]
	public EstablishmentSummaryDto(string urn, string name, string ukprn) =>
		(Urn, Name, UkPrn) = (urn, name, ukprn);
}
