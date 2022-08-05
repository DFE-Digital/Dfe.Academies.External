namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

public record EstablishmentResponse
{
	public EstablishmentResponse(string urn,
		string establishmentNumber,
		string establishmentName, 
		string ukprn,
		string uprn,
		AddressResponse address)
	{
		Urn = urn;
		EstablishmentNumber = establishmentNumber;
		EstablishmentName = establishmentName;
		Ukprn = ukprn;
		UPRN = uprn;
		Address = address;
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
