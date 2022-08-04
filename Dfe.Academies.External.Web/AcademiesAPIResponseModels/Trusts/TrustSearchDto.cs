namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public record TrustSearchDto
{
	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	public string UkPrn { get; set; }

	/// <summary>
	/// Unique identifier for a school.
	/// </summary>
	public string Urn { get; set; }

	public string GroupName { get; set; }

	public string CompaniesHouseNumber { get; set; }

	public GroupContactAddressDto TrustAddress { get; set; }
}
