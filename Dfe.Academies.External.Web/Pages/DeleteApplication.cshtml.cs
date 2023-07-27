using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Helpers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Dfe.Academies.External.Web.Models.Notifications;

namespace Dfe.Academies.External.Web.Pages
{
    public class DeleteApplicationModel : BasePageEditModel
    {

        private readonly IEmailNotificationService _emailNotificationService;
		
		private readonly IConversionApplicationService _academisationService;
		
        [BindProperty]
		public int ApplicationId { get; set; }

		public string ApplicationReferenceNumber { get; private set; } = string.Empty;

		private readonly string templateId;

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
			IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationService academisationService,
			IOptions<NotifyTemplateSettings> notifyTemplateSettings,IEmailNotificationService emailNotificationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_academisationService = academisationService;
			_emailNotificationService = emailNotificationService;
			this.templateId = notifyTemplateSettings.Value.ApplicationDeletedId;
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

		    var contibutors  = draftConversionApplication.Contributors;
			string leadContributor = contibutors[0].FirstName + " " + contibutors[0].LastName;
			string applicationType = draftConversionApplication.ApplicationType.GetDescription();

			foreach (Dtos.ConversionApplicationContributor i in contibutors)
			{
				var personalization = new Dictionary<string, object>();

				personalization.Add("app_ref", draftConversionApplication.ApplicationReference);
				personalization.Add("application_canceller", leadContributor);
				personalization.Add("app_type", applicationType);

				var message = new MessageDto(i.EmailAddress, this.templateId)
				{
					Personalisation = personalization,
				};

				await _emailNotificationService.SendAsync(message);
			}

			return RedirectToPage("YourApplications", new
			{
				deletedApplicationReferenceNumber = draftConversionApplication.ApplicationReference,
				deletedApplicationType = applicationType
			});
		}
    }
}
