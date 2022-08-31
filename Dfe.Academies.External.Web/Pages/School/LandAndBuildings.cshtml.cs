using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class LandAndBuildingsModel : BasePageEditModel
	{
	    private readonly ILogger<LandAndBuildingsModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;

	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture land & buildings data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string SchoolBuildLandOwnerExplained { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandWorksPlanned { get; set; }

		[BindProperty]
		public string? SchoolBuildLandWorksPlannedExplained { get; set; }

		[BindProperty]
		public DateTime? SchoolBuildLandWorksPlannedDate { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? WorksPlannedDateDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? WorksPlannedDateMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? WorksPlannedDateYear { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandSharedFacilities { get; set; }

		[BindProperty]
		public string? SchoolBuildLandSharedFacilitiesExplained { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandGrants { get; set; } 

		[BindProperty]
		public string? SchoolBuildLandGrantsBodies { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandPFIScheme { get; set; }

		[BindProperty]
		public string? SchoolBuildLandPFISchemeType { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandPriorityBuildingProgramme { get; set; } 

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandFutureProgramme { get; set; }

		public bool HasError
		{
			get
			{
				var bools = new[] { SchoolBuildLandWorksPlannedError, 
											SchoolBuildLandWorksPlannedDateError,
											SchoolBuildLandSharedFacilitiesExplainedError,
											SchoolBuildLandGrantsBodiesError,
											SchoolBuildLandPFISchemeTypeError
				};

				return bools.Any(b => b);
			}
		}

		public bool SchoolBuildLandWorksPlannedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandWorksPlannedExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool SchoolBuildLandWorksPlannedDateError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandWorksPlannedDateNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool SchoolBuildLandSharedFacilitiesExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandSharedFacilitiesExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool SchoolBuildLandGrantsBodiesError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandGrantsBodiesNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool SchoolBuildLandPFISchemeTypeError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandPFISchemeTypeNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public LandAndBuildingsModel(ILogger<LandAndBuildingsModel> logger,
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
					// TODO MR:- grab existing land & buildings data from API endpoint to populate VM - applicationId && SchoolId combination !
					// land & buildings stored against the school ?????????????? not implemented 18/08/2022

					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::LandAndBuildingsModel::OnGetAsync::Exception - {Message}", ex.Message);
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
			
			if (SchoolBuildLandWorksPlanned == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandWorksPlannedExplained))
			{
				ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			if (SchoolBuildLandWorksPlanned == SelectOption.Yes && !SchoolBuildLandWorksPlannedDate.HasValue)
			{
				ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", "You must select a scheduled completion date");
				PopulateValidationMessages();
				return Page();
			}

			if (SchoolBuildLandSharedFacilities == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandSharedFacilitiesExplained))
			{
				ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			if (SchoolBuildLandGrants == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandGrantsBodies))
			{
				ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			if (SchoolBuildLandPFIScheme == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandPFISchemeType))
			{
				ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// TODO MR:- call API endpoint to log land & buildings
				//var landAndBuildingsData = new SchoolLandAndBuildings();
				//await _academisationCreationService.ApplicationSchoolLandAndBuildings(landAndBuildingsData, ApplicationId);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("LandAndBuildingsSummary", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::LandAndBuildingsModel::OnPostAsync::Exception - {Message}", ex.Message);
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
			// TODO MR:- populate other props from API - not implemented 18/08/2022
			//SchoolBuildLandOwnerExplained = ;
			//SchoolBuildLandWorksPlanned = ;
			//SchoolBuildLandWorksPlannedExplained = ;
			//SchoolBuildLandWorksPlannedDate = ;
			//SchoolBuildLandSharedFacilities = ;
			//SchoolBuildLandSharedFacilitiesExplained = ;
			//SchoolBuildLandGrants = ;
			//SchoolBuildLandGrantsBodies = ;
			//SchoolBuildLandPFIScheme = ;
			//SchoolBuildLandPFISchemeType = ;
			//SchoolBuildLandPriorityBuildingProgramme = ;
			//SchoolBuildLandFutureProgramme = ;
		}
	}
}
