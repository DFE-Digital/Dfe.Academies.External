using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
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

			var landAndBuildings = selectedSchool.LandAndBuildings;

			SchoolLandAndBuildingsSummaryHeadingViewModel heading1 = new(SchoolLandAndBuildingsSummaryHeadingViewModel.Heading,
				"/school/LandAndBuildings")
			{
				Status = landAndBuildings.WorksPlanned.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.LandOwnership,
				landAndBuildings.OwnerExplained ?? QuestionAndAnswerConstants.NoAnswer)
			);

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorks,
				(landAndBuildings.WorksPlanned.GetStringDescription())
				)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorksDetails,
						(!string.IsNullOrWhiteSpace(landAndBuildings.WorksPlannedExplained) ?
							landAndBuildings.WorksPlannedExplained :
							QuestionAndAnswerConstants.NoAnswer)
					),
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorksWhen,
						(landAndBuildings.WorksPlannedDate.HasValue ?
							landAndBuildings.WorksPlannedDate.Value.ToString("dd/MM/yyyy") :
							QuestionAndAnswerConstants.NoAnswer)
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilities,
				(landAndBuildings.FacilitiesShared.GetStringDescription())
			)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilitiesList,
						landAndBuildings.FacilitiesSharedExplained ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.Grants,
				(landAndBuildings.Grants.GetStringDescription())
				)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.GrantBodies,
						landAndBuildings.GrantsAwardingBodies ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PFI,
				(landAndBuildings.PartOfPFIScheme.GetStringDescription())
				)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PFIKind,
						landAndBuildings.PartOfPFISchemeType ??
						QuestionAndAnswerConstants.NoAnswer)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PrioritySchoolBuildingProgram,
				(landAndBuildings.PartOfPrioritySchoolsBuildingProgramme.GetStringDescription())
				)
			);

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.BuildingSchoolsForTheFuture,
				(landAndBuildings.PartOfBuildingSchoolsForFutureProgramme.GetStringDescription())
				)
			);

			var vm = new List<SchoolLandAndBuildingsSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
