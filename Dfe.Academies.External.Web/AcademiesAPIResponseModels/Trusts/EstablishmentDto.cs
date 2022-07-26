using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class EstablishmentDto
{
	[JsonPropertyName("urn")]
	public string Urn { get; }

	[JsonPropertyName("localAuthorityCode")]
	public string LocalAuthorityCode { get; }

	[JsonPropertyName("localAuthorityName")]
	public string LocalAuthorityName { get; }

	[JsonPropertyName("establishmentNumber")]
	public string EstablishmentNumber { get; }

	[JsonPropertyName("establishmentName")]
	public string EstablishmentName { get; }

	[JsonPropertyName("headteacherTitle")]
	public string HeadteacherTitle { get; }

	[JsonPropertyName("headteacherFirstName")]
	public string HeadteacherFirstName { get; }

	[JsonPropertyName("headteacherLastName")]
	public string HeadteacherLastName { get; }

	[JsonPropertyName("establishmentType")]
	public EstablishmentTypeDto EstablishmentType { get; }

	[JsonPropertyName("census")]
	public CensusDto Census { get; }

	[JsonPropertyName("schoolWebsite")]
	public string SchoolWebsite { get; }

	[JsonPropertyName("schoolCapacity")]
	public string SchoolCapacity { get; }

	[JsonConstructor]
	public EstablishmentDto(string urn, string localAuthorityCode, string localAuthorityName,
		string establishmentNumber, string establishmentName, string headteacherTitle, string headteacherFirstName, string headteacherLastName, EstablishmentTypeDto establishmentType, CensusDto census, string schoolWebsite, string schoolCapacity) =>
		(Urn, LocalAuthorityCode, LocalAuthorityName, EstablishmentNumber, EstablishmentName, HeadteacherTitle, HeadteacherFirstName, HeadteacherLastName, EstablishmentType, Census, SchoolWebsite, SchoolCapacity) =
		(urn, localAuthorityCode, localAuthorityName, establishmentNumber, establishmentName, headteacherTitle, headteacherFirstName, headteacherLastName, establishmentType, census, schoolWebsite, schoolCapacity);
}
