using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationChangeSchoolNameModel : BasePageEditModel
	{
		private readonly ILogger<ApplicationChangeSchoolNameModel> _logger;
		private readonly IConversionApplicationCreationService _academisationCreationService;

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		//// MR:- VM props to capture data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption ChangeName { get; set; }

		[BindProperty]
		public string? ChangeSchoolName { get; set; }

		public bool ChangeSchoolNameError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("ChangeSchoolNameNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public ApplicationChangeSchoolNameModel(ILogger<ApplicationChangeSchoolNameModel> logger,
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
				_logger.LogError("School::ApplicationChangeSchoolNameModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			if (ChangeName == SelectOption.Yes && string.IsNullOrWhiteSpace(ChangeSchoolName))
			{
				ModelState.AddModelError("ChangeSchoolNameNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// MR:- save away ApplicationJoinTrustReason
				await _academisationCreationService.ApplicationChangeSchoolNameAndReason(ApplicationId, ChangeName, ChangeSchoolName, Urn);

				// update temp store for next step - application overview as last step in process
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("SchoolConversionKeyDetails", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationChangeSchoolNameModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			var conversionChangeNamePlanned = selectedSchool.ConversionChangeNamePlanned.GetEnumValue();

			if (conversionChangeNamePlanned.HasValue)
			{
				ChangeName = conversionChangeNamePlanned.Value;
			}

			ChangeSchoolName = selectedSchool.ProposedNewSchoolName;
		}
	}
}
