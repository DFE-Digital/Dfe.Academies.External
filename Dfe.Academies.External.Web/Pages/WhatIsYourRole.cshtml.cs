using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

using System.Security.Claims;
public class WhatIsYourRoleModel : BasePageModel
{
    private readonly ILogger<WhatIsYourRoleModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;
    private const string NextStepPage = "/ApplicationOverview";

    public WhatIsYourRoleModel(ILogger<WhatIsYourRoleModel> logger,
                                IConversionApplicationCreationService academisationCreationService)
    {
        _logger = logger;
        _academisationCreationService = academisationCreationService;
    }

    [BindProperty]
    [RequiredEnum(ErrorMessage = "You must give your role at the school")]
    public SchoolRoles SchoolRole { get; set; }

    [BindProperty]
    public string? OtherRoleNotListed { get; set; }

    public bool OtherRoleError
    {
        get
        {
            if (!ModelState.IsValid && ModelState.Keys.Contains("OtherRoleNotEntered"))
            {
                return true;
            }

            return false;
        }
    }

    public async Task OnGetAsync()
    {
	    try
	    {
		    //// on load - grab draft application from temp
		    var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		    //// MR:- Need to drop into this pages cache here ready for post / server callback !
		    TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
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
	        PopulateValidationMessages();
            return Page();
        }

        if (SchoolRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
        {
            ModelState.AddModelError("OtherRoleNotEntered", "You must give your role at the school");
            PopulateValidationMessages();
            return Page();
        }

        try
        {
	        //// grab draft application from temp= null
	        var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

	        var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
	        var lastName = User.FindFirst(ClaimTypes.Surname)?.Value ?? "";
	        var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
	        var creationContributor = new ConversionApplicationContributor(firstName, lastName, email, SchoolRole, OtherRoleNotListed);
	        draftConversionApplication.Contributors.Add(creationContributor);

            await _academisationCreationService.UpdateDraftApplication(draftConversionApplication);

            // update temp store for next step - application overview
            TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

            return RedirectToPage(NextStepPage);
        }
        catch (Exception ex)
        {
            _logger.LogError("Application::WhatIsYourRoleModel::OnPostAsync::Exception - {Message}", ex.Message);
            return Page();
        }
    }

    public override void PopulateValidationMessages()
    {
        ViewData["Errors"] = ConvertModelStateToDictionary();

        // V2 figma has different error messages hence why code below exists !!
		if (!ModelState.IsValid)
        {
            if (ModelState.Keys.Contains("SchoolRole") && !this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey("SchoolRole"))
            {
                this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add("SchoolRole", new[] { "Select a role type" });
            }
            else if (ModelState.Keys.Contains("OtherRoleNotEntered") && !this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey("OtherRoleNotEntered"))
            {
                this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add("OtherRoleNotEntered", new[] { "You must give your role" });
            }
        }
    }
}
