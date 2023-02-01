using System.Security.Claims;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageModel : PageModel
{
	public bool UserHasSubmitApplicationRole { get; set; } = false;

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
					this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add($"#{modelStateError.Key}", modelStateError.Value);
				}
			}
		}
	}

	protected bool UserIsContributorToApplication(ConversionApplication application)
	{
		// 1) grab application
		// 2) does application.Contributors() contain GetCurrentUserEmail()
		if (application.Contributors.Any())
		{
			return application.Contributors.Any(c =>
				string.Equals(c.EmailAddress.ToLower(), GetCurrentUserEmail().ToLower(), StringComparison.Ordinal));
		}

		return false;
	}

	protected string GetCurrentUserFirstName()
	{
		return User.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty;
	}

	protected string GetCurrentUserSurname()
	{
		return User.FindFirst(ClaimTypes.Surname)?.Value ?? string.Empty;
	}

	protected string GetCurrentUserEmail()
	{
		return User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
	}
}
