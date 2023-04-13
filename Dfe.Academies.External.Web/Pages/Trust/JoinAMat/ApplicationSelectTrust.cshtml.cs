using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.JoinAMat
{
	public class ApplicationSelectTrustModel : BaseApplicationPageEditModel
	{
		[BindProperty]
		[MinimumLength(ErrorMessage = "You must give the name of the trust")]
		public string? SearchQuery { get; set; } = string.Empty;

		[BindProperty]
		[ConfirmTrue(ErrorMessage = "You must confirm that this is the correct trust")]
		public bool CorrectTrustConfirmation { get; set; }

		[BindProperty]
		public string TrustReference { get; set; }

		public string SelectedTrustName
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(SearchQuery))
				{
					string[] trustSplit = SearchQuery
						.Trim()
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return trustSplit[0].Trim();
				}

				return string.Empty;
			}
		}

		public int SelectedUkPrn
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(SearchQuery))
				{
					string[] trustSplit = SearchQuery
						.Trim()
						.Replace(")", string.Empty)
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return Convert.ToInt32(trustSplit[^1]);
				}

				return 0;
			}
		}

		public ApplicationSelectTrustModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService conversionApplicationCreationService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				conversionApplicationCreationService, "/ApplicationOverview")
		{
		}

		[ValidateAntiForgeryToken]
		public async Task<ActionResult> OnPostAddTrust()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			// grab draft application from temp
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			await ConversionApplicationCreationService.AddTrustToApplication(draftConversionApplication.ApplicationId, SelectedUkPrn, SelectedTrustName, TrustReference);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = draftConversionApplication.ApplicationId });
		}

		public async Task<IActionResult> OnPostFind()
		{
			string? query = SearchQuery;

			return RedirectToPage("TrustSearchResults");
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
					ModelState.AddModelError("InvalidTrust", "You must give the name of the trust");
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
