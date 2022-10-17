using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationSchoolConsultationModelSummary : BaseSchoolSummaryPageModel
	{
		// MR:- VM props to show school consultation data
		public List<SchoolConsultationSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationSchoolConsultationModelSummary(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
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
			SchoolConsultationSummaryHeadingViewModel heading1 = new(SchoolPupilNumbersSummaryHeadingViewModel.Heading,
				"/school/ApplicationSchoolConsultation")
			{
				Status = selectedSchool.SchoolHasConsultedStakeholders.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};


			heading1.Sections.Add(new(SchoolConsultationSummarySectionViewModel.HasTheGoverningBodyConsulted,
				selectedSchool.SchoolHasConsultedStakeholders.GetStringDescription())
			{
				// MR:- no sub question showing on screen shot from Abi
				//SubQuestionAndAnswers = new()
				//{
				//	new SchoolConsultationSummarySectionViewModel(
				//		SchoolConsultationSummarySectionViewModel.WhenDoesTheGoverningBodyPlanToConsult,
				//		selectedSchool.SchoolPlanToConsultStakeholders ??
				//		QuestionAndAnswerConstants.NoAnswer
				//	)
				//}
			});

			var vm = new List<SchoolConsultationSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
