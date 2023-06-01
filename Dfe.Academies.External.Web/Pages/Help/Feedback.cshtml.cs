using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models.Notifications;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web.Pages.Help
{
	public class FeedbackModel : BasePageModel
	{
		private readonly IEmailNotificationService emailNotificationService;
		private readonly IConfiguration configuration;
		private readonly string templateId;

        [BindProperty]
        [RequiredEnum(ErrorMessage = "You must choose an option")]
        public Feedback Feedback {get;set;}

		[BindProperty]
		[Required(ErrorMessage = "You must give details")]
		public string FeedbackSummary { get; set; } = string.Empty;


		public FeedbackModel(IEmailNotificationService emailNotificationService, IOptions<NotifyTemplateSettings> notifyTemplateSettings, IConfiguration configuration)
		{
			this.emailNotificationService = emailNotificationService;
			this.configuration = configuration;
			this.templateId = notifyTemplateSettings.Value.FeedbackTemplateId;
		}

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			//send email
			var personalization = new Dictionary<string, object>();

            personalization.Add("How_do_you_feel", Feedback.GetDescription());
			personalization.Add("what_improvements", FeedbackSummary);
			
			var message = new MessageDto(this.configuration["emailnotifications:supportemail"], this.templateId)
			{
				Personalisation = personalization,
			};
			await this.emailNotificationService.SendAsync(message);

			return RedirectToPage("ThankYou", new { page = nameof(FeedbackModel) });
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			return true;
		}
		public bool IsPropertyInvalid(string propertyKey)
		{
			return ModelState.GetFieldValidationState(propertyKey) == ModelValidationState.Invalid;
		}

	}
}
