using System;
using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LandAndBuildingsModel : BasePageEditModel
	{
		private readonly ILogger<LandAndBuildingsModel> _logger;
		private readonly IConversionApplicationCreationService _academisationCreationService;
		public string PlannedDateFormInputName = "sip_lbworksplanneddate";

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture land & buildings data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string SchoolBuildLandOwnerExplained { get; set; } = string.Empty;

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SchoolBuildLandWorksPlanned { get; set; }

		[BindProperty]
		public string? SchoolBuildLandWorksPlannedExplained { get; set; }

		[BindProperty]
		public string? WorksPlannedDate { get; set; }

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
					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::LandAndBuildingsModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			// MR:- try and build a date from component parts !!!
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PlannedDateFormInputName);
			var day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			var month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			var year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var plannedDate = BuildDateTime(day, month, year);

			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			if (SchoolBuildLandWorksPlanned == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandWorksPlannedExplained))
			{
				ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			if (SchoolBuildLandWorksPlanned == SelectOption.Yes && plannedDate == DateTime.MinValue)
			{
				ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			if (SchoolBuildLandSharedFacilities == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandSharedFacilitiesExplained))
			{
				ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			if (SchoolBuildLandGrants == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandGrantsBodies))
			{
				ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", "You must provide details");
				PopulateValidationMessages();
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			if (SchoolBuildLandPFIScheme == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandPFISchemeType))
			{
				ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", "You must provide details");
				PopulateValidationMessages();
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				var landAndBuildingsData = new SchoolLandAndBuildings(
					this.SchoolBuildLandOwnerExplained,
					this.SchoolBuildLandWorksPlanned == SelectOption.Yes,
					this.SchoolBuildLandWorksPlannedExplained,
					plannedDate,
					this.SchoolBuildLandSharedFacilities == SelectOption.Yes,
					this.SchoolBuildLandSharedFacilitiesExplained,
					this.SchoolBuildLandGrants == SelectOption.Yes,
					this.SchoolBuildLandGrantsBodies,
					this.SchoolBuildLandPFIScheme == SelectOption.Yes,
					this.SchoolBuildLandPFISchemeType,
					this.SchoolBuildLandPriorityBuildingProgramme == SelectOption.Yes,
					this.SchoolBuildLandFutureProgramme == SelectOption.Yes
				);

				var dictionaryMapper = new Dictionary<string, dynamic>
				{
					{ nameof(SchoolApplyingToConvert.LandAndBuildings), landAndBuildingsData }
				};
				
				await _academisationCreationService.PutSchoolApplicationDetails( ApplicationId, this.Urn, dictionaryMapper);

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

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// TODO
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			SchoolBuildLandOwnerExplained = selectedSchool.LandAndBuildings.OwnerExplained;
			SchoolBuildLandWorksPlanned = selectedSchool.LandAndBuildings.WorksPlanned.Value ? SelectOption.Yes : SelectOption.No;
			SchoolBuildLandWorksPlannedExplained = selectedSchool.LandAndBuildings.WorksPlannedExplained;
			WorksPlannedDate = (selectedSchool.LandAndBuildings.WorksPlannedDate.HasValue ?
				selectedSchool.LandAndBuildings.WorksPlannedDate.Value.ToString("dd/MM/yyyy")
				: string.Empty);
			SchoolBuildLandSharedFacilities = selectedSchool.LandAndBuildings.FacilitiesShared.Value ? SelectOption.Yes : SelectOption.No;
			SchoolBuildLandSharedFacilitiesExplained = selectedSchool.LandAndBuildings.FacilitiesSharedExplained;
			SchoolBuildLandGrants = selectedSchool.LandAndBuildings.Grants.Value ? SelectOption.Yes : SelectOption.No;
			SchoolBuildLandGrantsBodies = selectedSchool.LandAndBuildings.GrantsAwardingBodies;
			SchoolBuildLandPFIScheme = selectedSchool.LandAndBuildings.PartOfPFIScheme.Value ? SelectOption.Yes : SelectOption.No;
			SchoolBuildLandPFISchemeType = selectedSchool.LandAndBuildings.PartOfPFISchemeType;
			SchoolBuildLandPriorityBuildingProgramme = selectedSchool.LandAndBuildings.PartOfPrioritySchoolsBuildingProgramme.Value ? SelectOption.Yes : SelectOption.No;
			SchoolBuildLandFutureProgramme = selectedSchool.LandAndBuildings.PartOfBuildingSchoolsForFutureProgramme.Value ? SelectOption.Yes : SelectOption.No;
		}

		private void RePopDatePickerModel(string worksPlannedDateDay, string worksPlannedDateMonth, string worksPlannedDateYear)
		{
			WorksPlannedDateDay = worksPlannedDateDay;
			WorksPlannedDateMonth = worksPlannedDateMonth;
			WorksPlannedDateYear = worksPlannedDateYear;
		}
	}
}
