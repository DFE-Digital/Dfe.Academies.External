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
	    try
	    {
		    // like on load - if navigating backwards from NextStepPage - will need to set model value from somewhere!
		    //// on load - grab draft application from temp
		    var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		    //// MR:- Need to drop into this pages cache here ready for post / server callback !
		    TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
        }
	    catch (Exception ex)
	    {
		    _logger.LogError("Application::WhatAreYouApplyingToDoModel::OnGetAsync::Exception - {Message}", ex.Message);
	    }
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
	        var applicationTypeSelected = ApplicationType;
            var _draftConversionApplication = new ConversionApplication
            {
                ApplicationType = applicationTypeSelected,
                UserEmail = "Auth user"
            };

            _draftConversionApplication = await _academisationCreationService.CreateNewApplication(_draftConversionApplication);

            if (_draftConversionApplication != null)
            {
	            // MR:- plop newApplication.Id somewhere so NextStepPage can pick this up !
	            TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, _draftConversionApplication);
            }

            return RedirectToPage(NextStepPage);
        }
        catch (Exception ex)
        {
            _logger.LogError("Application::WhatAreYouApplyingToDoModel::OnPostAsync::Exception - {Message}", ex.Message);
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
}
