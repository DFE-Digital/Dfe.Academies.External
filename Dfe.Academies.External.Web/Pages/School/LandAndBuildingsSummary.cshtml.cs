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
			// consume QuestionAndAnswerConstants.NoInfoAnswer if string.IsNullOrWhiteSpace()

			// 7 sections / questions
			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PupilNumberYr1, "TBC"));
			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PupilNumberYr2, "TBC"));
			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PupilNumberYr3, "TBC"));
			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.NumbersBasedUpon, "TBC"));
			heading1.Sections.Add(new(SchoolLandAndBuildingsSummarySectionViewModel.PAN, "TBC"));

			var vm = new List<SchoolLandAndBuildingsSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
