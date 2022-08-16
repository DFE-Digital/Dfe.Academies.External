using Dfe.Academies.External.Web.Enums;
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

		public string SigninApproverQuestionText { get; private set; } = string.Empty;

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

					var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData);

                    PopulateUiModel(selectedSchool, draftConversionApplication.ApplicationType);
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

	    private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ApplicationTypes applicationType)
	    {
		    ApplicationId = selectedSchool.ApplicationId;
		    Urn = selectedSchool.URN;
		    SchoolName = selectedSchool.SchoolName;

		    ViewModel = new ApplicationSchoolContactsViewModel(selectedSchool.ApplicationId, selectedSchool.URN);

			SigninApproverQuestionText = applicationType == ApplicationTypes.FormNewSingleAcademyTrust
						? "When your schools converts, we need to create a new DfE sign-in account for the academy. Please supply the most appropriate contact to be set up as the DfE Sign-in approver to manage the new academies account."
						: "When your schools converts, we need to create a new DfE sign-in account for the academy. Please provide the most suitable contact to manage the new academies account.";
		}
	}
}
