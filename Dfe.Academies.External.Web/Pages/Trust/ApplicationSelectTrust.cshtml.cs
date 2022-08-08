using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust
{
    public class ApplicationSelectTrustModel : BasePageModel
	{
	    private readonly ILogger<ApplicationSelectTrustModel> _logger;
	    private readonly IConversionApplicationCreationService _conversionApplicationCreationService;
	    private const string NextTrustStepPage = "/ApplicationOverview";

	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    [MinimumLength(ErrorMessage = "You must give the name of the trust")]
	    public string? SearchQuery { get; set; } = string.Empty;

	    [BindProperty]
	    [ConfirmTrue(ErrorMessage = "You must confirm that this is the correct trust")]
	    public bool CorrectTrustConfirmation { get; set; } = false;

	    public string SelectedTrustName
	    {
		    get
		    {
			    if (!string.IsNullOrWhiteSpace(SearchQuery))
			    {
				    var trustSplit = SearchQuery
					    .Trim()
					    .Split('(', StringSplitOptions.RemoveEmptyEntries);

				    return trustSplit[0].Trim();
			    }

			    return string.Empty;
		    }
	    }

	    public int SelectedUkPrn
	    {
		    get
		    {
			    if (!string.IsNullOrWhiteSpace(SearchQuery))
			    {
				    var trustSplit = SearchQuery
					    .Trim()
					    .Replace(")", string.Empty)
					    .Split('(', StringSplitOptions.RemoveEmptyEntries);

				    return Convert.ToInt32(trustSplit[^1]);
			    }

			    return 0;
		    }
	    }

	    public ApplicationSelectTrustModel(ILogger<ApplicationSelectTrustModel> logger, IConversionApplicationCreationService conversionApplicationCreationService)
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
			    _logger.LogError("Trust::ApplicationSelectTrustModel::OnGetAsync::Exception - {Message}", ex.Message);
		    }
	    }

	    [ValidateAntiForgeryToken]
	    public async Task<ActionResult> OnPostAddTrust()
	    {
		    if (!ModelState.IsValid)
		    {
			    // MR:- if you enter an incorrect name into the autocomplete, then the hidden input is blank (not populated in JS)
			    // so, currently get the 'You must give the trust of the school' validation warning
			    // rather than the "You must choose a trust from the list" (code below)

			    //// 2nd phase validation - check selected trust
			    if (string.IsNullOrWhiteSpace(SearchQuery))
			    {
				    ModelState.AddModelError("InvalidTrust", "You must give the name of the trust");
			    }

			    // error messages component consumes ViewData["Errors"]
			    PopulateValidationMessages();
			    return Page();
		    }

		    try
		    {
			    //// grab draft application from temp
			    var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			    await _conversionApplicationCreationService.AddTrustToApplication(draftConversionApplication.Id, SelectedUkPrn, SelectedTrustName);

			    // update temp store for next step - application overview
			    TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			    return RedirectToPage(NextTrustStepPage);
		    }
		    catch (Exception ex)
		    {
			    _logger.LogError("Trust::ApplicationSelectSchoolModel::OnPostAddTrust::Exception - {Message}", ex.Message);
			    return Page();
		    }
	    }

		public async Task<IActionResult> OnPostFind()
	    {
		    var query = SearchQuery;

		    return RedirectToPage("TrustSearchResults");
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
