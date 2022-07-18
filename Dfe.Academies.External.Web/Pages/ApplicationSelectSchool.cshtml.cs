using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationSelectSchoolModel : BasePageModel
    {
	    private readonly ILogger<ApplicationSelectSchoolModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
		private const string NextSchoolStepPage = "/ApplicationOverview";
		
		public SchoolSelectorViewModel ViewModel { get; set; }

		public ApplicationSelectSchoolModel(ILogger<ApplicationSelectSchoolModel> logger,
		    IConversionApplicationCreationService academisationCreationService)
	    {
		    _logger = logger;
		    _academisationCreationService = academisationCreationService;
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
				_logger.LogError("Application::WhatIsYourRoleModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

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
				ViewModel.SelectedSchool = new();
			}
		}
	}
}
