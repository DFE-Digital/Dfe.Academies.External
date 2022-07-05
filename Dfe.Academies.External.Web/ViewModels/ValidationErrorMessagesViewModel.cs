namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ValidationErrorMessagesViewModel
{
    public ValidationErrorMessagesViewModel()
    {
        this.ValidationErrorMessages = new Dictionary<string, string>();
    }

    public Dictionary<string, string> ValidationErrorMessages { get; set; }
}