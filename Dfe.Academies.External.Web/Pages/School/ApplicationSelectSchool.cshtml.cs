using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dfe.Academies.External.Web.Pages
{
	public class ApplicationSelectSchoolModel : BasePageModel
    {
	    private readonly ILogger<ApplicationSelectSchoolModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    private readonly IReferenceDataRetrievalService _referenceDataRetrievalService;
		private const string NextSchoolStepPage = "/ApplicationOverview";
		private const int SearchQueryMinLength = 3;

		public SchoolSelectorViewModel ViewModel { get; set; }

		public ApplicationSelectSchoolModel(ILogger<ApplicationSelectSchoolModel> logger,
		    IConversionApplicationCreationService academisationCreationService,
		    IReferenceDataRetrievalService referenceDataRetrievalService)
	    {
		    _logger = logger;
		    _academisationCreationService = academisationCreationService;
		    _referenceDataRetrievalService = referenceDataRetrievalService;
		}

		public async Task OnGetAsync()
		{
			try
			{
				//// on load - grab draft application from temp
				var conversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				//// MR:- Need to drop into this pages cache here ready for post / server callback !
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, conversionApplication);

				// TODO MR:- do we need to grab the SchoolApplyingToConvert here???
				PopulateUiModel(conversionApplication, null);
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationSelectSchoolModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}
		
		//public async Task<ActionResult> OnGetSchoolsSearchResult(string searchQuery)
		//{
		//	try
		//	{
		//		_logger.LogInformation("School::ApplicationSelectSchoolModel::OnGetSchoolsSearchResult");

		//		// Double check search query.
		//		if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < SearchQueryMinLength)
		//		{
		//			return new JsonResult(Array.Empty<SchoolSearchResultViewModel>());
		//		}

		//		var schoolSearch = new SchoolSearch(searchQuery, searchQuery);
		//		var schoolSearchResponse = await _referenceDataRetrievalService.SearchSchools(schoolSearch);

		//		return new JsonResult(schoolSearchResponse);
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError("School::ApplicationSelectSchoolModel::OnGetSchoolsSearchResult::Exception - {Message}", ex.Message);

		//		return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
		//	}
		//}

		// TODO MR:- do I need this????
		//public async Task<ActionResult> OnGetSelectedSchool(string schoolUkPrn)
		//{
		//	try
		//	{
		//		_logger.LogInformation("School::ApplicationSelectSchoolModel::OnGetSelectedSchool");

		//		// Double check selected trust.
		//		if (string.IsNullOrEmpty(schoolUkPrn) || schoolUkPrn.Contains("-") || schoolUkPrn.Length < SearchQueryMinLength)
		//			throw new Exception($"Trust::ApplicationSelectSchoolModel::OnGetSelectedSchool::Selected trust is incorrect - {schoolUkPrn}");

		//		// Store CaseState into cache.
		//		var userState = await _cachedService.GetData<UserState>(User.Identity.Name) ?? new UserState();
		//		userState.TrustUkPrn = schoolUkPrn;
		//		userState.CreateCaseModel = new CreateCaseModel();
		//		await _cachedService.StoreData(User.Identity.Name, userState);

		//		return new JsonResult(new { redirectUrl = Url.Page("Overview", new { id = schoolUkPrn }) });
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError("School::ApplicationSelectSchoolModel::OnGetSelectedSchool::Exception - {Message}", ex.Message);

		//		return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
		//	}
		//}

		public async Task<IActionResult> OnPostAsync()
	    {
		    if (!ModelState.IsValid)
		    {
			    // error messages component consumes ViewData["Errors"]
			    PopulateValidationMessages();
			    return Page();
		    }

		    try
		    {
			    //// grab draft application from temp
			    var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// TODO MR:-
				//   SchoolApplyingToConvert school = new();
				//   school.ApplicationId = _draftConversionApplication.Id;

				//await _academisationCreationService.AddSchoolToApplication(school);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage(NextSchoolStepPage);
		    }
		    catch (Exception ex)
		    {
			    _logger.LogError("Application::ApplicationSelectSchoolModel::OnPostAsync::Exception - {Message}", ex.Message);
			    return Page();
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

		private void PopulateUiModel(ConversionApplication? conversionApplication, SchoolApplyingToConvert? school)
		{
			ViewModel = new();

			if (conversionApplication != null)
			{
				ViewModel.ApplicationId = conversionApplication.Id;
				//ViewModel.SchoolName = ;
				ViewModel.CorrectSchoolConfirmation = false;
				ViewModel.SelectedSchool = new(string.Empty,string.Empty,string.Empty,string.Empty,string.Empty);
			}
		}
	}
}
