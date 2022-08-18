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

				// Grab other values from API
				if (selectedSchool != null)
				{
					// TODO MR:- grab data from API endpoint - applicationId && SchoolId combination !
					// land & buildings stored against the school ?????????????? not implemented 17/08/2022

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
			ApplicationId = selectedSchool.ApplicationId;
			Urn = selectedSchool.URN;
			SchoolName = selectedSchool.SchoolName;

			SchoolLandAndBuildingsSummaryHeadingViewModel heading1 = new(SchoolLandAndBuildingsSummaryHeadingViewModel.Heading,
				"/school/LandAndBuildings");

			// TODO MR:- if answer comes back from API, render data from API OR
			// Question Answer = QuestionAndAnswerConstants.NoInfoAnswer if string.IsNullOrWhiteSpace()

			// 7 sections / questions
			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.LandOwnership, "TBC"));

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PlannedBuildingWorks, "TBC")
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC"),
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC")
				}
			});

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.SharedFacilities, "TBC")
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC"),
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC")
				}
			});

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.Grants, "TBC")
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC"),
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC")
				}
			});

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PFI, "TBC")
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC"),
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC")
				}
			});

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PrioritySchoolBuildingProgram, "TBC")
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC"),
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC")
				}
			});

			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.BuildingSchoolsForTheFuture, "TBC")
			{
				SubQuestionAndAnswers = new()
				{
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC"),
					new SchoolLandAndBuildingsSummarySectionViewModel("", "TBC")
				}
			});

			var vm = new List<SchoolLandAndBuildingsSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
