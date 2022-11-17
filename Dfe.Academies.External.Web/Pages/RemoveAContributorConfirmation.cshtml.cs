using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class RemoveAContributorConfirmationModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		//// TODO MR:- VM props to capture data -
		public string ContributorName { get; private set; } = string.Empty;

		public RemoveAContributorConfirmationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
													IReferenceDataRetrievalService referenceDataRetrievalService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
        }

        // TODO:- might need to override - passing in app ID via query string? passing in contributor id?
		public async Task<ActionResult> OnGetAsync(int appId)
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
	        int contributorId = 0; // TODO:- fix!
			
			PopulateUiModel(draftConversionApplication, contributorId);

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
	        // does not apply on this page
	        return true;
		}

        ///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
        {
	        throw new NotImplementedException();
        }

        private void PopulateUiModel(ConversionApplication? application, int contributorId)
        {
	        if (application != null)
	        {
				// TODO:-  need to grab contributor show name on UI
				var contributor = application.Contributors.FirstOrDefault( c=> c.ContributorId == contributorId);

				if (contributor != null)
				{
					ContributorName = contributor.FullName;
				}
	        }
		}
	}
}
