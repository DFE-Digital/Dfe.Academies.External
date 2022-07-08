using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationOverviewModel : BasePageModel
    {
        private readonly ILogger<ApplicationOverviewModel> _logger;
        private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;
        private ConversionApplication _draftConversionApplication;

        // Below are props for UI display, shunt over to separate view model?
        public string ApplicationTypeDescription { get; private set; }

        public string ApplicationReferenceNumber { get; private set; }

        public short CompletedSections { get; private set; }

        public short TotalNumberOfSections => 8;

        /// <summary>
        /// comma separated list<schools>?
        /// </summary>
        public string SchoolApplyingToConvert { get; set; }

        public string NameOfTrustToJoin { get; set; }

        // about the conversion - overall application status
        public Status ApplicationStatus { get; private set; }

        public List<ViewModels.ApplicationComponentViewModel> Components { get; set; } = new();

        // List of contributors
        public List<ViewModels.ConversionApplicationContributorViewModel> Contributors { get; set; } = new();

        // List Of Audits
        public List<ViewModels.ApplicationAuditViewModel> Audits { get; set; } = new();

        /// <summary>
        /// to render submit button on UI
        /// </summary>
        public bool DoesUserHaveSubmitRole { get; private set; }

        public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger, IConversionApplicationRetrievalService conversionApplicationRetrievalService)
        {
            _logger = logger;
            _conversionApplicationRetrievalService = conversionApplicationRetrievalService;
            _draftConversionApplication = new ConversionApplication();
            ApplicationTypeDescription = string.Empty;
            ApplicationReferenceNumber = string.Empty;
            CompletedSections = 0;
            SchoolApplyingToConvert = string.Empty;
            NameOfTrustToJoin = string.Empty;
            DoesUserHaveSubmitRole = false;
        }

        public async Task OnGetAsync()
        {
             _draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

            // Grab other values from API
            var auditEntries = await _conversionApplicationRetrievalService.GetConversionApplicationAuditEntries(_draftConversionApplication.Id);
            _draftConversionApplication.ConversionApplicationComponents = await _conversionApplicationRetrievalService
                .GetConversionApplicationComponentStatuses(_draftConversionApplication.Id);
            var conversionApplicationContributors = await _conversionApplicationRetrievalService
                .GetConversionApplicationContributors(_draftConversionApplication.Id);

            PopulateUiModel(auditEntries, conversionApplicationContributors);
        }

        private void PopulateUiModel(List<ConversionApplicationAuditEntry> auditEntries,
            List<ConversionApplicationContributor> conversionApplicationContributors)
        {
            ApplicationTypeDescription = _draftConversionApplication.ApplicationType.GetDescription();
            ApplicationReferenceNumber = $"A2B_{_draftConversionApplication.Id}";
            CompletedSections = 3;
            ApplicationStatus =_draftConversionApplication.ApplicationStatus;

            SchoolApplyingToConvert = _draftConversionApplication.SchoolOrSchoolsApplyingToConvert.Count == 0 ? "No school selected" 
                : string.Join(",", _draftConversionApplication.SchoolOrSchoolsApplyingToConvert);

            NameOfTrustToJoin = _draftConversionApplication.TrustName ?? "No trust selected";

            // Convert from List<ConversionApplicationAuditEntry> -> List<ViewModels.ApplicationAuditViewModel>
            Audits = auditEntries.Select(e => 
                new ViewModels.ApplicationAuditViewModel
                {
                    What =
                        $"{e.CreatedBy} {e.TypeOfChange} the {e.PropertyChanged}", // TODO MR:- re-work text when I can how this looks on screen !
                    When = e.DateCreated,
                    Who = e.CreatedBy
                }).ToList();

            // Convert from List<ConversionApplicationContributor> -> List<ViewModels.ConversionApplicationContributorViewModel>
            Contributors = conversionApplicationContributors.Select(c => 
                new ViewModels.ConversionApplicationContributorViewModel 
                {
                    Name = c.Name,
                    Role = c.Role
                }).ToList();

            // Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
            Components = _draftConversionApplication.ConversionApplicationComponents.Select(c =>
                new ViewModels.ApplicationComponentViewModel
                {
                    Name = c.Name,
                    Status = c.Status,
                    URI = SetApplicationComponentUriFromName(c.Name)
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

        private string SetApplicationComponentUriFromName(string componentName)
        {
            switch (componentName.ToLower().Trim())
            {
                case "contact details":
                    return "/ApplicationSchoolContactDetails";
                case "performance and safeguarding":
                    return "/ApplicationSchoolPerformanceAndSafeguarding";
                case "pupil numbers":
                    return "/ApplicationSchoolPupilNumbers";
                case "finances":
                    return "/ApplicationSchoolFinances";
                case "partnerships and affiliations":
                    return "/ApplicationSchoolPartnershipsAndAffliates";
                case "religious education":
                    return "/ApplicationSchoolReligiousEducation";
                case "land and buildings":
                    return "/ApplicationSchoolLandAndBuildings";
                case "local authority":
                    return "/ApplicationSchoolLocalAuthority";
                default:
                    return string.Empty; 
            }
        }
    }
}
