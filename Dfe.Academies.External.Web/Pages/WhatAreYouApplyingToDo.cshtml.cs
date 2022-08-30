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
    private const string NextStepPage = "/WhatIsYourRole";

    public WhatAreYouApplyingToDoModel(ILogger<WhatAreYouApplyingToDoModel> logger)
    {
        _logger = logger;
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
			// error messages component consumes ViewData["Errors"] so populate it
			PopulateValidationMessages();
			return Page();
        }

        try
        {
	        var applicationTypeSelected = ApplicationType;
            var draftConversionApplication = new ConversionApplication
            {
                ApplicationType = applicationTypeSelected
            };

			// MR:- plop draftApplication somewhere so WhatIsYourRole page can pick this up.
			// WhatIsYourRole page will carry on updating it and commit the API / DB !
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

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
	    PopulateViewDataErrorsWithModelStateErrors();
	}
}
