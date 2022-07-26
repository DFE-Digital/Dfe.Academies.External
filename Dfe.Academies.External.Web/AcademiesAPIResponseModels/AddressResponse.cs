namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels
{
    public class AddressResponse
    {
	    public AddressResponse(string street, string town, string fullUkPostcode)
	    {
		    Street = street;
		    Town = town;
		    Postcode = fullUkPostcode;
	    }

        public string Street { get; set; }
        public string? Locality { get; set; }
        public string? AdditionalLine { get; set; }
        public string Town { get; set; }
        public string? County { get; set; }
        public string Postcode { get; set; }
    }
}