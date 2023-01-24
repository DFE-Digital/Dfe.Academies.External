using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using System.ComponentModel.DataAnnotations;
using System;
using Dfe.Academies.External.Web.Pages.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notify.Models;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using static GovUk.Frontend.AspNetCore.ComponentDefaults;

namespace Dfe.Academies.External.Web.Pages.Trust.JoinAMat
{
	public class ApplicationSchoolChangesToATrustModel : BaseSchoolPageEditModel
	{

		public ApplicationSchoolChangesToATrustModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "ApplicationSchoolLocalGovernanceArrangements")
		{
		}

		[BindProperty]
		public TrustChange? TrustChange { get; set; }

		public string? SelectedTrustName { get; set; }

		[BindProperty]
		public string? TrustChangeExplained { get; set; }

		public bool TrustChangeExplainedError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustChangeExplainedNotEntered");
			}
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!TrustChange.HasValue)
			{
				ModelState.AddModelError("TrustChangeExplainedNotSelected", "You must select an option");
				PopulateValidationMessages();
				return false;
			}

			if (TrustChange.HasValue && TrustChange.Value == Enums.TrustChange.Yes && string.IsNullOrWhiteSpace(TrustChangeExplained))
			{
				ModelState.AddModelError("TrustChangeExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new();
		}

		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			// Grab other values from API
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
			SelectedTrustName = applicationDetails?.JoinTrustDetails?.TrustName ?? string.Empty;
			TrustChange = applicationDetails?.JoinTrustDetails?.ChangesToTrust;
			TrustChangeExplained = applicationDetails?.JoinTrustDetails?.ChangesToTrustExplained;

			return await base.OnGetAsync(urn, appId);
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);

			if (!RunUiValidation())
			{
				return Page();
			}

			if (applicationDetails?.JoinTrustDetails == null)
			{
				throw new InvalidOperationException("Application has no existing trust details");
			}

			TrustChangeExplained = TrustChange == Enums.TrustChange.Yes ? TrustChangeExplained : null;
			var updatedTrustDetails = applicationDetails.JoinTrustDetails with { ChangesToTrust = TrustChange, ChangesToTrustExplained = TrustChangeExplained };

			await ConversionApplicationCreationService.SetExistingTrustDetails(ApplicationId, updatedTrustDetails);

			var draftConversionApplication =
			TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
		}
	}
}
