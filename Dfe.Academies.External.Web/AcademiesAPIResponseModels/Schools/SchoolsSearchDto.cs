using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

/// <summary>
/// To de-serialize response from :-
/// {{api-host}}/establishments?api-version=V1&Name=wise&Urn=101934&ukprn=10006563
/// </summary>
public sealed class SchoolsSearchDto
{
	[JsonConstructor]
	public SchoolsSearchDto(string urn, string name, string ukprn) =>
		(Urn, Name, Ukprn) =
		(urn, name, ukprn);

	/// <summary>
	/// Unique identifier for a school.
	/// </summary>
	[JsonPropertyName("urn")]
	public string Urn { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	[JsonPropertyName("ukprn")]
	public string Ukprn { get; set; }

	// TODO MR:- current establishments search API doesn't return address details
	//[JsonPropertyName("street")]
	//public string Street { get; }

	//[JsonPropertyName("town")]
	//public string Town { get; }

	//[JsonPropertyName("FullUkPostcode")]
	//public string FullUkPostcode { get; }

	//[JsonConstructor]
	//public SchoolsSearchDto(string name, int urn, string ukprn,
	//	string street, string town, string fullUkPostcode) =>
	//	(Name, Urn, Ukprn, Street, Town, FullUkPostcode) =
	//	(name, urn, ukprn, street, town, fullUkPostcode);
}
