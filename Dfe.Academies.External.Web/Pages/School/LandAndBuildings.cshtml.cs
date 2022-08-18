using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static GovUk.Frontend.AspNetCore.ComponentDefaults;

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
		public SelectOption SchoolBuildLandSharedFacilities { get; set; }

		[BindProperty]
		public string? SchoolBuildLandSharedFacilitiesExplained { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandWorksPlanned { get; set; } 

		[BindProperty]
		public string? SchoolBuildLandWorksPlannedExplained { get; set; }

		[BindProperty]
		public DateTime? SchoolBuildLandWorksPlannedDate { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandGrants { get; set; } 

		[BindProperty]
		public string? SchoolBuildLandGrantsBody { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandPriorityBuildingProgramme { get; set; } 

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandFutureProgramme { get; set; } 

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandPFIScheme { get; set; } 

		[BindProperty]
		public string? SchoolBuildLandPFISchemeType { get; set; }

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

				// Grab other values from API
				if (selectedSchool != null)
				{
					// TODO MR:- grab existing pupil numbers from API endpoint to populate VM - applicationId && SchoolId combination !
					// land & buildings stored against the school ?????????????? not implemented 17/08/2022

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

			// TODO MR:- conditional radio validation !
			// if

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// TODO MR:- call API endpoint to log land & buildings
				//var landAndBuildingsData = new SchoolLandAndBuildings();
				//await _academisationCreationService.ApplicationSchoolLandAndBuildings(landAndBuildingsData);

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
			ApplicationId = selectedSchool.ApplicationId;
			Urn = selectedSchool.URN;
			SchoolName = selectedSchool.SchoolName;

			// TODO MR:- populate other props
			//string? SchoolBuildLandOwnerExplained,
			//bool? SchoolBuildLandSharedFacilities, // should this be y/n enum ??
			//bool? SchoolBuildLandWorksPlanned,  // should this be y/n enum ??
			//string? SchoolBuildLandWorksPlannedExplained,
			//	DateTime? SchoolBuildLandWorksPlannedDate,
			//bool? SchoolBuildLandGrants, // should this be y/n enum ??
			//string? SchoolBuildLandGrantsBody,
			//bool? SchoolBuildLandPriorityBuildingProgramme, // should this be y/n enum ??
			//bool? SchoolBuildLandFutureProgramme, // should this be y/n enum ??
			//bool? SchoolBuildLandPFIScheme, // should this be y/n enum ??
			//string? SchoolBuildLandPFISchemeType
		}
	}
}
