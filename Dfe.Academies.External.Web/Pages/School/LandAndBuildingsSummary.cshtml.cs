using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class LandAndBuildingsSummaryModel : BasePageEditModel
	{
		private readonly ILogger<LandAndBuildingsSummaryModel> _logger;

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to show school conversion data
		public List<SchoolLandAndBuildingsSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public LandAndBuildingsSummaryModel(ILogger<LandAndBuildingsSummaryModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
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
				_logger.LogError("School::LandAndBuildingsSummaryModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

			SchoolLandAndBuildingsSummaryHeadingViewModel heading1 = new(SchoolLandAndBuildingsSummaryHeadingViewModel.Heading,
				"/school/LandAndBuildings")
			{
				Status = selectedSchool.LandAndBuildings.WorksPlanned.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.LandOwnership,
				selectedSchool.LandAndBuildings.OwnerExplained ?? "Not entered")
			);

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorks,
				(selectedSchool.LandAndBuildings.WorksPlanned.HasValue ?
					"Yes" :
					"No")
				)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorksDetails,
						(!string.IsNullOrWhiteSpace(selectedSchool.LandAndBuildings.WorksPlannedExplained) ?
							selectedSchool.LandAndBuildings.WorksPlannedExplained : 
							QuestionAndAnswerConstants.NoAnswer)
					),
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorksWhen,
						(selectedSchool.LandAndBuildings.WorksPlannedDate.HasValue ?
							selectedSchool.LandAndBuildings.WorksPlannedDate.Value.ToString("dd/MM/yyyy") : 
							QuestionAndAnswerConstants.NoAnswer)
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilities,
				(selectedSchool.LandAndBuildings.FacilitiesShared.HasValue ?
					"Yes" :
					"No")
			)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilitiesList,
						selectedSchool.LandAndBuildings.FacilitiesSharedExplained ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.Grants,
				(selectedSchool.LandAndBuildings.Grants.HasValue ?
					"Yes" :
					"No")
				)
				{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.GrantBodies, 
						selectedSchool.LandAndBuildings.GrantsAwardingBodies ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PFI,
				(selectedSchool.LandAndBuildings.PartOfPFIScheme.HasValue ?
					"Yes" :
					"No")
				)
				{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PFIKind,
						selectedSchool.LandAndBuildings.PartOfPFISchemeType ??
						QuestionAndAnswerConstants.NoAnswer)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PrioritySchoolBuildingProgram,
				(selectedSchool.LandAndBuildings.PartOfPrioritySchoolsBuildingProgramme.HasValue ?
					"Yes" :
					"No")
				)
			);

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.BuildingSchoolsForTheFuture,
				(selectedSchool.LandAndBuildings.PartOfBuildingSchoolsForFutureProgramme.HasValue ?
					"Yes" :
					"No")
				)
			);

			var vm = new List<SchoolLandAndBuildingsSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
