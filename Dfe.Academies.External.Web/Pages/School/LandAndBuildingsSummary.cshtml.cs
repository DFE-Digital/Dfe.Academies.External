using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LandAndBuildingsSummaryModel : BaseSchoolSummaryPageModel
	{
		//// MR:- VM props to show school conversion data
		public List<SchoolLandAndBuildingsSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public LandAndBuildingsSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
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
			// does not apply on this page
			return new();
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			var landAndBuildings = selectedSchool.LandAndBuildings;

			SchoolLandAndBuildingsSummaryHeadingViewModel heading1 = new(SchoolLandAndBuildingsSummaryHeadingViewModel.Heading,
				"/school/LandAndBuildings")
			{
				Status = landAndBuildings.WorksPlanned.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.LandOwnership,
				landAndBuildings.OwnerExplained ?? QuestionAndAnswerConstants.NoInfoAnswer)
			);

			// MR:- coming back as min date, need to check landAndBuildings.WorksPlannedDate.Value = DateTime.MinDate() !
			bool worksPlannedDateFilledIn = landAndBuildings.WorksPlannedDate.HasValue && landAndBuildings.WorksPlannedDate.Value != DateTime.MinValue;

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
						(worksPlannedDateFilledIn ?
							landAndBuildings.WorksPlannedDate.Value.ToString("dd/MM/yyyy") :
							QuestionAndAnswerConstants.NoAnswer)
					)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilities,
				landAndBuildings.FacilitiesShared.GetStringDescription()
			)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilitiesList,
						!string.IsNullOrWhiteSpace(landAndBuildings.FacilitiesSharedExplained) ?
							landAndBuildings.FacilitiesSharedExplained :
							QuestionAndAnswerConstants.NoAnswer)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.Grants,
				landAndBuildings.Grants.GetStringDescription()
			)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.GrantBodies,
						!string.IsNullOrWhiteSpace(landAndBuildings.GrantsAwardingBodies) ?
							landAndBuildings.GrantsAwardingBodies :
							QuestionAndAnswerConstants.NoAnswer)
				}
			});

			heading1.Sections.Add(new(
				SchoolLandAndBuildingsSummarySectionViewModel.PFI,
				landAndBuildings.PartOfPFIScheme.GetStringDescription()
			)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel(
						SchoolLandAndBuildingsSummarySectionViewModel.PFIKind,
						!string.IsNullOrWhiteSpace(landAndBuildings.PartOfPFISchemeType) ?
							landAndBuildings.PartOfPFISchemeType :
							QuestionAndAnswerConstants.NoAnswer)
				}
			});

			heading1.Sections.Add(new(
					SchoolLandAndBuildingsSummarySectionViewModel.PrioritySchoolBuildingProgram,
					landAndBuildings.PartOfPrioritySchoolsBuildingProgramme.GetStringDescription()
				)
			);

			heading1.Sections.Add(new(
					SchoolLandAndBuildingsSummarySectionViewModel.BuildingSchoolsForTheFuture,
					landAndBuildings.PartOfBuildingSchoolsForFutureProgramme.GetStringDescription()
				)
			);

			var vm = new List<SchoolLandAndBuildingsSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
