using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
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
	    private readonly ILogger<AddAContributorModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    
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
		public bool HideRadios { get; set; }

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
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return Page();
			}

			if (ContributorRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
			{
				ModelState.AddModelError("OtherRoleNotEntered", "You must give your role at the school");
				PopulateValidationMessages();
				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return Page();
			}

			try
			{
				var contributor = new ConversionApplicationContributor("", Name, EmailAddress, ContributorRole, OtherRoleNotListed);

				await _academisationCreationService.AddContributorToApplication(contributor, ApplicationId);

				// update temp store for next step
				draftConversionApplication.Contributors.Add(contributor);
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				// MR:- need to stay on the page to show user a green confirmation banner that email has been sent / db updated
				ShowConfirmationBox = true;

				// MR:- need to call below otherwise will lose ExistingContributors()
				PopulateUiModel(draftConversionApplication);
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError("Application::AddAContributorModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}

		private void PopulateUiModel(ConversionApplication? application)
		{
			if (application != null)
			{
				// convert application?.Contributors -> list<ConversionApplicationContributorViewModel>
				if (application.Contributors.Any())
				{
					var contributors = application.Contributors
						.Select(e => new ConversionApplicationContributorViewModel(e.FullName, e.Role, e.OtherRoleName))
						.ToList();

					ExistingContributors = contributors;

					// MR:- setup roles collection i.e. if we already have a 'ChairOfGovernors' within the contributors collection
					// remove from UI so can't have 2 because that would break business rules
					var roles = Enum.GetValues(typeof(SchoolRoles)).OfType<SchoolRoles>();

					bool hasChair = application.Contributors.Any(x => x.Role == SchoolRoles.ChairOfGovernors);

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
				}
			}
			
			// this form won't be used as an update. Only add, so hence, so no VM property binding
		}
	}
}
