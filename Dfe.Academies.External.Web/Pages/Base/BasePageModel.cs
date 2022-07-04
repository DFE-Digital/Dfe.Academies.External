using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageModel : PageModel
{
    public BasePageModel()
    {
        this.ValidationErrorMessages = new ValidationErrorMessagesViewModel();
    }

    public ValidationErrorMessagesViewModel ValidationErrorMessages { get; set; }

    public Dictionary<string, string?> ConvertModelDictionary()
    {
        return ModelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).FirstOrDefault()?.ToString()
        );
    }

    public abstract void PopulateValidationMessages();
}