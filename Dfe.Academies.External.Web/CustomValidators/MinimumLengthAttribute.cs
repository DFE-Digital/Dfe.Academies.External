//// See this for more information:- http://programersnotebook.blogspot.com/2013/03/customizing-validation-attributes-in-mvc.html

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.CustomValidators;

[AttributeUsage(AttributeTargets.Property)]
public sealed class MinimumLengthAttribute : ValidationAttribute, IClientModelValidator
{
	private const short MinimumLength = 4;

	protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
	{
		string? elementValue = Convert.ToString(value);

		if (string.IsNullOrWhiteSpace(elementValue) || elementValue.Length < MinimumLength)
		{
			return new ValidationResult(ErrorMessage);
		}
		else
			return ValidationResult.Success!;
	}

	public void AddValidation(ClientModelValidationContext context)
	{
		// this is tagging on the HTML 5 data attributes that make this work client side
		MergeAttribute(context.Attributes, "data-val", "true");
		var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
		MergeAttribute(context.Attributes, "data-val-searchqueryrequired", errorMessage);
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