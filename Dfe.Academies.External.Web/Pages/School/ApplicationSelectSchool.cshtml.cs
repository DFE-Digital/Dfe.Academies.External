using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationSelectSchoolModel : BaseApplicationPageEditModel
	{
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
					string[] schoolSplit = SearchQuery
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
					string[] schoolSplit = SearchQuery
						.Trim()
						.Replace(")", string.Empty)
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return Convert.ToInt32(schoolSplit[^1]);
				}

				return 0;
			}
		}

		public ApplicationSelectSchoolModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService) :
			base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "/ApplicationOverview")
		{
		}

		[ValidateAntiForgeryToken]
		public async Task<ActionResult> OnPostAddSchool()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			await ConversionApplicationCreationService.AddSchoolToApplication(ApplicationId, SelectedUrn, SelectedSchoolName);
			return RedirectToPage(NextStepPage, new { appId = ApplicationId });
		}

		public IActionResult OnPostFind()
		{
			string? query = SearchQuery;

			return RedirectToPage("SchoolSearchResults");
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				// other view model properties initialized within properties
			}
		}

		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				// MR:- if you enter an incorrect name into the autocomplete, then the hidden input is blank (not populated in JS)
				// so, currently get the 'You must give the trust of the school' validation warning
				// rather than the "You must choose a trust from the list" (code below)

				// 2nd phase validation - check selected trust
				if (string.IsNullOrWhiteSpace(SearchQuery))
				{
					ModelState.AddModelError("InvalidSchool", "You must give the name of the school");
				}

				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}
	}
}
