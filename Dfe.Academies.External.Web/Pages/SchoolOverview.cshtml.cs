using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages
{
    public class SchoolOverviewModel : BasePageEditModel
    {
	    private readonly ILogger<SchoolOverviewModel> _logger;
	    private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

	    public int SchoolId { get; private set; }

        public string SchoolName { get; private set; } = string.Empty;

        public string ApplicationReferenceNumber { get; private set; } = string.Empty;

        public short CompletedSections { get; private set; }

	    public short TotalNumberOfSections => 8;

        public List<ApplicationComponentViewModel> Components { get; set; } = new();

        public SchoolOverviewModel(ILogger<SchoolOverviewModel> logger, IConversionApplicationRetrievalService conversionApplicationRetrievalService) : base(conversionApplicationRetrievalService)
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

                // TODO MR:- get School by SchoolId OR URN ?
                //// var schoolCacheViewModel = ViewDataHelper.GetSerialisedValue<SchoolCacheValuesViewModel>(nameof(SchoolCacheValuesViewModel), ViewData) ?? new SchoolCacheValuesViewModel();
                var selectedSchool = await LoadAndSetSchoolDetails(99);

                // Grab other values from API
                if (selectedSchool != null)
                {
	                selectedSchool.SchoolApplicationComponents = await _conversionApplicationRetrievalService
		                .GetSchoolApplicationComponents(selectedSchool.SchoolId);

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
            var applicationCacheViewModel = ViewDataHelper.GetSerialisedValue<ApplicationCacheValuesViewModel>(nameof(ApplicationCacheValuesViewModel), ViewData) ?? new ApplicationCacheValuesViewModel();
            ApplicationReferenceNumber = applicationCacheViewModel.ApplicationReference;

            SchoolId = selectedSchool.SchoolId;
            SchoolName = selectedSchool.SchoolName;
            CompletedSections = 0; // TODO MR:- what logic drives this, component exists / hasData??

            // Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
            Components = selectedSchool.SchoolApplicationComponents.Select(c =>
	            new ApplicationComponentViewModel(name: c.Name, uri: SetSchoolApplicationComponentUriFromName(c.Name))
	            {
		            Status = c.Status
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
