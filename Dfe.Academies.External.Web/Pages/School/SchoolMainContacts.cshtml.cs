using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class SchoolMainContactsModel : BasePageEditModel
	{
	    private readonly ILogger<SchoolMainContactsModel> _logger;

	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

		// TODO MR:- bind properties for UI - LOTS !!!
		[BindProperty]
		public ApplicationSchoolContactsViewModel ViewModel { get; set; }

		public SchoolMainContactsModel(ILogger<SchoolMainContactsModel> logger,
		    IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		    IReferenceDataRetrievalService referenceDataRetrievalService,
		    IConversionApplicationCreationService academisationCreationService)
		    : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
	    {
		    _logger = logger;
	    }

	    public async Task OnGetAsync(int urn, int appId)
	    {
		    try
		    {
			    LoadAndStoreCachedConversionApplication();

			    var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			    // Grab other values from API
			    if (selectedSchool != null)
			    {
				    // TODO MR:- grab data from API endpoint - applicationId && SchoolId combination !


				    PopulateUiModel(selectedSchool);
			    }
		    }
		    catch (Exception ex)
		    {
			    _logger.LogError("School::SchoolMainContactsModel::OnGetAsync::Exception - {Message}", ex.Message);
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

	    private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	    {
		    ApplicationId = selectedSchool.ApplicationId;
		    Urn = selectedSchool.URN;
		    SchoolName = selectedSchool.SchoolName;

		    ViewModel = new ApplicationSchoolContactsViewModel(selectedSchool.ApplicationId, selectedSchool.URN);
	    }
	}
}
