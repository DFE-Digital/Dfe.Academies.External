using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationSelectSchoolModel : BasePageModel
    {
	    private readonly ILogger<ApplicationSelectSchoolModel> _logger;
	    private readonly IConversionApplicationCreationService _conversionApplicationCreationService;
		private const string NextSchoolStepPage = "/ApplicationOverview";

		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		[Required(AllowEmptyStrings = false, ErrorMessage = "You must give the name of the school")]
		public string SearchQuery { get; set; } = string.Empty;

		[BindProperty]
		[Range(typeof(bool), "true", "true", ErrorMessage = "You must confirm that is the correct school")]
		public bool CorrectSchoolConfirmation { get; set; } = false;

		public string SelectedSchoolName
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(SearchQuery))
				{
					var schoolSplit = SearchQuery
						.Trim()
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return schoolSplit[0].Trim();
				}

				return string.Empty;
			}
		}

		public int SelectedUrn {
			get
			{
				if (!string.IsNullOrWhiteSpace(SearchQuery))
				{
					var schoolSplit = SearchQuery
						.Trim()
						.Replace(")", string.Empty)
						.Split('(', StringSplitOptions.RemoveEmptyEntries);

					return Convert.ToInt32(schoolSplit[^1]);
				}

				return 0;
			}
		} 

		public ApplicationSelectSchoolModel(ILogger<ApplicationSelectSchoolModel> logger, IConversionApplicationCreationService conversionApplicationCreationService)
	    {
		    _logger = logger;
		    _conversionApplicationCreationService = conversionApplicationCreationService;
		}

		public async Task OnGetAsync()
		{
			try
			{
				//// on load - grab draft application from temp
				var conversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				//// MR:- Need to drop into this pages cache here ready for post / server callback !
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, conversionApplication);

				PopulateUiModel(conversionApplication);
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationSelectSchoolModel::OnGetAsync::Exception - {Message}", ex.Message);
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

			// TODO MR:- add secondary validation
			// 2nd phase validation - is SelectedUrn >0
			if (SelectedUrn == 0)
			{
				ModelState.AddModelError("InvalidSchool", "You must choose a school from the list");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
			    //// grab draft application from temp
			    var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			    await _conversionApplicationCreationService.AddSchoolToApplication(draftConversionApplication.Id, SelectedUrn);

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

		private void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				ApplicationId = conversionApplication.Id;
				// other view model properties initialized within properties
			}
		}
	}
}
