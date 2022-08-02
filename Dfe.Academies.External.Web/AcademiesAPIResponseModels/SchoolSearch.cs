namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels;

/// <summary>
/// Container object to pass school search criteria to API layer :-
/// {{api-host}}/establishments?Name=wise&api-version=V1&Urn=101934&ukprn=10006563
/// </summary>
public class SchoolSearch
{
	public SchoolSearch(string name, string urn, string ukprn) =>
		(Name, Urn, Ukprn) = (name, urn, ukprn);

	public string Name { get; }

	/// <summary>
	/// Unique identifier for a school.
	/// </summary>
	public string Urn { get; }

	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	public string Ukprn { get; set; }
}