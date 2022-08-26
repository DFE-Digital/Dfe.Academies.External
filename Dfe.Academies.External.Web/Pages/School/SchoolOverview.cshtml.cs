using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class SchoolOverviewModel : BasePageEditModel
    {
	    private readonly ILogger<SchoolOverviewModel> _logger;

	    public int URN { get; set; }

        public string SchoolName { get; private set; } = string.Empty;

        public ApplicationTypes ApplicationType { get; private set; }

        public SchoolComponentsViewModel SchoolComponents { get; private set; } = new();

        public SchoolOverviewModel(ILogger<SchoolOverviewModel> logger, 
									IConversionApplicationRetrievalService conversionApplicationRetrievalService,
									IReferenceDataRetrievalService referenceDataRetrievalService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
	        _logger = logger;
        }

        public async Task OnGetAsync(int urn, int appId)
        {
	        try
	        {
		        //// on load - grab draft application from temp
		        var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		        //// MR:- Need to drop into this pages cache here ready for post / server callback !
		        TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
		        //var conversionApplication = await LoadAndSetApplicationDetails(draftConversionApplication.ApplicationId);

                // var schoolCacheViewModel = ViewDataHelper.GetSerialisedValue<SchoolCacheValuesViewModel>(nameof(SchoolCacheValuesViewModel), ViewData) ?? new SchoolCacheValuesViewModel();
                var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

                // Grab other values from API
                if (selectedSchool != null)
                {
	                selectedSchool.SchoolApplicationComponents = await ConversionApplicationRetrievalService
		                .GetSchoolApplicationComponents(appId, urn);

	                PopulateUiModel(selectedSchool);
                }
	        }
	        catch (Exception ex)
	        {
		        _logger.LogError("Application::SchoolOverviewModel::OnGetAsync::Exception - {Message}", ex.Message);
            }
        }

        private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
        {
            // MR:- below equals cached ApplicationReferenceNumber
            var applicationCacheViewModel = ViewDataHelper.GetSerialisedValue<ApplicationCacheValuesViewModel>(TempDataHelper.DraftConversionApplicationKey, ViewData) ?? new ApplicationCacheValuesViewModel();

            ApplicationType = applicationCacheViewModel.ApplicationType;
            URN = selectedSchool.URN;
            SchoolName = selectedSchool.SchoolName;

            SchoolComponentsViewModel componentsVm = new()
            {
	            URN = selectedSchool.URN,
	            ApplicationId = applicationCacheViewModel.ApplicationId,
	            // Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
	            SchoolComponents = selectedSchool.SchoolApplicationComponents.Select(c =>
		            new ApplicationComponentViewModel(name: c.Name,
			            uri: SetSchoolApplicationComponentUriFromName(c.Name))
		            {
			            Status = c.Status
		            }).ToList()
            };

            SchoolComponents = componentsVm;
        }

        public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
		}
    }
}
