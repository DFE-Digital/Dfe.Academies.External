namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Frontend model class used only for UI rendering
/// </summary>
public sealed class SchoolSearchResultViewModel
{
	private readonly string _isNullOrEmpty = "-".PadRight(2);

	public SchoolSearchResultViewModel(string schoolName, string urn, string street, string town, string fullUkPostcode)
	{
		SchoolName = schoolName;
		URN = urn;
		Street = street;
		Town = town;
		FullUkPostcode = fullUkPostcode;
	}

	public string SchoolName { get; set; }

	/// <summary>
	/// Not nullable - GIAS unique school Id ? e.g. GAT00123
	/// </summary>
	public string URN { get; set; }

	/// <summary>
	/// e.g. 7083
	/// </summary>
	public string? EstablishmentNumber { get; set; }

	/// <summary>
	/// Another unique school Id (6 digit number) e.g. 587634
	/// </summary>
	public int? UKPRN { get; set; }

	// registered address
	public string Street { get; set; }

	public string? Locality { get; set; }

	public string? Address3 { get; set; }

	public string Town { get; set; }

	public string? CountyDescription { get; set; }

	public string FullUkPostcode { get; set; }

	//public string DisplayName
	//{
	//	get
	//	{
	//		var sb = new StringBuilder();
	//		sb.Append(string.IsNullOrEmpty(SchoolName) ? _isNullOrEmpty : SchoolName);
	//		sb.Append(",").Append(" ");
	//		sb.Append(string.IsNullOrEmpty(UkPrn) ? _isNullOrEmpty : UkPrn);
	//		sb.Append(" ");

	//		return sb.ToString();
	//	}
	//}
}