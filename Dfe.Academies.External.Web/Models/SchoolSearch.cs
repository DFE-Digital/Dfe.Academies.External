namespace Dfe.Academies.External.Web.Models;

/// <summary>
/// Container object to pass school search criteria to API layer
/// </summary>
public class SchoolSearch
{
	public SchoolSearch(string schoolName, string ukprn) =>
		(SchoolName, Ukprn) = (schoolName, ukprn);

	public string SchoolName { get; }
	public string Ukprn { get; }
}