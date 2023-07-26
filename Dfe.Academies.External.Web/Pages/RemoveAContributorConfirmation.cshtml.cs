using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class RemoveAContributorConfirmationModel : BasePageEditModel
	{
		private readonly IConversionApplicationService _academisationCreationService;
		
		[BindProperty]
		public int ContributorId { get; set; }

		[BindProperty]
		public int ApplicationId { get; set; }

		public string ContributorName { get; private set; } = string.Empty;

		private string NextStepPage { get; set; } = "/AddAContributor";

		public RemoveAContributorConfirmationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
	        _academisationCreationService = academisationCreationService;
        }

		public async Task<ActionResult> OnGetAsync(int appId, int contributorId)
        {
	        //// on load - grab draft application from temp
	        var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

	        // check user access
	        var checkStatus = await CheckApplicationPermission(appId);

	        if (checkStatus is ForbidResult)
	        {
		        return RedirectToPage("ApplicationAccessException");
	        }

	        ApplicationId = appId;
	        ContributorId = contributorId;

			PopulateUiModel(draftConversionApplication);

			return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			//// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey,
					TempData) ?? new ConversionApplication();

			if (!RunUiValidation())
			{
				return Page();
			}

			await _academisationCreationService.RemoveContributorFromApplication(ContributorId, ApplicationId);

			// update temp store for next step
			var contributor = draftConversionApplication.Contributors.FirstOrDefault(c => c.ContributorId == this.ContributorId);
			draftConversionApplication.Contributors.Remove(contributor);
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, removedContributorName = contributor.FullName,  });
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			throw new NotImplementedException();
		}

		public void PopulateUiModel(ConversionApplication? conversionApplication)
        {
	        if (conversionApplication != null)
	        {
		        var contributor = conversionApplication.Contributors.FirstOrDefault(c => c.ContributorId == this.ContributorId);

		        if (contributor != null)
		        {
			        ContributorName = contributor.FullName;
		        }
	        }
		}
	}
}
