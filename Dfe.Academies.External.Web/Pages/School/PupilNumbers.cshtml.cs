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
        private const string NextStepPage = "/SchoolOverview";

        //// MR:- selected school props for UI rendering
        public int SchoolId { get; private set; }

        public string SchoolName { get; private set; } = string.Empty;

        public string ApplicationReferenceNumber { get; private set; } = string.Empty;

        //// MR:- VM props to capture pupil numbers data
        public int SchoolCapacityPublishedAdmissionsNumber { get; set; }
        public int ProjectedPupilNumbersYear1 { get; set; }
        public int ProjectedPupilNumbersYear2 { get; set; }
        public int ProjectedPupilNumbersYear3 { get; set; }
        public string SchoolCapacityAssumptions { get; set; } = string.Empty;


        public PupilNumbersModel(ILogger<PupilNumbersModel> logger,
						        IConversionApplicationRetrievalService conversionApplicationRetrievalService,
						        IReferenceDataRetrievalService referenceDataRetrievalService,
                                IConversionApplicationCreationService academisationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
            _logger = logger;
            _academisationCreationService = academisationCreationService;
        }

        public async Task OnGetAsync()
        {
            // TODO MR:- grab existing pupil numbers
            try
            {
                //// on load - grab draft application from temp
                var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

                //// MR:- Need to drop into this pages cache here ready for post / server callback !
                TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
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
                // error messages component consumes ViewData["Errors"]
                PopulateValidationMessages();
                return Page();
            }

            try
            {
                //// grab draft application from temp= null
                var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

                // TODO MR:- call API endpoint to log pupil numbers
                //await _academisationCreationService.UpdateSchoolPupilNumbers(draftConversionApplication);

                // update temp store for next step - application overview
                TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

                return RedirectToPage(NextStepPage);
            }
            catch (Exception ex)
            {
                _logger.LogError("School::PupilNumbersModel::OnPostAsync::Exception - {Message}", ex.Message);
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
    }
}
