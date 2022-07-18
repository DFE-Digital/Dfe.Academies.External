using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationOverviewModel : BasePageEditModel
    {
        private readonly ILogger<ApplicationOverviewModel> _logger;
        private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

        // Below are props for UI display, shunt over to separate view model?
        public ApplicationTypes ApplicationType { get; private set; }

        public string ApplicationReferenceNumber { get; private set; } = string.Empty;

        public short CompletedSections { get; private set; }

        public short TotalNumberOfSections => 3;

        public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; private set; } = new();

        public string? NameOfTrustToJoin { get; private set; } 

        // overall application status
        public string ApplicationStatus { get; private set; } = string.Empty;

        public Status ConversionStatus { get; private set; }

        public string SchoolHeaderText { get; private set; } = string.Empty;

        /// <summary>
        /// this will ONLY have a value IF ApplicationType = FormNewMat OR FormNewSingleAcademyTrust
        /// </summary>
        public string? SchoolName { get; private set; }

        /// <summary>
        /// this will ONLY have a value IF ApplicationType = FormNewMat OR FormNewSingleAcademyTrust
        /// </summary>
        public List<ViewModels.ApplicationComponentViewModel>? SchoolComponents { get; private set; }

        public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger, 
										IConversionApplicationRetrievalService conversionApplicationRetrievalService): base(conversionApplicationRetrievalService)
        {
            _logger = logger;
            _conversionApplicationRetrievalService = conversionApplicationRetrievalService;
        }

        public async Task OnGetAsync()
        {
	        try
	        {
		        //// on load - grab draft application from temp
		        var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		        //// MR:- Need to drop into this pages cache here ready for post / server callback !
		        TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
                var conversionApplication = await LoadAndSetApplicationDetails(draftConversionApplication.Id, draftConversionApplication.ApplicationType);

                if (conversionApplication != null)
                {
	                var school = conversionApplication.SchoolOrSchoolsApplyingToConvert.FirstOrDefault();

	                if (school != null)
	                {
		                school.SchoolApplicationComponents =
			                await _conversionApplicationRetrievalService.GetSchoolApplicationComponents(school.SchoolId);
                    }

	                PopulateUiModel(conversionApplication, school);
                }
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

        private void PopulateUiModel(ConversionApplication? conversionApplication, SchoolApplyingToConvert? school)
        {
	        if (conversionApplication != null)
	        {
		        ApplicationType = conversionApplication.ApplicationType;
		        ApplicationReferenceNumber = conversionApplication.ApplicationReference;
		        CompletedSections = 0; // TODO MR:- what logic drives this !
		        ApplicationStatus = "incomplete"; // TODO MR:- what logic drives this !
		        ConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
		        SchoolOrSchoolsApplyingToConvert = conversionApplication.SchoolOrSchoolsApplyingToConvert;
		        NameOfTrustToJoin = conversionApplication.TrustName;

		        if (conversionApplication.ApplicationType == ApplicationTypes.FormNewMat)
		        {
			        SchoolHeaderText = "The schools applying to convert";
                }
		        else
		        {
			        SchoolHeaderText = "The school applying to convert";
			        SchoolName = conversionApplication.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolName;
                    // Convert from List<ConversionApplicationAuditEntry> -> List<ViewModels.ApplicationAuditViewModel>
                    //Audits = auditEntries.Select(e =>
                    // new ViewModels.ApplicationAuditViewModel
                    // {
                    //  What =
                    //   $"{e.CreatedBy} {e.TypeOfChange} the {e.PropertyChanged}", // TODO MR:- re-work text when I can how this looks on screen !
                    //  When = e.DateCreated,
                    //  Who = e.CreatedBy
                    // }).ToList();
		        }

		        // Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
		        if (school != null)
			        SchoolComponents = school.SchoolApplicationComponents.Select(c =>
				        new ViewModels.ApplicationComponentViewModel(name: c.Name,
					        uri: SetSchoolApplicationComponentUriFromName(c.Name))
				        {
					        Status = c.Status
				        }).ToList();
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
