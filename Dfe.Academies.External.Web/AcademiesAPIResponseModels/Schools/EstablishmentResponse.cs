using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

public class EstablishmentResponse
{
	/// <summary>
	/// System.Text de-serialization requires this !!!
	/// </summary>
	public EstablishmentResponse()
	{
		Address = new AddressResponse();
	}

	public EstablishmentResponse(string name, string urn, string ukprn, string street, string town, string fullUkPostcode)
	{
		Urn = urn;
		Name = name;
		Ukprn = ukprn;
		Address = new AddressResponse(street:street, town:town, fullUkPostcode: fullUkPostcode);
	}

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
