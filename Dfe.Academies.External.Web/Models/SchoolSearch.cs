namespace Dfe.Academies.External.Web.Models;

/// <summary>
/// Container object to pass school search criteria to API layer
/// </summary>
public class SchoolSearch
{
	public SchoolSearch(string schoolName, string urn) =>
		(SchoolName, Urn) = (schoolName, urn);

	public string SchoolName { get; }
	public string Urn { get; }
}