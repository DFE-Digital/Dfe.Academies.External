using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageModel : PageModel
{
	public bool UserHasSubmitApplicationRole { get; private set; } = false;

	public ValidationErrorMessagesViewModel ValidationErrorMessagesViewModel { get; set; }

	protected BasePageModel()
	{
		this.ValidationErrorMessagesViewModel = new ValidationErrorMessagesViewModel();
	}

	public IReadOnlyDictionary<string, IEnumerable<string>?> ConvertModelStateToDictionary()
	{
		return ModelState.ToDictionary(
			kvp => kvp.Key,
			kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage)
		);
	}

	public abstract void PopulateValidationMessages();

	protected void PopulateViewDataErrorsWithModelStateErrors()
	{
		ViewData["Errors"] = ConvertModelStateToDictionary();

		if (!ModelState.IsValid)
		{
			foreach (var modelStateError in ConvertModelStateToDictionary())
			{
				// MR:- add friendly message for validation summary
				if (!this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey(modelStateError.Key))
				{
					this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add(modelStateError.Key, modelStateError.Value);
				}
			}
		}
	}
}
