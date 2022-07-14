using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationOverviewModel : BasePageEditModel
    {
        private readonly ILogger<ApplicationOverviewModel> _logger;
        private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;
        
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
        
        // List Of Audits
        public List<ViewModels.ApplicationAuditViewModel> Audits { get; private set; } = new();

        /// <summary>
        /// to render submit button on UI
        /// </summary>
        //public bool UserHasSubmitApplicationRole { get; private set; } = false; // MR:- now in page base

        public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger, IConversionApplicationRetrievalService conversionApplicationRetrievalService): base(conversionApplicationRetrievalService)
        {
            _logger = logger;
            _conversionApplicationRetrievalService = conversionApplicationRetrievalService;
        }

        public async Task OnGetAsync()
        {
	        try
	        {
		        var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		        // Grab other values from API
		        var auditEntries = await _conversionApplicationRetrievalService.GetConversionApplicationAuditEntries(draftConversionApplication.Id);

		        PopulateUiModel(auditEntries, draftConversionApplication);
            }
	        catch (Exception ex)
	        {
		        _logger.LogError("Application::ApplicationOverviewModel::OnGetAsync::Exception - {Message}", ex.Message);
	        }
        }

        private void PopulateUiModel(List<ConversionApplicationAuditEntry> auditEntries, ConversionApplication draftConversionApplication)
        {
            ApplicationTypeDescription = draftConversionApplication.ApplicationType.GetDescription();
            ApplicationReferenceNumber = draftConversionApplication.ApplicationReference;
            CompletedSections = 0; // TODO MR:- what logic drives this !
            ApplicationStatus = "incomplete"; // TODO MR:- what logic drives this !
            ConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
            SchoolOrSchoolsApplyingToConvert = draftConversionApplication.SchoolOrSchoolsApplyingToConvert;

            // Convert from List<ConversionApplicationAuditEntry> -> List<ViewModels.ApplicationAuditViewModel>
            Audits = auditEntries.Select(e => 
                new ViewModels.ApplicationAuditViewModel
                {
                    What =
                        $"{e.CreatedBy} {e.TypeOfChange} the {e.PropertyChanged}", // TODO MR:- re-work text when I can how this looks on screen !
                    When = e.DateCreated,
                    Who = e.CreatedBy
                }).ToList();
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
