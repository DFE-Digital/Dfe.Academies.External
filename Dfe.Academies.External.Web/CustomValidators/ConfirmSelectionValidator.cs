//// Example here:-
//// https://stackoverflow.com/questions/36566836/asp-net-core-mvc-client-side-validation-for-custom-attribute

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.CustomValidators;

public sealed class ConfirmSelectionValidator : ValidationAttribute, IClientModelValidator
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		if (!Convert.ToBoolean(value))
			return new ValidationResult(ErrorMessage);
		else
			return ValidationResult.Success;
	}

	// below inherited from IClientValidatable ??
	//public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
	//{
	//	ModelClientValidationRule mvr = new ModelClientValidationRule(errorMessage: ErrorMessage, validationType: "confirmselection");
	//	return new[] { mvr };
	//}

	public void AddValidation(ClientModelValidationContext context)
	{
		MergeAttribute(context.Attributes, "data-val", "true");
		var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
		MergeAttribute(context.Attributes, "data-val-confirmselection", errorMessage);
	}

	private bool MergeAttribute(
		IDictionary<string, string> attributes,
		string key,
		string value)
	{
		if (attributes.ContainsKey(key))
		{
			return false;
		}
		attributes.Add(key, value);
		return true;
	}
}