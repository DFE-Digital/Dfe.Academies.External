using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class PupilNumbersSummaryModel : BasePageEditModel
	{
		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to show school conversion data
		public List<SchoolPupilNumbersSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public PupilNumbersSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
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

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

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
