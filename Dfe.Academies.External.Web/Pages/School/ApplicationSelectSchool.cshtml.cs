using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationSelectSchoolModel : BasePageModel
	{
		private readonly ILogger<ApplicationSelectSchoolModel> _logger;
		private readonly IConversionApplicationCreationService _conversionApplicationCreationService;
		private const string NextSchoolStepPage = "/ApplicationOverview";

		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		[MinimumLengthAttribute(ErrorMessage = "You must give the name of the school")]
		public string? SearchQuery { get; set; } = string.Empty;

		[BindProperty]
		[ConfirmTrue(ErrorMessage = "You must confirm that this is the correct school")]
		public bool CorrectSchoolConfirmation { get; set; } = false;

		public string SelectedSchoolName
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(SearchQuery))
				{
					var schoolSplit = SearchQuery
						.Trim()
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return schoolSplit[0].Trim();
				}

				return string.Empty;
			}
		}

		public int SelectedUrn
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(SearchQuery))
				{
					var schoolSplit = SearchQuery
						.Trim()
						.Replace(")", string.Empty)
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return Convert.ToInt32(schoolSplit[^1]);
				}

				return 0;
			}
		}

		public ApplicationSelectSchoolModel(ILogger<ApplicationSelectSchoolModel> logger, IConversionApplicationCreationService conversionApplicationCreationService)
		{
			_logger = logger;
			_conversionApplicationCreationService = conversionApplicationCreationService;
		}
		public void OnGet(int appId)
		{
			try
			{
				ApplicationId = appId;
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationSelectSchoolModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<ActionResult> OnPostAddSchool()
		{
			if (!ModelState.IsValid)
			{
				// MR:- if you enter an incorrect name into the autocomplete, then the hidden input is blank (not populated in JS)
				// so, currently get the 'You must give the name of the school' validation warning
				// rather than the "You must choose a school from the list" (code below)

				//// 2nd phase validation - check selected school
				if (string.IsNullOrWhiteSpace(SearchQuery))
				{
					ModelState.AddModelError("InvalidSchool", "You must give the name of the school");
				}

				PopulateValidationMessages();
				return Page();
			}

			try
			{
				await _conversionApplicationCreationService.AddSchoolToApplication(ApplicationId, SelectedUrn, SelectedSchoolName);
				return RedirectToPage(NextSchoolStepPage, new { appId = ApplicationId });
			}
			catch (Exception ex)
			{
				_logger.LogError("Application::ApplicationSelectSchoolModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public IActionResult OnPostFind()
		{
			var query = SearchQuery;

			return RedirectToPage("SchoolSearchResults");
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				ApplicationId = conversionApplication.ApplicationId;
				// other view model properties initialized within properties
			}
		}
	}
}
