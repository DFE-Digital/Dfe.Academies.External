using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class PupilNumbersModel : BasePageEditModel
	{
		private readonly ILogger<PupilNumbersModel> _logger;
		private readonly IConversionApplicationCreationService _academisationCreationService;

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture pupil numbers data

		[BindProperty]
		[Required(ErrorMessage = "You must give the school's published admissions number (PAN)")]
		public int? SchoolCapacityPublishedAdmissionsNumber { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must give the projected pupil number for the academy's first year")]
		public int? ProjectedPupilNumbersYear1 { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must give the projected pupil number for the academy's second year")]
		public int? ProjectedPupilNumbersYear2 { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must give the projected pupil number for the academy's third year")]
		public int? ProjectedPupilNumbersYear3 { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must tell us what your projected pupil numbers are based on")]
		public string? SchoolCapacityAssumptions { get; set; } = string.Empty;


		public PupilNumbersModel(ILogger<PupilNumbersModel> logger,
								IConversionApplicationRetrievalService conversionApplicationRetrievalService,
								IReferenceDataRetrievalService referenceDataRetrievalService,
								IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
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
				_logger.LogError("School::PupilNumbersModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				await _academisationCreationService.ApplicationSchoolFuturePupilNumbers(
					ApplicationId,
					Urn,
					ProjectedPupilNumbersYear1.Value,
					ProjectedPupilNumbersYear2.Value,
					ProjectedPupilNumbersYear3.Value,
					SchoolCapacityAssumptions,
					SchoolCapacityPublishedAdmissionsNumber.Value
					);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("PupilNumbersSummary", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::PupilNumbersModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			SchoolCapacityPublishedAdmissionsNumber = selectedSchool.SchoolCapacityPublishedAdmissionsNumber;
			ProjectedPupilNumbersYear1 = selectedSchool.ProjectedPupilNumbersYear1;
			ProjectedPupilNumbersYear2 = selectedSchool.ProjectedPupilNumbersYear2;
			ProjectedPupilNumbersYear3 = selectedSchool.ProjectedPupilNumbersYear3;
			SchoolCapacityAssumptions = selectedSchool.SchoolCapacityAssumptions;
		}
	}
}
