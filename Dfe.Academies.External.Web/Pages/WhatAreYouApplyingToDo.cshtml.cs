using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

public class WhatAreYouApplyingToDoModel : BasePageModel
{
    private readonly ILogger<WhatAreYouApplyingToDoModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;
    private const string NextStepPage = "/WhatIsYourRole";
    private ConversionApplication _draftConversionApplication;

    public WhatAreYouApplyingToDoModel(ILogger<WhatAreYouApplyingToDoModel> logger, 
                                        IConversionApplicationCreationService academisationCreationService)
    {
        _logger = logger;
        _academisationCreationService = academisationCreationService;
    }

    [BindProperty]
    [RequiredEnum(ErrorMessage = "Select an application type")]
    public ApplicationTypes ApplicationType { get; set; }

    public async Task OnGetAsync()
    {
        // like on load - if navigating backwards from NextStepPage - will need to set model value from somewhere!
        _draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
        ApplicationType = _draftConversionApplication.ApplicationType;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // error messages component consumes ViewData["Errors"]
            ViewData["Errors"] = ConvertModelStateToDictionary();
            return Page();
        }

        try
        {
            _draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
            _draftConversionApplication.ApplicationType = ApplicationType;
            _draftConversionApplication.UserEmail = "Auth user";

            _draftConversionApplication = await _academisationCreationService.CreateNewApplication(_draftConversionApplication);

            if (_draftConversionApplication != null)
            {
                // MR:- plop newApplication.Id somewhere so NextStepPage can pick this up !
                TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, _draftConversionApplication);
                return RedirectToPage(NextStepPage);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Application::WhatAreYouApplyingToDoModel::OnPostAsync::Exception - {Message}", ex.Message);
            return Page();
        }
        
        return Page();
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
}
