using System.Text;
using Dfe.Academies.Contracts.V4;

namespace Dfe.Academies.External.Web.Helpers
{
	public static class AddressHelper
	{
		public static string ToFullAddress(this AddressDto address)
		{

			StringBuilder returnAddress = new StringBuilder { Length = 0 };

			if (!string.IsNullOrWhiteSpace(address.Street))
			{
				returnAddress.Append($"{address.Street}, ");
			}

			if (!string.IsNullOrWhiteSpace(address.Locality))
			{
				returnAddress.Append($"{address.Locality}, ");
			}

			if (!string.IsNullOrWhiteSpace(address.Additional))
			{
				returnAddress.Append($"{address.Additional}, ");
			}

			if (!string.IsNullOrWhiteSpace(address.Town))
			{
				returnAddress.Append($"{address.Town}, ");
			}

			if (!string.IsNullOrWhiteSpace(address.County))
			{
				returnAddress.Append($"{address.County}, ");
			}

			if (!string.IsNullOrWhiteSpace(address.Postcode))
			{
				returnAddress.Append($"{address.Postcode}, ");
			}

			return returnAddress.ToString().TrimEnd(new char[] { ' ', ',' });

		}
	}
	
}
