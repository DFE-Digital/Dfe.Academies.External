using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class PupilNumbersModel : BaseSchoolPageEditModel
	{
		// MR:- VM props to capture pupil numbers data

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
		
		public PupilNumbersModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
								IReferenceDataRetrievalService referenceDataRetrievalService,
								IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "PupilNumbersSummary")
		{}

		//public async Task OnGetAsync(int urn, int appId)
		//{
		//	LoadAndStoreCachedConversionApplication();

		//	var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
		//	ApplicationId = appId;
		//	Urn = urn;

		//	// Grab other values from API
		//	if (selectedSchool != null)
		//	{
		//		PopulateUiModel(selectedSchool);
		//	}
		//}

		//public async Task<IActionResult> OnPostAsync()
		//{
		//	if (!RunUiValidation())
		//	{
		//		return Page();
		//	}

		//	// grab draft application from temp= null
		//	var draftConversionApplication =
		//		TempDataHelper.GetSerialisedValue<ConversionApplication>(
		//			TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		//	var dictionaryMapper = PopulateUpdateDictionary();
		//	await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

		//	// update temp store for next step - application overview
		//	TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

		//	return RedirectToPage("PupilNumbersSummary", new { appId = ApplicationId, urn = Urn });
		//}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

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
			// no radios / optional inputs on this form !!!

			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.ProjectedPupilNumbersYear1), ProjectedPupilNumbersYear1!.Value },
				{ nameof(SchoolApplyingToConvert.ProjectedPupilNumbersYear2), ProjectedPupilNumbersYear2!.Value },
				{ nameof(SchoolApplyingToConvert.ProjectedPupilNumbersYear3), ProjectedPupilNumbersYear3!.Value },
				{ nameof(SchoolApplyingToConvert.SchoolCapacityAssumptions), SchoolCapacityAssumptions! },
				{ nameof(SchoolApplyingToConvert.SchoolCapacityPublishedAdmissionsNumber), SchoolCapacityPublishedAdmissionsNumber!.Value },
			};
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolCapacityPublishedAdmissionsNumber = selectedSchool.SchoolCapacityPublishedAdmissionsNumber;
			ProjectedPupilNumbersYear1 = selectedSchool.ProjectedPupilNumbersYear1;
			ProjectedPupilNumbersYear2 = selectedSchool.ProjectedPupilNumbersYear2;
			ProjectedPupilNumbersYear3 = selectedSchool.ProjectedPupilNumbersYear3;
			SchoolCapacityAssumptions = selectedSchool.SchoolCapacityAssumptions;
		}
	}
}
