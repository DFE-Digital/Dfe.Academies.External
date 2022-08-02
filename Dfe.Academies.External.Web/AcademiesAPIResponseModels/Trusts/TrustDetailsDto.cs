using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

/// <summary>
/// To de-serialize response from trust search && GetTrustByUkPrn :-
/// {{api-host}}/trusts?api-version=V1&groupName=grammar
/// {{api-host}}/trust/10058464?api-version=V1
/// INFO:- trusts & schools are slammed together within the API and returned together
/// </summary>
public sealed class TrustDetailsDto
{
	/// <summary>
	/// MR:- need gias response - as response might be a school
	/// </summary>
	[JsonPropertyName("giasData")]
	public GiasDataDto GiasData { get; }

	/// <summary>
	/// MR:- need ifd response - as response might be a trust
	/// </summary>
	[JsonPropertyName("ifdData")]
	public IfdDataDto IfdData { get; }

	[JsonConstructor]
	public TrustDetailsDto(GiasDataDto giasData, IfdDataDto ifdData) =>
		(GiasData, IfdData) = (giasData, ifdData);
}
