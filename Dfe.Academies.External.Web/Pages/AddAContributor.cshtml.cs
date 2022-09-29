using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
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
		[RequiredEnum(ErrorMessage = "You must give your role at the school")]
		public SchoolRoles ContributorRole { get; set; }

		[BindProperty]
		public string? OtherRoleNotListed { get; set; }

		[Required(ErrorMessage = "You must provide details")]
		public string Name { get; set; }

		[EmailAddress]
		[Required(ErrorMessage = "You must provide details")]
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

		public List<ConversionApplicationContributor>? ExistingContributors { get; set; }

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
				_logger.LogError("School::AddAContributorModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(ConversionApplication? application)
		{
			// TODO MR:- setup roles collection i.e. if we already have a 'ChairOfGovernors' within the contributors collection
			// remove from UI

			ExistingContributors = application?.Contributors;

			// this form won't be used as an update. Only add, so hence, so no VM property binding
		}
	}
}
