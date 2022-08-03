namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public record GroupContactAddressDto
{
	public string Street { get; }

	public string Locality { get; }

	public string AdditionalLine { get; }

	public string Town { get; }

	public string County { get; }

	public string Postcode { get; }

	public GroupContactAddressDto(string street, string locality, string additionalLine, string town, string county, string postcode) =>
		(Street, Locality, AdditionalLine, Town, County, Postcode) = (street, locality, additionalLine, town, county, postcode);
}
