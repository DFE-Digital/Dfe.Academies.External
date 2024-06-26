﻿using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Security.Claims;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
	/// <summary>
	/// Purpose of this is to give other staff members within a school access to the application
	/// contributors collection already exists against the application
	/// So this is just adding a new one
	/// ONLY business logic is:- if we already have a 'ChairOfGovernors' within the contributors collection
	/// do NOT give user ability to add another one !!!!!
	/// </summary>
	public class AddAContributorModel : BasePageEditModel
	{
	    private readonly IConversionApplicationService _academisationCreationService;
	    private readonly IContributorEmailSenderService _contributorEmailSenderService;

		//// MR:- selected school props for UI rendering
		[BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
		public IEnumerable<SchoolRoles> RolesToDisplay { get; set; }

		//// MR:- VM props to capture data -
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must choose a role")]
		public SchoolRoles ContributorRole { get; set; }

		[BindProperty]
		public string? OtherRoleNotListed { get; set; }

		[BindProperty]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		[Required(ErrorMessage = "You must provide an email address")]
		public string? EmailAddress { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide a name")]
		public string? Name { get; set; } = string.Empty;

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

		[BindProperty]
		public bool ShowConfirmationBox { get; set; }

		[BindProperty]
		public bool ShowContributorRemovedConfirmationBox { get { return !string.IsNullOrEmpty(RemovedContributorName);  } }

		[BindProperty]
		public string? RemovedContributorName { get; set; }

		[BindProperty]
		public bool HideRadios { get; set; }

		public List<ConversionApplicationContributorViewModel> ExistingContributors { get; private set; }

		public AddAContributorModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService,
			IContributorEmailSenderService contributorEmailSenderService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_academisationCreationService = academisationCreationService;
			_contributorEmailSenderService = contributorEmailSenderService;
		}

		/// <summary>
		/// contributors against application NOT school !!!
		/// </summary>
		/// <param name="appId"></param>
		/// <returns></returns>
		public async Task<ActionResult> OnGetAsync(int appId, string? removedContributorName)
		{
			//// on load - grab draft application from temp
			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);
			RemovedContributorName = removedContributorName;

			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			ApplicationId = appId;

			PopulateUiModel(draftConversionApplication);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
			RemovedContributorName = null;

			if (!RunUiValidation())
			{
				return Page();
			}

			var contributor = new ConversionApplicationContributor("", Name!, EmailAddress!, ContributorRole, OtherRoleNotListed);

			await _academisationCreationService.AddContributorToApplication(contributor, ApplicationId);

			string firstName = User.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
			string lastName = User.FindFirst(ClaimTypes.Surname)?.Value ?? "";
			string invitingUserName = $"{firstName} {lastName}";
			var schoolName = ApplicationSchoolName(draftConversionApplication);
			var emailVariables = new EmailVariablesDto
			{
				ContributorName = Name!, InvitingUsername = invitingUserName, SchoolName = schoolName
			};
			
			await _contributorEmailSenderService.SendInvitationToContributor(draftConversionApplication.ApplicationType, ContributorRole,
				EmailAddress!, emailVariables);

			// update temp store for next step
			draftConversionApplication = await LoadAndSetApplicationDetails(ApplicationId);
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			// MR:- need to stay on the page to show user a green confirmation banner that email has been sent / db updated
			ShowConfirmationBox = true;

			// MR:- need to call below otherwise will lose ExistingContributors()
			PopulateUiModel(draftConversionApplication);
			Name = Name;
			return Page();
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return false;
			}

			if (ContributorRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
			{
				ModelState.AddModelError("OtherRoleNotEntered", "You must give your role at the school");
				PopulateValidationMessages();
				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return false;
			}

			// check not stupidly inviting yourself!
			string currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
			if (EmailAddress?.Trim().ToLower() == currentUserEmail.Trim().ToLower())
			{
				ModelState.AddModelError("InvitedYourself", "You cannot invite yourself as a contributor");
				PopulateValidationMessages();
				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return false;
			}

			//check email address not already in use
			if (draftConversionApplication.Contributors.Any(x => x.EmailAddress.ToLower().Equals(EmailAddress?.Trim().ToLower())))
			{
				ModelState.AddModelError("EmailAddressInUse", "You must use a unique email address, the email address is already in use");
				PopulateValidationMessages();
				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}
		
		private void PopulateUiModel(ConversionApplication? application)
		{
			if (application == null)
				return;
			
			if (!application.Contributors.Any())
				return;
			

			var contributors = application.Contributors
				.Select(e => new ConversionApplicationContributorViewModel(e.ContributorId,
					application.ApplicationId,
					e.FullName, 
					e.Role, 
					e.OtherRoleName,
					e.EmailAddress))
				.ToList();

			ExistingContributors = contributors;

			// MR:- setup roles collection i.e. if we already have a 'ChairOfGovernors' within the contributors collection
			// remove from UI so can't have 2 because that would break business rules
			var roles = Enum.GetValues(typeof(SchoolRoles)).OfType<SchoolRoles>();

			bool hasChair = application.Contributors.Any(x => x.Role == SchoolRoles.ChairOfGovernors && application.ApplicationType != ApplicationTypes.FormAMat);

			if (hasChair)
			{
				RolesToDisplay = roles.Where(x=> x != SchoolRoles.ChairOfGovernors);
				ContributorRole = SchoolRoles.Other;
				HideRadios = true;
			}
			else
			{
				RolesToDisplay = roles;
				HideRadios = false;
			}

			// this form won't be used as an update. Only add, so hence, so no VM property binding
		}

		private string ApplicationSchoolName(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				if (conversionApplication.ApplicationType == ApplicationTypes.JoinAMat)
				{
					var school = conversionApplication.Schools.FirstOrDefault();

					if (school != null)
					{
						return school.SchoolName;
					}
				}
				else // FAM
				{
					// TODO: user will have to select a school !!!!
					return string.Empty;
				}
			}

			return string.Empty;
		}
	}
}
