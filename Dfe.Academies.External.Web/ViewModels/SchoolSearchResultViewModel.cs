namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Frontend model class used only for UI rendering
/// </summary>
public sealed class SchoolSearchResultViewModel
{
	public SchoolSearchResultViewModel(int urn, string name,  string ukprn)
	{
		URN = urn;
		SchoolName = name;
		UKPRN = ukprn;
	}

	/// <summary>
	/// Another unique school Id (6 digit number) e.g. 587634
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

	// registered address - not returned by trams / academies search school endpoint currently
	public string Street { get; set; } = string.Empty;

	public string? Locality { get; set; }

	public string? Address3 { get; set; }

	public string Town { get; set; } = string.Empty;

	public string? CountyDescription { get; set; }

	public string FullUkPostcode { get; set; } = string.Empty;

	public string DisplayName => $"{SchoolName} ({URN})";
}