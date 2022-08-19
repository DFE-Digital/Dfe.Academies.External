using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationOverviewModel : BasePageEditModel
    {
        private readonly ILogger<ApplicationOverviewModel> _logger;
        private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

        public int ApplicationId { get; private set; }

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

        public string TrustHeaderText  { get; private set; } = string.Empty;

        /// <summary>
        /// Always have a trust conversion status whether Join a MAT or form a MAT !!
        /// </summary>
        public Status TrustConversionStatus { get; private set; }

        /// <summary>
        /// this will ONLY have a value IF ApplicationType = FormNewMat OR FormNewSingleAcademyTrust
        /// </summary>
        public SchoolComponentsViewModel SchoolComponents { get; private set; } = new();

        public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger, 
										IConversionApplicationRetrievalService conversionApplicationRetrievalService,
										IReferenceDataRetrievalService referenceDataRetrievalService
        ) : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
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
                var conversionApplication = await LoadAndSetApplicationDetails(draftConversionApplication.ApplicationId);

                if (conversionApplication != null)
                {
	                var school = conversionApplication.Schools.FirstOrDefault();

	                if (school != null)
	                {



                        school.SchoolApplicationComponents =
			                await _conversionApplicationRetrievalService.GetSchoolApplicationComponents(draftConversionApplication.ApplicationId, school.URN);
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
		        ApplicationId = conversionApplication.ApplicationId;
                ApplicationType = conversionApplication.ApplicationType;
		        ApplicationReferenceNumber = conversionApplication.ApplicationReference;
		        CompletedSections = 0; // TODO MR:- what logic drives this !
		        ApplicationStatus = "incomplete"; // TODO MR:- what logic drives this !
		        ConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
		        SchoolOrSchoolsApplyingToConvert = conversionApplication.Schools;
		        NameOfTrustToJoin = conversionApplication.TrustName;

		        if (conversionApplication.ApplicationType == ApplicationTypes.FormNewMat)
		        {
			        TrustHeaderText = "The trust being formed";
                    SchoolHeaderText = "The schools applying to convert";
			        TrustConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
                }
		        else
		        {
					TrustHeaderText = "The trust the school will join";
					SchoolHeaderText = "The school applying to convert";
					SchoolName = school?.SchoolName;
					TrustConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !

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
		        {
			        SchoolComponentsViewModel componentsVm = new() 
			        {
				        URN = school.URN,
				        ApplicationId = conversionApplication.ApplicationId,
				        SchoolComponents = school.SchoolApplicationComponents.Select(c =>
					        new ApplicationComponentViewModel(name: c.Name,
						        uri: SetSchoolApplicationComponentUriFromName(c.Name))
					        {
						        Status = c.Status
					        }).ToList()
			        };

			        SchoolComponents = componentsVm;
		        }
	        }
        }

        public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
		}
    }
}
