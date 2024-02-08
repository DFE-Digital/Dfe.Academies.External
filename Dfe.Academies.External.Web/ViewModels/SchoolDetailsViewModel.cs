namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Most of these properties are mapping onto [sip].[gias].[Establishment] table
/// As it's assumed that this is the driving table
/// </summary>
public class SchoolDetailsViewModel
{
	public SchoolDetailsViewModel(string schoolName, int urn, string street, string town, string fullUkPostcode)
	{
		SchoolName = schoolName;
		URN = urn;
		Street = street;
		Town = town;
		FullUkPostcode = fullUkPostcode;
	}

	public string SchoolName { get; set; }

	/// <summary>
	/// Another unique school Id (6 digit number) e.g. 587634
	/// </summary>
	public int URN { get; set; }

	/// <summary>
	/// e.g. 7083
	/// </summary>
	public string? EstablishmentNumber { get; set; }

	// registered address
	public string Street { get; set; }

	public string? Locality { get; set; }

	public string? Address3 { get; set; }

	public string Town { get; set; }

	public string? CountyDescription { get; set; }

	public string FullUkPostcode { get; set; }
}
