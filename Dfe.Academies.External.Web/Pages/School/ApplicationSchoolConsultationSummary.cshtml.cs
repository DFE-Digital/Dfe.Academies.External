using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationSchoolConsultationModelSummary : BasePageEditModel
	{
		private readonly ILogger<ApplicationSchoolConsultationModelSummary> _logger;

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to show school conversion data
		public List<SchoolConsultationSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationSchoolConsultationModelSummary(ILogger<ApplicationSchoolConsultationModelSummary> logger,
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
				_logger.LogError("School::ApplicationSchoolConsultationModelSummary::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

			SchoolConsultationSummaryHeadingViewModel heading1 = new(SchoolPupilNumbersSummaryHeadingViewModel.Heading,
				"/school/ApplicationSchoolConsultation");

			// TODO API:- 
			heading1.Sections.Add(new(SchoolConsultationSummarySectionViewModel.HasTheGoverningBodyConsulted,
				QuestionAndAnswerConstants.NoInfoAnswer)
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolConsultationSummarySectionViewModel(
						SchoolConsultationSummarySectionViewModel.WhenDoesTheGoverningBodyPlanToConsult,
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});

			var vm = new List<SchoolConsultationSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
