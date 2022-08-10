using System.Globalization;

namespace Dfe.Academies.External.Web.Helpers;
public static class DateTimeMultiFormatParser
{
	public static bool TryParse(string input, out DateTime date) =>
		DateTime.TryParse(input, null, DateTimeStyles.RoundtripKind, out date) ||
		DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None, out date) ||
		DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None, out date) ||
		DateTime.TryParseExact(input, "dd-MM-yyyyThh:mm:ss", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None, out date);
}
