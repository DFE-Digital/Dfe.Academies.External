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
			
			PopulateUiModel(draftConversionApplication);

			return Page();
        }

		public override void PopulateValidationMessages()
        {
	        throw new NotImplementedException();
        }

        public override bool RunUiValidation()
        {
			// TODO:- not sure this applies
	        throw new NotImplementedException();
        }

        public override Dictionary<string, dynamic> PopulateUpdateDictionary()
        {
	        throw new NotImplementedException();
        }

        private void PopulateUiModel(ConversionApplication? application)
        {
	        if (application != null)
	        {
		        // TODO:-  need to grab contrbutor name to show on UI
			}
		}
	}
}
