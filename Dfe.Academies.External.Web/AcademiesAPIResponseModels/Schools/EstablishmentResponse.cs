using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

public class EstablishmentResponse
{

	[JsonPropertyName("urn")]
	public string Urn { get; set; }

	[JsonPropertyName("establishmentNumber")]
	public string Number { get; set; }

	[JsonPropertyName("establishmentName")]
	public string Name { get; set; }

	public AddressResponse Address { get; set; }

	[JsonPropertyName("uprn")]
	public string UPRN { get; set; }
}
