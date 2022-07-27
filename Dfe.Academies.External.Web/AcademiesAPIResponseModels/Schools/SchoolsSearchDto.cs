using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;

/// <summary>
/// To de-serialize response from :-
/// {{api-host}}/establishments?api-version=V1&Name=wise&Urn=101934&ukprn=10006563
/// </summary>
public sealed class SchoolsSearchDto
{
	[JsonConstructor]
	public SchoolsSearchDto(string name, string urn, string ukprn) =>
		(Urn, Name, Ukprn) =
		(int.Parse(urn), name, ukprn);

	[JsonPropertyName("urn")]
	public int Urn { get; }

	[JsonPropertyName("name")]
	public string Name { get; }

	[JsonPropertyName("ukprn")]
	public string Ukprn { get; }

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
