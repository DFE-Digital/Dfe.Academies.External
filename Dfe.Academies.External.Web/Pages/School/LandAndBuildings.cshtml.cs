using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LandAndBuildingsModel : BaseSchoolPageEditModel
	{
		public string PlannedDateFormInputName = "sip_lbworksplanneddate";

		// MR:- VM props to capture land & buildings data
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
				return !ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandWorksPlannedExplainedNotEntered");
			}
		}

		public bool SchoolBuildLandWorksPlannedDateError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandWorksPlannedDateNotEntered");
			}
		}

		public bool SchoolBuildLandSharedFacilitiesExplainedError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandSharedFacilitiesExplainedNotEntered");
			}
		}

		public bool SchoolBuildLandGrantsBodiesError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandGrantsBodiesNotEntered");
			}
		}

		public bool SchoolBuildLandPFISchemeTypeError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("SchoolBuildLandPFISchemeTypeNotEntered");
			}
		}

		public DateTime? WorksPlannedDateLocal { get; set; }

		public LandAndBuildingsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "LandAndBuildingsSummary")
		{}

		/// <summary>
		/// different overload because of datepicker stuff!!
		/// </summary>
		/// <returns></returns>
		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;

			// MR:- try and build a date from component parts !!!
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PlannedDateFormInputName);
			string day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			WorksPlannedDateLocal = BuildDateTime(day, month, year);

			if (!RunUiValidation())
			{
				RePopDatePickerModel(day, month, year);
				return Page();
			}
			
			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = PopulateUpdateDictionary();
			await ConversionApplicationCreationService.PutSchoolApplicationDetails( ApplicationId, this.Urn, dictionaryMapper);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			if (SchoolBuildLandWorksPlanned == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandWorksPlannedExplained))
			{
				ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (SchoolBuildLandWorksPlanned == SelectOption.Yes && WorksPlannedDateLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				return false;
			}

			if (SchoolBuildLandSharedFacilities == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandSharedFacilitiesExplained))
			{
				ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (SchoolBuildLandGrants == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandGrantsBodies))
			{
				ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (SchoolBuildLandPFIScheme == SelectOption.Yes && string.IsNullOrWhiteSpace(SchoolBuildLandPFISchemeType))
			{
				ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", "You must provide details");
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
			// if this.SchoolBuildLandWorksPlanned == 'no', blank out 'SchoolBuildLandWorksPlannedExplained' && works planned date
			if (this.SchoolBuildLandWorksPlanned == SelectOption.No)
			{
				this.SchoolBuildLandWorksPlannedExplained = null;
				WorksPlannedDateLocal = null;
			}

			// if this.SchoolBuildLandSharedFacilities == 'no', blank out 'SchoolBuildLandSharedFacilitiesExplained'
			if (this.SchoolBuildLandSharedFacilities == SelectOption.No)
			{
				this.SchoolBuildLandSharedFacilitiesExplained = null;
			}

			// if this.SchoolBuildLandPFIScheme == 'no', blank out 'SchoolBuildLandPFISchemeType'
			if (this.SchoolBuildLandPFIScheme == SelectOption.No)
			{
				this.SchoolBuildLandPFISchemeType = null;
			}

			var landAndBuildingsData = new SchoolLandAndBuildings(
				this.SchoolBuildLandOwnerExplained,
				this.SchoolBuildLandWorksPlanned == SelectOption.Yes,
				this.SchoolBuildLandWorksPlannedExplained,
				WorksPlannedDateLocal,
				this.SchoolBuildLandSharedFacilities == SelectOption.Yes,
				this.SchoolBuildLandSharedFacilitiesExplained,
				this.SchoolBuildLandGrants == SelectOption.Yes,
				this.SchoolBuildLandGrantsBodies,
				this.SchoolBuildLandPFIScheme == SelectOption.Yes,
				this.SchoolBuildLandPFISchemeType,
				this.SchoolBuildLandPriorityBuildingProgramme == SelectOption.Yes,
				this.SchoolBuildLandFutureProgramme == SelectOption.Yes
			);

			return new Dictionary<string, dynamic> { { nameof(SchoolApplyingToConvert.LandAndBuildings), landAndBuildingsData } };
		}

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
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
