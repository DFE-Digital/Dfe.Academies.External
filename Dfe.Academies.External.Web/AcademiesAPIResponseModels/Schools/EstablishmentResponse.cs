namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

public record EstablishmentResponse
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
		EstablishmentName = name;
		Ukprn = ukprn;
		Address = new AddressResponse(street:street, town:town, fullUkPostcode: fullUkPostcode);
	}

	/// <summary>
	/// Unique identifier for a school.
	/// </summary>
	public string Urn { get; set; }

	public string EstablishmentNumber { get; set; }

	public string EstablishmentName { get; set; }

	public AddressResponse Address { get; set; }
	
	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	public string Ukprn { get; set; }

	/// <summary>
	/// Some other identifier
	/// </summary>
	public string UPRN { get; set; }
}
