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
				var conversionApplication = await LoadAndSetApplicationDetails(appId);
				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

                // Grab other values from API
                if (selectedSchool != null)
                {
	                selectedSchool.SchoolApplicationComponents = await ConversionApplicationRetrievalService
		                .GetSchoolApplicationComponents(appId, urn);

	                PopulateUiModel(selectedSchool, conversionApplication);
                }
	        }
	        catch (Exception ex)
	        {
		        _logger.LogError("Application::SchoolOverviewModel::OnGetAsync::Exception - {Message}", ex.Message);
            }
        }

        private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ConversionApplication? application)
        {
	        URN = selectedSchool.URN;
            SchoolName = selectedSchool.SchoolName;

            ApplicationType = application.ApplicationType;
			SchoolComponentsViewModel componentsVm = new()
            {
	            URN = selectedSchool.URN,
	            ApplicationId = application.ApplicationId,
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
