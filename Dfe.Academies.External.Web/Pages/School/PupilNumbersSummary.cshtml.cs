using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class PupilNumbersSummaryModel : BasePageEditModel
	{
	    private readonly ILogger<PupilNumbersSummaryModel> _logger;

	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; private set; }

	    public string SchoolName { get; private set; } = string.Empty;

	    //// MR:- VM props to show school conversion data
	    public List<SchoolConversionComponentHeadingViewModel> ViewModel { get; set; } = new();

	    public PupilNumbersSummaryModel(ILogger<PupilNumbersSummaryModel> logger,
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
			    _logger.LogError("School::SchoolConversionKeyDetailsModel::OnGetAsync::Exception - {Message}", ex.Message);
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
			// TODO MR:- sort out sections - setup VM from what we get back from API

			SchoolConversionComponentHeadingViewModel heading1 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchool,
																		"/school/ApplicationSelectSchool");

			// TODO MR:- fo answer, consume SchoolConversionComponentSectionViewModel.NoInfoAnswer if string.isnullorempty()
			heading1.Sections.Add(new(SchoolConversionComponentSectionViewModel.NameOfSchoolSectionName, "TBC"));
			
			var vm = new List<SchoolConversionComponentHeadingViewModel> { heading1  };

			ViewModel = vm;
		}
	}
}
