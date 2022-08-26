using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Sentry.Protocol;

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
	// enum - to school / to trust
	[BindProperty]
	public PayFundsTo? SchoolSupportGrantFundsPaidTo { get; set; }

	[BindProperty]
	public bool? ConfirmSchoolPay { get; set; }

	public bool HasError
	{
		get
		{
			var bools = new[] { SchoolSupportGrantFundsPaidToError,
				ConfirmSchoolPayError
			};

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

	public bool ConfirmSchoolPayError
	{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("ConfirmSchoolPayNotEntered"))
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
			var conversionApplication = await LoadAndSetApplicationDetails(appId);

			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			// Grab other values from API
			if (selectedSchool != null)
			{
				// TODO MR:- grab data from API endpoint - applicationId && SchoolId combination !
				// data stored against the school ?????????????? not implemented 22/08/2022


				PopulateUiModel(selectedSchool, conversionApplication);
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

		if (ApplicationType == ApplicationTypes.JoinMat && !SchoolSupportGrantFundsPaidTo.HasValue)
		{
			ModelState.AddModelError("SchoolSupportGrantFundsPaidToNotEntered", "You must provide details");
			PopulateValidationMessages();
			return Page();
		}
		else if (ApplicationType != ApplicationTypes.JoinMat && !ConfirmSchoolPay.HasValue)
		{
			ModelState.AddModelError("ConfirmSchoolPayNotEntered", "You must provide details");
			PopulateValidationMessages();
			return Page();
		}

		try
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
			PayFundsTo schoolSupportGrantFundsPaidTo = PayFundsTo.School;

			if (ApplicationType == ApplicationTypes.JoinMat)
			{
				schoolSupportGrantFundsPaidTo = SchoolSupportGrantFundsPaidTo!.Value;
			}
			else
			{
				if (ConfirmSchoolPay!.Value!= true)
				{
					schoolSupportGrantFundsPaidTo = PayFundsTo.Trust;
				}
			}

			// TODO MR:- call API endpoint to log data
			//await _academisationCreationService.ApplicationPreOpeningSupportGrantUpdate(schoolSupportGrantFundsPaidTo);

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
		ApplicationId = selectedSchool.ApplicationId;
		Urn = selectedSchool.URN;
		SchoolName = selectedSchool.SchoolName;
		// TODO MR:- populate other props from API - not implemented 22/08/2022
		//PayFundsTo = selectedSchool.PayFundsTo;
	}
}

