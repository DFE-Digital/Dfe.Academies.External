using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

/// <summary>
/// To de-serialize response from :-
/// {{api-host}}/establishments?api-version=V1&Name=wise&Urn=101934&ukprn=10006563
/// </summary>
public record SchoolsSearchDto
{
	[JsonConstructor]
	public SchoolsSearchDto(string urn, string name, string ukprn) =>
		(Urn, Name, Ukprn) =
		(urn, name, ukprn);

	/// <summary>
	/// Unique identifier for a school.
	/// </summary>
	public string Urn { get; set; }

	public string Name { get; set; }

	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	public string Ukprn { get; set; }
}
