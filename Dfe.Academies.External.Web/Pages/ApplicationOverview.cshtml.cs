using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

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
                // TODO MR:- get AppId from cache
                //var ApplicationCacheValuesViewModel = ViewDataHelper.GetSerialisedValue<ApplicationCacheValuesViewModel>(nameof(ApplicationCacheValuesViewModel), ViewData) ?? new ApplicationCacheValuesViewModel();
                var conversionApplication = await LoadAndSetApplicationDetails(99);

                PopulateUiModel(conversionApplication);
            }
	        catch (Exception ex)
	        {
		        _logger.LogError("Application::ApplicationOverviewModel::OnGetAsync::Exception - {Message}", ex.Message);
	        }
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
