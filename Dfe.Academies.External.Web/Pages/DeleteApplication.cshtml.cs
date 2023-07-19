using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class DeleteApplicationModel : BasePageEditModel
    {


        [BindProperty]
		public int ApplicationId { get; set; }

		public string ApplicationReferenceNumber { get; private set; } = string.Empty;


		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}

		public override void PopulateValidationMessages()
		{
			// does not apply on this page
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public override bool RunUiValidation()
		{
			// does not apply on this page
			return true;
		}

       public DeleteApplicationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			
			
		}
        
        public async Task<ActionResult> OnGetAsync(int appId)
        {
            var draftConversionApplication = await LoadAndSetApplicationDetails(appId);
		
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			ApplicationId = appId;

			ApplicationReferenceNumber = draftConversionApplication.ApplicationReference;
			
			return Page();    
        }

		public void OnGetBackClick()
        {
           	RedirectToPage("ApplicationOverview", new { appId = ApplicationId });
        }
    }
}
