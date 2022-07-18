namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Most of these properties are mapping onto [sip].[gias].[Establishment] table
/// As it's assumed that this is the driving table
/// </summary>
public class SchoolDetailsViewModel
{
	public SchoolDetailsViewModel(string schoolName, int urn, string street, string town, string fullUKPostcode)
	{
		SchoolName = schoolName;
		URN = urn;
		Street = street;
		Town = town;
		FullUkPostcode = fullUKPostcode;
	}

	public string SchoolName { get; set; }

	/// <summary>
	/// Not nullable - GIAS unique school Id !
	/// </summary>
	public int URN { get; set; }

	/// <summary>
	/// e.g. 7083
	/// </summary>
	public string? EstablishmentNumber { get; set; }

	public string? UKPRN { get; set; }


	// registered address
	public string Street { get; set; }

	public string? Locality { get; set; }

	public string? Address3 { get; set; }

	public string Town { get; set; }

	public string? CountyDescription { get; set; }

	public string FullUkPostcode { get; set; }
}