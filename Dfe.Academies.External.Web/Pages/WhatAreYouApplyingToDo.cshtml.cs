using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

public class WhatAreYouApplyingToDoModel : BasePageModel
{
	private readonly IConfiguration _configuration;
	private const string NextStepPage = "/WhatIsYourRole";

	[BindProperty]
	[RequiredEnum(ErrorMessage = "Select an application type")]
	public ApplicationTypes ApplicationType { get; set; }

	public bool FamApplicationsEnabled
	{
		get
		{
			bool famApplicationsEnabled = _configuration.GetValue<bool>("features:fam_applications");

			return famApplicationsEnabled;
		}
	}

	public WhatAreYouApplyingToDoModel(IConfiguration configuration)
	{
		_configuration = configuration;
	}
	public async Task OnGetAsync()
	{
		// like on load - if navigating backwards from NextStepPage - will need to set model value from somewhere!
		//// on load - grab draft application from temp
		var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		//// MR:- Need to drop into this pages cache here ready for post / server callback !
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			// error messages component consumes ViewData["Errors"] so populate it
			PopulateValidationMessages();
			return Page();
		}

		var applicationTypeSelected = ApplicationType;
		var draftConversionApplication = new ConversionApplication
		{
			ApplicationType = applicationTypeSelected
		};

		// MR:- plop draftApplication somewhere so WhatIsYourRole page can pick this up.
		// WhatIsYourRole page will carry on updating it and commit the API / DB !
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

		return RedirectToPage(NextStepPage, new {Type = (int)ApplicationType});
	}

	public override void PopulateValidationMessages()
	{
		PopulateViewDataErrorsWithModelStateErrors();
	}
}
