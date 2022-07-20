using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.Models
{
	public sealed class SchoolsSearchDto
	{		
		[JsonPropertyName("urn")]
		public int Urn { get; }

		[JsonPropertyName("schoolName")]
		public string SchoolName { get; }
		
		[JsonPropertyName("street")]
		public string Street { get; }

		[JsonPropertyName("town")]
		public string Town { get; }

		[JsonPropertyName("FullUkPostcode")]
		public string FullUkPostcode { get; }

		[JsonConstructor]
		public SchoolsSearchDto(string schoolName, int urn, 
			string street, string town, string fullUkPostcode) => 
			(SchoolName, Urn, Street, Town, FullUkPostcode) = 
			(schoolName, Urn, street, town, fullUkPostcode);
	}
}