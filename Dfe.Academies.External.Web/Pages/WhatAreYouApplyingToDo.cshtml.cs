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

    public WhatAreYouApplyingToDoModel(ILogger<WhatAreYouApplyingToDoModel> logger, IConversionApplicationCreationService academisationCreationService)
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
        // ApplicationType = TempData["applicationTypeSelected"];
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // error messages component consumes ViewData["Errors"]
            ViewData["Errors"] = ConvertModelStateToDictionary();
            return Page();
        }

        var applicationTypeSelected = ApplicationType;
        try
        {
            var newApplication = new ConversionApplication
            {
                ApplicationType = applicationTypeSelected,
                UserEmail = "Auth user"
            };

            newApplication = await _academisationCreationService.CreateNewApplication(newApplication);

            if(newApplication!=null)
                // MR:- plop newApplication.Id somewhere so NextStepPage can pick this up !
                TempData["newApplicationId"] = newApplication.Id.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError("Application::WhatAreYouApplyingToDoModel::OnPostAsync::Exception - {Message}", ex.Message);
            return Page();
        }
        
        switch (applicationTypeSelected)
        {
            case ApplicationTypes.JoinMat:
                TempData["applicationTypeSelected"] = applicationTypeSelected;
                return RedirectToPage(NextStepPage);
            case ApplicationTypes.FormNewMat:
                TempData["applicationTypeSelected"] = applicationTypeSelected;
                return RedirectToPage(NextStepPage);
            case ApplicationTypes.FormNewSingleAcademyTrust:
                TempData["applicationTypeSelected"] = applicationTypeSelected;
                return RedirectToPage(NextStepPage);
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
