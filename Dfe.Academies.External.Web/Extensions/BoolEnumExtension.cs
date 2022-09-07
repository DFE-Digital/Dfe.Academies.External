using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;

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

	public static string GetStringDescription(this bool? value)
	{
		if (!value.HasValue)
			return QuestionAndAnswerConstants.NoAnswer;

		if (value.Value)
		{
			return "Yes";
		}
		else
		{
			return "No";
		}
	}
}
