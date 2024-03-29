﻿using System.Security.Claims;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

public class WhatIsYourRoleModel : BasePageModel
{
	private readonly IConversionApplicationService _academisationCreationService;
	private readonly ILogger<WhatIsYourRoleModel> logger;
	private const string NextStepPage = "/ApplicationOverview";

	public WhatIsYourRoleModel(IConversionApplicationService academisationCreationService, ILogger<WhatIsYourRoleModel> logger)
	{
		_academisationCreationService = academisationCreationService;
		this.logger = logger;
	}

	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must give your role at the school")]
	public SchoolRoles SchoolRole { get; set; }
	
	[BindProperty]
	public ApplicationTypes ApplicationTypes { get; set; }
	
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

	public async Task OnGetAsync(int type)
	{
		ApplicationTypes = Enum.Parse<ApplicationTypes>(type.ToString());
		//// on load - grab draft application from temp
		var draftConversionApplication = TempDataHelper.GetSerialisedValueAndLog<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData, this.logger) ?? new ConversionApplication();

		//// MR:- Need to drop into THIS pages cache here ready for post / server callback !
		TempDataHelper.StoreSerialisedValueAndLog(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication, this.logger);

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

		//// grab draft application from temp= null
		var draftConversionApplication = TempDataHelper.GetSerialisedValueAndLog<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData, logger) ?? new ConversionApplication();

		this.logger.LogInformation($"application type of draft application: {draftConversionApplication.ApplicationType.ToString()}");

		var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
		var lastName = User.FindFirst(ClaimTypes.Surname)?.Value ?? "";
		var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
		var creationContributor = new ConversionApplicationContributor(firstName, lastName, email, SchoolRole, OtherRoleNotListed);
		draftConversionApplication.Contributors.Add(creationContributor);

		draftConversionApplication = await _academisationCreationService.CreateNewApplication(draftConversionApplication);

		// update temp store for next step - application overview i.e. ConversionApplication.ApplicationId
		TempDataHelper.StoreSerialisedValueAndLog(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication, logger);

		return RedirectToPage(NextStepPage, new { appId = draftConversionApplication.ApplicationId });
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
