using System.Collections;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationOverviewModel : BasePageModel
    {
        private readonly ILogger<ApplicationOverviewModel> _logger;
        private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;
        private ConversionApplication _draftConversionApplication;

        // Below are props for UI display, shunt over to separate view model?
        public string ApplicationTypeDescription { get; set; }

        public string ApplicationReferenceNumber { get; set; }

        public short CompletedSections { get; set; }

        public short TotalNumberOfSections => 8;

        /// <summary>
        /// comma separated list<schools>?
        /// </summary>
        public string SchoolApplyingToConvert { get; set; }

        public string NameOfTrustToJoin { get; set; }

        // about the conversion ?
        public ApplicationComponentsStatus ConversionStatus { get; set; }

        // list of contributors

        // List Of Audits
        public List<ViewModels.ApplicationAuditViewModel> ApplicationAudits { get; set; }

        /// <summary>
        /// to render submit button on UI
        /// </summary>
        public bool DoesUserHaveSubmitRole { get; set; }

        public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger, IConversionApplicationRetrievalService conversionApplicationRetrievalService)
        {
            _logger = logger;
            _conversionApplicationRetrievalService = conversionApplicationRetrievalService;
        }

        public async Task OnGetAsync()
        {
             _draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();

            // Grab other values from API e.g. Audit entries
            var auditEntries = await _conversionApplicationRetrievalService.GetConversionApplicationAuditEntries(_draftConversionApplication.Id);

            PopulateUiModel(auditEntries);
        }

        private void PopulateUiModel(List<ConversionApplicationAuditEntry> auditEntries)
        {
            ApplicationTypeDescription = _draftConversionApplication.ApplicationType.GetDescription();
            ApplicationReferenceNumber = _draftConversionApplication.Id.ToString();
            CompletedSections = 3;
            SchoolApplyingToConvert = string.Join(",", _draftConversionApplication.SchoolOrSchoolsApplyingToConvert);
            NameOfTrustToJoin = _draftConversionApplication.TrustName ?? string.Empty;

            // convert from list<ConversionApplicationAuditEntry> -> list<ApplicationAuditViewModel>
            //auditEntries.
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
