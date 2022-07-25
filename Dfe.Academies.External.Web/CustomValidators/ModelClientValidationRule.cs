namespace Dfe.Academies.External.Web.CustomValidators;

public sealed class ModelClientValidationRule
{
	public ModelClientValidationRule(string errorMessage, string validationType)
	{
		ErrorMessage = errorMessage;
		ValidationType = validationType;
	}

	public string ErrorMessage { get; set; }

	public string ValidationType { get; set; }

	// MR:- not sure this is right definition !!!
	public Dictionary<string,string> ValidationParameters { get; set; }
}