namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Frontend model class used only for UI rendering
/// </summary>
public sealed class SchoolSearchResultViewModel
{
	public SchoolSearchResultViewModel(string schoolName, int urn, string street, string town, string fullUkPostcode)
	{
		URN = urn;
		SchoolName = schoolName;
		Street = street;
		Town = town;
		FullUkPostcode = fullUkPostcode;
	}

	/// <summary>
	/// Unique school Id (6 digit number) e.g. 587634
	/// </summary>
	public int URN { get; set; }

	/// <summary>
	/// Not nullable - GIAS unique school Id ? e.g. GAT00123
	/// </summary>
	public string? UKPRN { get; set; }

	public string SchoolName { get; set; }
	
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

    public string DisplayName => $"{SchoolName} ({URN})";
}