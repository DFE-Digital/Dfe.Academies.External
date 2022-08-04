<<<<<<< HEAD
using Dfe.Academies.External.Web.Enums;
=======
>>>>>>> stubbing new page
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationChangeSchoolNameModel : BasePageEditModel
=======

namespace Dfe.Academies.External.Web.Pages.School
{
    public class ApplicationChangeSchoolNameModel : BasePageEditModel
>>>>>>> stubbing new page
	{
	    private readonly ILogger<ApplicationChangeSchoolNameModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    
	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; private set; }

<<<<<<< HEAD
		//// MR:- VM props to capture data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption ChangeName { get; set; }

		[BindProperty]
		public string? ChangeSchoolNameReason { get; set; }

		public bool ChangeSchoolNameReasonError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("ChangeSchoolNameReasonNotEntered"))
				{
					return true;
				}

				return false;
			}
		}
=======
		//// MR:- VM props to capture pupil numbers data
>>>>>>> stubbing new page

		public ApplicationChangeSchoolNameModel(ILogger<ApplicationChangeSchoolNameModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
		}

<<<<<<< HEAD
		public async Task OnGetAsync(int urn, int appId)
		{
			try
			{
				LoadAndStoreCachedConversionApplication();

				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

				// Grab other values from API
				if (selectedSchool != null)
				{
					// TODO MR:- grab existing school name change deets from API endpoint - applicationId && SchoolId combination !


					PopulateUiModel(selectedSchool);
				}
=======
		public async Task OnGetAsync()
		{
			try
			{
				// TODO
>>>>>>> stubbing new page
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationChangeSchoolNameModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

<<<<<<< HEAD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				return Page();
			}

			if (ChangeName == SelectOption.Yes && string.IsNullOrWhiteSpace(ChangeSchoolNameReason))
			{
				ModelState.AddModelError("ChangeSchoolNameReasonNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// MR:- save away ApplicationJoinTrustReason
				await _academisationCreationService.ApplicationChangeSchoolNameAndReason(draftConversionApplication, ChangeName, ChangeSchoolNameReason);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage(SchoolOverviewPath, new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationChangeSchoolNameModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

=======
>>>>>>> stubbing new page
		public override void PopulateValidationMessages()
		{
			ViewData["Errors"] = ConvertModelStateToDictionary();

			if (!ModelState.IsValid)
			{
				foreach (var modelStateError in ConvertModelStateToDictionary())
				{
					// MR:- add friendly message for validation summary
					if (!this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey(modelStateError.Key))
					{
						this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add(modelStateError.Key, modelStateError.Value);
					}
				}
			}
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			ApplicationId = selectedSchool.ApplicationId;
			Urn = selectedSchool.URN;
		}
	}
}
