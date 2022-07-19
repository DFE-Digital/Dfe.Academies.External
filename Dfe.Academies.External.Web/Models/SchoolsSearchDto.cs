using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.Models
{
	public sealed class SchoolsSearchDto
	{		
		[JsonPropertyName("ukprn")]
		public string UkPrn { get; }
		
		[JsonPropertyName("urn")]
		public string Urn { get; }
		
		[JsonPropertyName("schoolName")]
		public string SchoolName { get; }
		
		[JsonPropertyName("street")]
		public string Street { get; }
		
		[JsonPropertyName("FullUkPostcode")]
		public string FullUkPostcode { get; }

		[JsonConstructor]
		public SchoolsSearchDto(string ukprn, string urn, string schoolName, 
			string street, string fullUkPostcode) => 
			(UkPrn, Urn, SchoolName, Street, FullUkPostcode) = 
			(ukprn, urn, schoolName, street, fullUkPostcode);
	}
}