using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationJoinTrustReasonsModel : BasePageEditModel
	{
	    private readonly ILogger<ApplicationJoinTrustReasonsModel> _logger;
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
		    _logger = logger;
		    _academisationCreationService = academisationCreationService;
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
				    // TODO MR:- grab existing reasons for joining from API endpoint - applicationId && SchoolId combination !


				    PopulateUiModel(selectedSchool);
			    }
		    }
		    catch (Exception ex)
		    {
			    _logger.LogError("School::ApplicationJoinTrustReasonsModel::OnGetAsync::Exception - {Message}", ex.Message);
		    }
	    }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // error messages component consumes ViewData["Errors"]
                PopulateValidationMessages();
                return Page();
            }

            try
            {
                //// grab draft application from temp= null
                var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

                // MR:- save away ApplicationJoinTrustReason
                await _academisationCreationService.ApplicationAddJoinTrustReasons(ApplicationId, ApplicationJoinTrustReason, Urn);

                // update temp store for next step
                TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

                return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn});
            }
            catch (Exception ex)
            {
                _logger.LogError("School::ApplicationJoinTrustReasonsModel::OnPostAsync::Exception - {Message}", ex.Message);
                return Page();
            }
        }

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
