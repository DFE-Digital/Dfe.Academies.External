using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class EstablishmentDto
{
	[JsonPropertyName("urn")]
	public string Urn { get; }

	[JsonPropertyName("establishmentNumber")]
	public string EstablishmentNumber { get; }

	[JsonPropertyName("establishmentName")]
	public string EstablishmentName { get; }

	[JsonPropertyName("establishmentType")]
	public EstablishmentTypeDto EstablishmentType { get; }

	[JsonConstructor]
	public EstablishmentDto(string urn, string establishmentNumber, string establishmentName, EstablishmentTypeDto establishmentType) =>
		(Urn, EstablishmentNumber, EstablishmentName, EstablishmentType) =
		(urn, establishmentNumber, establishmentName, establishmentType);
}
