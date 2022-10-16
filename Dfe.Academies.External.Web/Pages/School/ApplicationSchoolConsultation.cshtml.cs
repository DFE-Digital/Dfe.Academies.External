using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationSchoolConsultationModel : BasePageEditModel
{
	private readonly IConversionApplicationCreationService _academisationCreationService;

	//// MR:- selected school props for UI rendering
	[BindProperty]
	public int ApplicationId { get; set; }

	[BindProperty]
	public int Urn { get; set; }

	public string SchoolName { get; private set; } = string.Empty;

	//// MR:- VM props to capture data
	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must choose an option")]
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
		_academisationCreationService = academisationCreationService;
	}

	public async Task OnGetAsync(int urn, int appId)
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

	public async Task<IActionResult> OnPostAsync()
	{
		if (!RunUiValidation())
		{
			return Page();
		}

		// grab draft application from temp= null
		var draftConversionApplication =
			TempDataHelper.GetSerialisedValue<ConversionApplication>(
				TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		var dictionaryMapper = PopulateUpdateDictionary();
		await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

		// update temp store for next step - application overview as last step in process
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

		return RedirectToPage("ApplicationSchoolConsultationSummary", new { appId = ApplicationId, urn = Urn });
	}

	///<inheritdoc/>
	public override bool RunUiValidation()
	{
		if (!ModelState.IsValid)
		{
			PopulateValidationMessages();
			return false;
		}

		if (SchoolConsultationStakeholders == SelectOption.No && string.IsNullOrWhiteSpace(SchoolConsultationStakeholdersConsult))
		{
			ModelState.AddModelError("SchoolConsultationStakeholdersConsultNotEntered", "You must provide details");
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
		// if school HAS consulted blank out 'SchoolConsultationStakeholdersConsult'
		if (SchoolConsultationStakeholders == SelectOption.No)
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.SchoolHasConsultedStakeholders), false },
				{ nameof(SchoolApplyingToConvert.SchoolPlanToConsultStakeholders), SchoolConsultationStakeholdersConsult! }
			};
		}
		else
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.SchoolHasConsultedStakeholders), true },
				{ nameof(SchoolApplyingToConvert.SchoolPlanToConsultStakeholders), null }
			};
		}
	}

	private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
		SchoolName = selectedSchool.SchoolName;
		SchoolConsultationStakeholders = selectedSchool.SchoolHasConsultedStakeholders != null && selectedSchool.SchoolHasConsultedStakeholders.Value ? SelectOption.Yes : SelectOption.No;
		SchoolConsultationStakeholdersConsult = selectedSchool.SchoolPlanToConsultStakeholders;
	}
}
