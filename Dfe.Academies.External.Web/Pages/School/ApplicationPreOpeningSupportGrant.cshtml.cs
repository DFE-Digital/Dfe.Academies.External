using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationPreOpeningSupportGrantModel : BasePageEditModel
{
	private readonly ILogger<ApplicationPreOpeningSupportGrantModel> _logger;
	private readonly IConversionApplicationCreationService _academisationCreationService;

	//// MR:- selected school props for UI rendering
	[BindProperty]
	public int ApplicationId { get; set; }

	[BindProperty]
	public int Urn { get; set; }

	public string SchoolName { get; private set; } = string.Empty;

	[BindProperty]
	public ApplicationTypes ApplicationType { get; set; }

	//// MR:- VM props to capture data
	[BindProperty]
	public PayFundsTo? SchoolSupportGrantFundsPaidTo { get; set; }

	[BindProperty]
	public bool ConfirmSchoolPay { get; set; }

	public bool HasError
	{
		get
		{
			var bools = new[] { SchoolSupportGrantFundsPaidToError };

			return bools.Any(b => b);
		}
	}

	public bool SchoolSupportGrantFundsPaidToError
	{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolSupportGrantFundsPaidToNotEntered"))
			{
				return true;
			}

			return false;
		}
	}

	public ApplicationPreOpeningSupportGrantModel(ILogger<ApplicationPreOpeningSupportGrantModel> logger,
		IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
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
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool, draftConversionApplication);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError("School::ApplicationPreOpeningSupportGrantModel::OnGetAsync::Exception - {Message}", ex.Message);
		}
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			// error messages component consumes ViewData["Errors"]
			PopulateValidationMessages();
			return Page();
		}

		if (ApplicationType == ApplicationTypes.JoinAMat && !SchoolSupportGrantFundsPaidTo.HasValue)
		{
			ModelState.AddModelError("SchoolSupportGrantFundsPaidToNotEntered", "You must provide details");
			PopulateValidationMessages();
			return Page();
		}

		try
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
			PayFundsTo schoolSupportGrantFundsPaidTo = PayFundsTo.School;

			if (ApplicationType == ApplicationTypes.JoinAMat)
			{
				schoolSupportGrantFundsPaidTo = SchoolSupportGrantFundsPaidTo!.Value;
			}
			else
			{
				if (ConfirmSchoolPay != true)
				{
					schoolSupportGrantFundsPaidTo = PayFundsTo.Trust;
				}
			}

			var dictionaryMapper = new Dictionary<string, dynamic>
			{
				{ "SchoolSupportGrantFundsPaidTo", schoolSupportGrantFundsPaidTo },
				{ "ConfirmPaySupportGrantToSchool", ConfirmSchoolPay }
			};
			// MR:- call API endpoint to log data
			await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage("ApplicationPreOpeningSupportGrantSummary", new { appId = ApplicationId, urn = Urn });
		}
		catch (Exception ex)
		{
			_logger.LogError("School::ApplicationPreOpeningSupportGrantModel::OnPostAsync::Exception - {Message}", ex.Message);
			return Page();
		}
	}

	public override void PopulateValidationMessages()
	{
		PopulateViewDataErrorsWithModelStateErrors();
	}

	private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ConversionApplication? conversionApplication)
	{
		ApplicationType = conversionApplication.ApplicationType;
		SchoolName = selectedSchool.SchoolName;
		if (conversionApplication.ApplicationType != ApplicationTypes.JoinAMat)
		{
			SchoolSupportGrantFundsPaidTo = PayFundsTo.Trust;
			ConfirmSchoolPay = false;
		}
		else
		{
			SchoolSupportGrantFundsPaidTo = selectedSchool.SchoolSupportGrantFundsPaidTo;
			ConfirmSchoolPay = selectedSchool.ConfirmPaySupportGrantToSchool ?? false;
		}

		// TODO MR:- populate other props from API - not implemented 22/08/2022
		//SchoolSupportGrantFundsPaidTo = selectedSchool.PayFundsTo;
		//ConfirmSchoolPay = selectedSchool.PayFundsTo;
	}
}

