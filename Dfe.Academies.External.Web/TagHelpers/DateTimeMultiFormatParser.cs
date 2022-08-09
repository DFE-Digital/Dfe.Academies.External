using System.Globalization;

namespace Dfe.Academies.External.Web.TagHelpers;
public static class DateTimeMultiFormatParser
{
	public static bool TryParse(string input, out DateTime date) =>
		DateTime.TryParse(input, null, DateTimeStyles.RoundtripKind, out date) ||
		DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ||
		DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ||
		DateTime.TryParseExact(input, "dd-MM-yyyyThh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
}
