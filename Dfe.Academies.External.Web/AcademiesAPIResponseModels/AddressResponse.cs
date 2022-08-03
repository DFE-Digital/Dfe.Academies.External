using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels
{
    public record AddressResponse
    {
		/// <summary>
		/// System.Text de-serialization requires this !!!
		/// </summary>
	    public AddressResponse()
	    {
	    }

	    public AddressResponse(string street, string town, string fullUkPostcode)
	    {
		    Street = street;
		    Town = town;
		    Postcode = fullUkPostcode;
	    }

	    [JsonPropertyName("street")]
		public string Street { get; set; }

		[JsonPropertyName("locality")]
		public string? Locality { get; set; }

		[JsonPropertyName("additionalLine")]
		public string? AdditionalLine { get; set; }

		[JsonPropertyName("town")]
		public string Town { get; set; }

		[JsonPropertyName("county")]
		public string? County { get; set; }

		[JsonPropertyName("postcode")]
		public string Postcode { get; set; }
    }
}