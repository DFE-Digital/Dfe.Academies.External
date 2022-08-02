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

	    /// <summary>
	    /// Unique identifier for a school.
	    /// </summary>
        public int Urn { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Unique identifier for a trust. urn is null on trust search
        /// </summary>
		public string? Ukprn { get; set; }

        public AddressResponse Address { get; set; }

        /// <summary>
        /// Some other identifier
        /// </summary>
		public string? UPRN { get; set; }
    }
}