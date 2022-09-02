using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationSchoolConsultationModel : BasePageEditModel
{
	private readonly ILogger<ApplicationSchoolConsultationModel> _logger;
	private readonly IConversionApplicationCreationService _academisationCreationService;

	//// MR:- selected school props for UI rendering
	[BindProperty]
	public int ApplicationId { get; set; }

	[BindProperty]
	public int Urn { get; set; }

	public string SchoolName { get; private set; } = string.Empty;

	//// MR:- VM props to capture data
	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must provide details")]
	public SelectOption SchoolConsultationStakeholders { get; set; }

	[BindProperty]
	public string? SchoolConsultationStakeholdersConsult { get; set; }

	public bool HasError
	{
		get
		{
			var bools = new[] { SchoolConsultationStakeholdersConsultError };

			return bools.Any(b => b);
		}
	}

	public bool SchoolConsultationStakeholdersConsultError
{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolConsultationStakeholdersConsultNotEntered"))
			{
				return true;
			}

			return false;
		}
	}

	public ApplicationSchoolConsultationModel(ILogger<ApplicationSchoolConsultationModel> logger,
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
			_logger.LogError("School::ApplicationSchoolConsultationModel::OnGetAsync::Exception - {Message}", ex.Message);
		}
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			PopulateValidationMessages();
			return Page();
		}

		if (SchoolConsultationStakeholders == SelectOption.No && string.IsNullOrWhiteSpace(SchoolConsultationStakeholdersConsult))
		{
			ModelState.AddModelError("SchoolConsultationStakeholdersConsultNotEntered", "You must provide details");
			PopulateValidationMessages();
			return Page();
		}

		try
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			// TODO MR:- save away data
			// await _academisationCreationService.ApplicationChangeSchoolNameAndReason(draftConversionApplication, SchoolConsultationStakeholders, SchoolConsultationStakeholdersConsult, Urn);

			// update temp store for next step - application overview as last step in process
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage("ApplicationSchoolConsultationSummary", new { appId = ApplicationId, urn = Urn });
		}
		catch (Exception ex)
		{
			_logger.LogError("School::ApplicationSchoolConsultationModel::OnPostAsync::Exception - {Message}", ex.Message);
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
		// TODO MR:- populate other props from API - not implemented 22/08/2022
		//SchoolConsultationStakeholders = selectedSchool.;
		//SchoolConsultationStakeholdersConsult = selectedSchool.;
	}
}
