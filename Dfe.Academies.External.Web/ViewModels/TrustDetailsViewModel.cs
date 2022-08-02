namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Frontend model class used only for UI rendering
/// </summary>
public class TrustDetailsViewModel
{
	public TrustDetailsViewModel(string trustName, int ukprn, string street, string town, string fullUkPostcode)
	{
		Ukprn = ukprn;
		TrustName = trustName;
		Street = street;
		Town = town;
		FullUkPostcode = fullUkPostcode;
	}

	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	public int Ukprn { get; set; }

	public string TrustName { get; set; }

	// registered address
	public string Street { get; set; }

	public string? Locality { get; set; }

	public string? Address3 { get; set; }

	public string Town { get; set; }

	public string? CountyDescription { get; set; }

	public string FullUkPostcode { get; set; }

	public string DisplayName => $"{TrustName} ({Ukprn})";
}