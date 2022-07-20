using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.Models
{
	public sealed class SchoolsSearchDto
	{		
		[JsonPropertyName("ukprn")]
		public int UkPrn { get; }

		[JsonPropertyName("schoolName")]
		public string SchoolName { get; }
		
		[JsonPropertyName("street")]
		public string Street { get; }

		[JsonPropertyName("town")]
		public string Town { get; }

		[JsonPropertyName("FullUkPostcode")]
		public string FullUkPostcode { get; }

		[JsonConstructor]
		public SchoolsSearchDto(string schoolName, int ukprn, 
			string street, string town, string fullUkPostcode) => 
			(SchoolName, UkPrn, Street, Town, FullUkPostcode) = 
			(schoolName, ukprn, street, town, fullUkPostcode);
	}
}