using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

public class WhatIsYourRoleModel : BasePageModel
{
    private readonly ILogger<WhatIsYourRoleModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;
    private ConversionApplication _draftConversionApplication;
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
        //// on load - grab draft application from temp
        _draftConversionApplication = TempDataHelperService.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // error messages component consumes ViewData["Errors"]
            PopulateValidationMessages();
            return Page();
        }

        if (SchoolRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
        {
            ModelState.AddModelError("OtherRoleNotEntered", "You must give your role at the school");
            PopulateValidationMessages();
            return Page();
        }

        //// grab draft application from temp
        _draftConversionApplication = TempDataHelperService.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();

        try
        {
            _draftConversionApplication.SchoolRole = SchoolRole;
            _draftConversionApplication.OtherRoleNotListed = OtherRoleNotListed;

            await _academisationCreationService.UpdateDraftApplication(_draftConversionApplication);

            // update temp store for next step
            TempDataHelperService.StoreSerialisedValue("draftConversionApplication", TempData, _draftConversionApplication);

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