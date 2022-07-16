using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationOverviewModel : BasePageEditModel
    {
        private readonly ILogger<ApplicationOverviewModel> _logger;
        
        // Below are props for UI display, shunt over to separate view model?
        public string ApplicationTypeDescription { get; private set; } = string.Empty;

        public string ApplicationReferenceNumber { get; private set; } = string.Empty;

        public short CompletedSections { get; private set; }

        public short TotalNumberOfSections => 3;

        public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; private set; } = new();

        public string? NameOfTrustToJoin { get; private set; } 

        // overall application status
        public string ApplicationStatus { get; private set; } = string.Empty;

        public Status ConversionStatus { get; private set; }

        /// <summary>
        /// this will ONLY have a value IF ApplicationType = JoinAMat
        /// </summary>
        public SchoolApplyingToConvert? SchoolApplyingToConvert { get; private set; }

        /// <summary>
        /// to render submit button on UI
        /// </summary>
        //public bool UserHasSubmitApplicationRole { get; private set; } = false; // MR:- now in page base

        public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger, 
										IConversionApplicationRetrievalService conversionApplicationRetrievalService): base(conversionApplicationRetrievalService)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
	        try
	        {
		        //// on load - grab draft application from temp
		        var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		        //// MR:- Need to drop into this pages cache here ready for post / server callback !
		        TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
                var conversionApplication = await LoadAndSetApplicationDetails(draftConversionApplication.Id);

                PopulateUiModel(conversionApplication);
            }
	        catch (Exception ex)
	        {
		        _logger.LogError("Application::ApplicationOverviewModel::OnGetAsync::Exception - {Message}", ex.Message);
	        }
        }

        public async Task<IActionResult> OnPostAsync()
        {
	        // update temp store for next page
            return RedirectToPage("/SchoolOverview");
        }

        private void PopulateUiModel(ConversionApplication? conversionApplication)
        {
	        if (conversionApplication != null)
	        {
		        ApplicationTypeDescription = conversionApplication.ApplicationType.GetDescription();
		        ApplicationReferenceNumber = conversionApplication.ApplicationReference;
		        CompletedSections = 0; // TODO MR:- what logic drives this !
		        ApplicationStatus = "incomplete"; // TODO MR:- what logic drives this !
		        ConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
		        SchoolOrSchoolsApplyingToConvert = conversionApplication.SchoolOrSchoolsApplyingToConvert;
		        NameOfTrustToJoin = conversionApplication.TrustName;

                // MR:- we do below to be able to show the school application components status on this page
		        if (conversionApplication.ApplicationType == ApplicationTypes.JoinMat)
		        {
			        SchoolApplyingToConvert = conversionApplication.SchoolOrSchoolsApplyingToConvert.FirstOrDefault();
		        }
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
