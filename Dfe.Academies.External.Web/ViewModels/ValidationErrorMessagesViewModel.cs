namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ValidationErrorMessagesViewModel
{
	public ValidationErrorMessagesViewModel()
	{
		this.ValidationErrorMessages = new Dictionary<string, IEnumerable<string>?>();
	}

	public Dictionary<string, IEnumerable<string>?> ValidationErrorMessages { get; set; }
}