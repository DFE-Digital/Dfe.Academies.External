using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

/// <summary>
/// To de-serialize response from trust search && GetTrustByUkPrn :-
/// {{api-host}}/trusts?api-version=V1&groupName=grammar
/// {{api-host}}/trust/10058464?api-version=V1
/// </summary>
public sealed class TrustDetailsDto
{
	[JsonPropertyName("giasData")]
	public GiasDataDto GiasData { get; }

	[JsonPropertyName("ifdData")]
	public IfdDataDto IfdData { get; }

	[JsonPropertyName("establishments")]
	public List<EstablishmentDto> Establishments { get; }

	[JsonConstructor]
	public TrustDetailsDto(GiasDataDto giasData, IfdDataDto ifdData, List<EstablishmentDto> establishments) =>
		(GiasData, IfdData, Establishments) = (giasData, ifdData, establishments);
}
