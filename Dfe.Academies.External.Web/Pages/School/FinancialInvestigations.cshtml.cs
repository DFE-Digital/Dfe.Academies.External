using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class FinancialInvestigationsModel : BaseSchoolPageEditModel
	{
		// MR:- VM props to capture data
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? FinanceOngoingInvestigations { get; set; }

		[BindProperty]
		public string? FinancialInvestigationsExplain { get; set; }

		[BindProperty]
		public SelectOption? FinancialInvestigationsTrustAware { get; set; }
		
		public bool FinancialInvestigationsExplainError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("FinancialInvestigationsExplainNotEntered");
			}
		}

		public bool FinancialInvestigationsTrustAwareError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("FinancialInvestigationsTrustAwareNotSelected");
			}
		}

		public bool HasError
		{
			get
			{
				var bools = new[] { FinancialInvestigationsExplainError, 
					FinancialInvestigationsTrustAwareError };

				return bools.Any(b => b);
			}
		}

		public FinancialInvestigationsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "FinancesReview")
		{}

		//public async Task OnGetAsync(int urn, int appId)
		//{
		//	LoadAndStoreCachedConversionApplication();

		//	var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
		//	ApplicationId = appId;
		//	Urn = urn;

		//	// Grab other values from API
		//	if (selectedSchool != null)
		//	{
		//		PopulateUiModel(selectedSchool);
		//	}
		//}

		//public async Task<IActionResult> OnPostAsync()
		//{
		//	if (!RunUiValidation())
		//	{
		//		return Page();
		//	}
			
		//	// grab draft application from temp= null
		//	var draftConversionApplication =
		//		TempDataHelper.GetSerialisedValue<ConversionApplication>(
		//			TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		//	var dictionaryMapper = PopulateUpdateDictionary();
		//	await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

		//	// update temp store for next step - application overview
		//	TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

		//	return RedirectToPage("FinancesReview", new { appId = ApplicationId, urn = Urn });
		//}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}


			if (FinanceOngoingInvestigations == SelectOption.Yes && string.IsNullOrWhiteSpace(FinancialInvestigationsExplain))
			{
				ModelState.AddModelError("FinancialInvestigationsExplainNotEntered", "You must provide details of the investigation");
				PopulateValidationMessages();
				return false;
			}

			if (FinanceOngoingInvestigations == SelectOption.Yes && !FinancialInvestigationsTrustAware.HasValue)
			{
				ModelState.AddModelError("FinancialInvestigationsTrustAwareNotSelected", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// if FinanceOngoingInvestigations == no, clear FinancialInvestigationsExplain && FinancialInvestigationsTrustAware
			if (FinanceOngoingInvestigations == SelectOption.No)
			{
				return new Dictionary<string, dynamic>
				{
					{nameof(SchoolApplyingToConvert.FinanceOngoingInvestigations), false},
					{nameof(SchoolApplyingToConvert.FinancialInvestigationsExplain), null},
					{nameof(SchoolApplyingToConvert.FinancialInvestigationsTrustAware), null},
				};
			}
			else
			{
				return new Dictionary<string, dynamic>
					{
						{nameof(SchoolApplyingToConvert.FinanceOngoingInvestigations), true},
						{nameof(SchoolApplyingToConvert.FinancialInvestigationsExplain), FinancialInvestigationsExplain!},
						{nameof(SchoolApplyingToConvert.FinancialInvestigationsTrustAware), FinancialInvestigationsTrustAware == SelectOption.Yes},
					};
			}
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			FinanceOngoingInvestigations = selectedSchool.FinanceOngoingInvestigations.GetEnumValue();
			FinancialInvestigationsExplain = selectedSchool.FinancialInvestigationsExplain;
			FinancialInvestigationsTrustAware = selectedSchool.FinancialInvestigationsTrustAware.GetEnumValue(); ; 
		}
	}
}
