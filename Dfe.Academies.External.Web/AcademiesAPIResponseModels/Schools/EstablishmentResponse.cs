using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

public class EstablishmentResponse
{
	//public EstablishmentResponse()
	//{
	//	Address = new AddressResponse();
	//}

	[JsonPropertyName("urn")]
	public string Urn { get; set; }

	[JsonPropertyName("establishmentNumber")]
	public string Number { get; set; }

	[JsonPropertyName("establishmentName")]
	public string Name { get; set; }

	[JsonInclude]
	public AddressResponse Address { get; set; }

	[JsonPropertyName("uprn")]
	public string UPRN { get; set; }

	[JsonPropertyName("ukprn")]
	public string Ukprn { get; set; }
}
