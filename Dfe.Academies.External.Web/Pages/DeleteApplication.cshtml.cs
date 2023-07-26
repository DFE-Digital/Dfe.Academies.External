using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Dfe.Academies.External.Web.Extensions;


namespace Dfe.Academies.External.Web.Pages
{
    public class DeleteApplicationModel : BasePageEditModel
    {

		private readonly IConversionApplicationService _academisationService;
		private readonly IConfiguration configuration;

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
			IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationService academisationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_academisationService = academisationService;			
		}
        
        public async Task<ActionResult> OnGetAsync(int appId)
        {
            var draftConversionApplication = await LoadAndSetApplicationDetails(appId);
		
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			if (draftConversionApplication?.ApplicationStatus == Enums.ApplicationStatus.Submitted || draftConversionApplication?.DeletedAt != null)
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

		public async Task<IActionResult> OnPostAsync(int appId)
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			var checkStatus = await CheckApplicationPermission(appId);

			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			if (draftConversionApplication.ApplicationStatus == Enums.ApplicationStatus.Submitted)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			await _academisationService.CancelApplication(appId);	

			return RedirectToPage("YourApplications", new 
				{ deletedApplicationReferenceNumber = draftConversionApplication.ApplicationReference,
			 	deletedApplicationType = draftConversionApplication.ApplicationType.GetDescription()
				});
		}
    }
}
