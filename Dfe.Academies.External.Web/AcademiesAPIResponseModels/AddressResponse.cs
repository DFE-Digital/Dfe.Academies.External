using System.Text;

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

		public string Street { get; set; }

		public string? Locality { get; set; }

		public string? AdditionalLine { get; set; }

		public string Town { get; set; }

		public string? County { get; set; }

		public string Postcode { get; set; }

		public string FullAddress
		{
			get
			{
				StringBuilder returnAddress = new StringBuilder { Length = 0};
				
				if (!string.IsNullOrWhiteSpace(Street))
				{
					returnAddress.Append($"{Street}, ");
				}

				if (!string.IsNullOrWhiteSpace(Locality))
				{
					returnAddress.Append($"{Locality}, ");
				}

				if (!string.IsNullOrWhiteSpace(AdditionalLine))
				{
					returnAddress.Append($"{AdditionalLine}, ");
				}

				if (!string.IsNullOrWhiteSpace(Town))
				{
					returnAddress.Append($"{Town}, ");
				}

				if (!string.IsNullOrWhiteSpace(County))
				{
					returnAddress.Append($"{County}, ");
				}

				if (!string.IsNullOrWhiteSpace(Postcode))
				{
					returnAddress.Append($"{Postcode}, ");
				}

				return returnAddress.ToString();
			}
		}
	}
}
