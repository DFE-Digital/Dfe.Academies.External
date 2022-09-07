using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Extensions;

internal static class BoolEnumExtension
{
	public static SelectOption? GetEnumValue(this bool? value)
	{
		if (!value.HasValue)
			return null;

		if (value.Value)
		{
			return SelectOption.Yes;
		}
		else
		{
			return SelectOption.No;
		}
	}
}
