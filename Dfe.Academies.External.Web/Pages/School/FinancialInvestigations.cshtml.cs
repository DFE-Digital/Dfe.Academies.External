﻿using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class FinancialInvestigationsModel : BasePageEditModel
	{
	    private readonly ILogger<FinancialInvestigationsModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;

	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public SelectOption? FinanceOngoingInvestigations { get; set; }

		[BindProperty]
		public string? FinancialInvestigationsExplain { get; set; }

		[BindProperty]
		public SelectOption? FinancialInvestigationsTrustAware { get; set; }
		
		public bool FinancialInvestigationsExplainError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("FinancialInvestigationsExplainNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool FinancialInvestigationsTrustAwareError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("FinancialInvestigationsTrustAwareNotSelected"))
				{
					return true;
				}

				return false;
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
			ILogger<FinancialInvestigationsModel> logger,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			try
			{
				LoadAndStoreCachedConversionApplication();

				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
				ApplicationId = appId;
				Urn = urn;

				// Grab other values from API
				if (selectedSchool != null)
				{
					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::FinancialInvestigationsModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{

			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				return Page();
			}

			if (FinanceOngoingInvestigations == SelectOption.Yes && string.IsNullOrWhiteSpace(FinancialInvestigationsExplain))
			{
				ModelState.AddModelError("FinancialInvestigationsExplainNotEntered", "You must provide details of the investigation");
				PopulateValidationMessages();
				return Page();
			}

			if (FinanceOngoingInvestigations == SelectOption.Yes && !FinancialInvestigationsTrustAware.HasValue)
			{
				ModelState.AddModelError("FinancialInvestigationsTrustAwareNotSelected", "You must select an option");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// TODO MR:- no API 28/09/2022
				//var schoolFinanceInvestigations = new FinancialInvestigations(FinanceOngoingInvestigations,
				//	SchoolFinancialInvestigationsExplain,
				//	SchoolFinancialInvestigationsTrustAware);

				//var propertiesToPopulate =
				//	new Dictionary<string, dynamic>
				//	{
				//		{nameof(SchoolApplyingToConvert.FinancialInvestigations), schoolFinanceInvestigations}
				//	};

				//await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, propertiesToPopulate);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("SchoolOverview", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::FinancialInvestigationsModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

		}

		// re-pop date - any dates?
	}
}