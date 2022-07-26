using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class EstablishmentTypeDto
{
	[JsonPropertyName("name")]
	public string Name { get; }
	
	[JsonPropertyName("code")]
	public string Code { get; }
	
	[JsonConstructor]
	public EstablishmentTypeDto(string name, string code) => 
		(Name, Code) = 
		(name, code);
}
