using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationSubmittedModel : BasePageEditModel
	{
		//// Below are props for UI display
		public string ApplicationReferenceNumber { get; private set; } = string.Empty;

		public ApplicationMilestonesViewModel Milestones { get; private set; }

		public ApplicationSubmittedModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			Milestones = new();
		}

		public async Task<ActionResult> OnGetAsync(int appId)
		{
			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

			if (draftConversionApplication == null)
			{
				return Page();
			}

			PopulateUiModel(draftConversionApplication);

			return Page();
		}

		private void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				//ApplicationId = conversionApplication.ApplicationId;
				//ApplicationType = conversionApplication.ApplicationType;
				ApplicationReferenceNumber = conversionApplication.ApplicationReference;
				//NameOfTrustToJoin = conversionApplication.TrustName;

				// initialise - populate however @ViewData["MilestonesConfig"] is done in as-is !
				// ViewData["Milestones"] = SessionHelper.GetSectionsFromSession("Milestones"); gets data from json in constants !
				Milestones = new ApplicationMilestonesViewModel();
			}
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
			return true;
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
	}
}
