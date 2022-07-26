namespace Dfe.Academies.External.Web.Models;

public class Address
{
	public Address(string street, string town, string fullUkPostcode)
	{
		Street = street;
		Town = town;
		FullUkPostcode = fullUkPostcode;
	}

	public string Street { get; set; }

	public string? Locality { get; set; }

	public string? Address3 { get; set; }

	public string Town { get; set; }

	public string? CountyDescription { get; set; }

	public string FullUkPostcode { get; set; }
}