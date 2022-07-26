using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class CensusDto
{
	[JsonPropertyName("numberOfPupils")]
	public string NumberOfPupils { get; }

	[JsonConstructor]
	public CensusDto(string numberOfPupils) =>
		(NumberOfPupils) =
		(numberOfPupils);
}
