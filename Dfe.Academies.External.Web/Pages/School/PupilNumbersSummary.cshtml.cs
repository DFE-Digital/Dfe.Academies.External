﻿using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class PupilNumbersSummaryModel : BaseSchoolSummaryPageModel
	{
		// MR:- VM props to show school conversion data
		public List<SchoolPupilNumbersSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationStatus ApplicationStatus { get; private set;}

		public PupilNumbersSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
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
			var applicationDetails = ConversionApplicationRetrievalService.GetApplication(ApplicationId).Result;
		    ApplicationStatus = applicationDetails.ApplicationStatus;

			SchoolPupilNumbersSummaryHeadingViewModel heading1 = new(
				SchoolPupilNumbersSummaryHeadingViewModel.Heading,
				"/school/PupilNumbers")
			{
				Status = selectedSchool.ProjectedPupilNumbersYear1.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(
				new(
					SchoolPupilNumbersSummarySectionViewModel.PupilNumberYr1,
					selectedSchool.ProjectedPupilNumbersYear1?.ToString() ?? QuestionAndAnswerConstants.NoInfoAnswer
					)
				);
			heading1.Sections.Add(
				new(
					SchoolPupilNumbersSummarySectionViewModel.PupilNumberYr2,
					selectedSchool.ProjectedPupilNumbersYear2?.ToString() ?? QuestionAndAnswerConstants.NoInfoAnswer
					)
				);
			heading1.Sections.Add(
				new(
					SchoolPupilNumbersSummarySectionViewModel.PupilNumberYr3,
					selectedSchool.ProjectedPupilNumbersYear3?.ToString() ?? QuestionAndAnswerConstants.NoInfoAnswer
					)
				);
			heading1.Sections.Add(
				new(
					SchoolPupilNumbersSummarySectionViewModel.NumbersBasedUpon,
					selectedSchool.SchoolCapacityAssumptions ?? QuestionAndAnswerConstants.NoInfoAnswer
					)
				);
			heading1.Sections.Add(
				new(
					SchoolPupilNumbersSummarySectionViewModel.PAN,
					selectedSchool.SchoolCapacityPublishedAdmissionsNumber?.ToString() ?? QuestionAndAnswerConstants.NoInfoAnswer
					)
				);

			var vm = new List<SchoolPupilNumbersSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
