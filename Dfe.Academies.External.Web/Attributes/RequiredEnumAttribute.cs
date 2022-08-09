using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.Attributes;

internal class RequiredEnumAttribute : RequiredAttribute
{
	public override bool IsValid(object value)
	{
		if (value == null) return false;
		var type = value.GetType();
		return type.IsEnum && Enum.IsDefined(type, value);
	}
}