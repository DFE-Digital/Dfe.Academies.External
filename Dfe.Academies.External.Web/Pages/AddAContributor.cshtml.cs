using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static GovUk.Frontend.AspNetCore.ComponentDefaults;

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
	    private readonly ILogger<AddAContributorModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    
	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

		//// TODO MR:- VM props to capture data -
		/// just 4 props
		/// firstName = non nullable string!
		/// lastName = non nullable string!
		/// email = non nullable string!
		/// SchoolRole = non nullable

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must choose a role")]
		public SchoolRoles ContributorRole { get; set; }

		[BindProperty]
		public string? OtherRoleNotListed { get; set; }

		[Required(ErrorMessage = "You must provide a name")]
		public string Name { get; set; }

		[EmailAddress]
		[Required(ErrorMessage = "You must provide an email address")]
		public string EmailAddress { get; set; }

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

		public List<ConversionApplicationContributorViewModel> ExistingContributors { get; private set; } = new();

		public AddAContributorModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			ILogger<AddAContributorModel> logger,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
		}

		/// <summary>
		/// contributors against application NOT school !!!
		/// </summary>
		/// <param name="appId"></param>
		/// <returns></returns>
		public async Task OnGetAsync(int appId)
		{
			try
			{
				//// on load - grab draft application from temp
				var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

				ApplicationId = appId;

				PopulateUiModel(draftConversionApplication);
			}
			catch (Exception ex)
			{
				_logger.LogError("Application::AddAContributorModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			if (ContributorRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
			{
				ModelState.AddModelError("OtherRoleNotEntered", "You must give your role at the school");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				//var dictionaryMapper = new Dictionary<string, dynamic>
				//{
				//	{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateSpecified), ContributorRole },
				//	{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDate), OtherRoleNotListed },
				//	{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateExplained), EmailAddress },
				//	{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateExplained), Name }
				//};

				// TODO MR:- sort out name
				var creationContributor = new ConversionApplicationContributor("", "", EmailAddress, ContributorRole, OtherRoleNotListed);
				draftConversionApplication.Contributors.Add(creationContributor);

				// TODO MR:- need an update application service func
				//await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, dictionaryMapper);

				// update temp store for next step
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("ApplicationOverview", new { appId = draftConversionApplication.ApplicationId });
			}
			catch (Exception ex)
			{
				_logger.LogError("Application::AddAContributorModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(ConversionApplication? application)
		{
			if (application != null)
			{
				// TODO MR:- setup roles collection i.e. if we already have a 'ChairOfGovernors' within the contributors collection
				// remove from UI
				var roles = Enum.GetValues(typeof(SchoolRoles)).OfType<SchoolRoles>();


				// convert application?.Contributors -> list<ConversionApplicationContributorViewModel>
				if (application.Contributors.Any())
				{
					var contributors = application.Contributors
						.Select(e => new ConversionApplicationContributorViewModel(e.FullName, e.Role, e.OtherRoleName))
						.ToList();

					ExistingContributors = contributors;
				}
			}
			
			// this form won't be used as an update. Only add, so hence, so no VM property binding
		}
	}
}
