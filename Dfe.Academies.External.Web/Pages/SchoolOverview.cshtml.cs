using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages
{
    public class SchoolOverviewModel : BasePageEditModel
    {
	    private readonly ILogger<SchoolOverviewModel> _logger;
	    private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;
	    
		public SchoolApplyingToConvert SelectedSchool { get; private set; } = new(string.Empty);

        public string ApplicationReferenceNumber { get; private set; } = string.Empty;

        public short CompletedSections { get; private set; }

	    public short TotalNumberOfSections => 8;

        public List<ViewModels.ApplicationComponentViewModel> Components { get; set; } = new();

        public SchoolOverviewModel(ILogger<SchoolOverviewModel> logger, IConversionApplicationRetrievalService conversionApplicationRetrievalService) : base(conversionApplicationRetrievalService)
        {
	        _logger = logger;
	        _conversionApplicationRetrievalService = conversionApplicationRetrievalService;
        }

        public async Task OnGetAsync()
        {
	        try
	        {
		        // SelectedSchool = ;// TODO MR:- going to have to wham into session !
                SelectedSchool = TempDataHelper.GetSerialisedValue<SchoolApplyingToConvert>(TempDataHelper.SelectedSchoolKey, TempData) ?? new SchoolApplyingToConvert(string.Empty);

                // Grab other values from API
                SelectedSchool.SchoolApplicationComponents = await _conversionApplicationRetrievalService
			        .GetSchoolApplicationComponents(SelectedSchool.Id);

                PopulateUiModel(SelectedSchool);
            }
	        catch (Exception ex)
	        {
		        _logger.LogError("Application::SchoolOverviewModel::OnGetAsync::Exception - {Message}", ex.Message);
            }
        }

        private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
        {
            CompletedSections = 0; // TODO MR:- what logic drives this, component exists / hasData??

            // Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
            Components = selectedSchool.SchoolApplicationComponents.Select(c =>
	            new ViewModels.ApplicationComponentViewModel(name: c.Name, uri: SetSchoolApplicationComponentUriFromName(c.Name))
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

        private string SetSchoolApplicationComponentUriFromName(string componentName)
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
