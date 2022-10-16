using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationJoinTrustReasonsModel : BasePageEditModel
	{
		private readonly IConversionApplicationCreationService _academisationCreationService;
		private const string NextStepPage = "ApplicationChangeSchoolName";

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		//// MR:- VM props to capture pupil numbers data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string? ApplicationJoinTrustReason { get; set; } = string.Empty;

		public ApplicationJoinTrustReasonsModel(ILogger<ApplicationJoinTrustReasonsModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_academisationCreationService = academisationCreationService;
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

		public async Task<IActionResult> OnPostAsync()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = PopulateUpdateDictionary();
			await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}
			else
			{
				return true;
			}
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.ApplicationJoinTrustReason), ApplicationJoinTrustReason! }
			};
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			ApplicationJoinTrustReason = selectedSchool.ApplicationJoinTrustReason;
		}
	}
}
