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

	/// <summary>
	/// setup model state errors
	/// </summary>
	public abstract void PopulateValidationMessages();

	/// <summary>
	/// Populate ViewData["Errors"] with model state errors to then be displayed by _ErrorMessages partial view
	/// on post-back
	/// </summary>
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
