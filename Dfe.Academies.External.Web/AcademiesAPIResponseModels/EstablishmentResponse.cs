namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels
{
    public class EstablishmentResponse
    {
	    public EstablishmentResponse(string name, int urn, string? ukprn, string street, string town, string fullUkPostcode)
	    {
		    Name = name;
		    Ukprn = ukprn;
		    Urn = urn;
		    Address = new AddressResponse(street: street, town:town, fullUkPostcode: fullUkPostcode);
	    }

        public int Urn { get; set; }
        public string Name { get; set; }
        public string? Ukprn { get; set; }
        public AddressResponse Address { get; set; }
        public string? UPRN { get; set; }
    }
}