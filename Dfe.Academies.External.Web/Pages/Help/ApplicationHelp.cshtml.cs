using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models.Notifications;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using static GovUk.Frontend.AspNetCore.ComponentDefaults;

namespace Dfe.Academies.External.Web.Pages.Help
{
	public class ApplicationHelpModel : BasePageModel
	{
		private readonly IEmailNotificationService emailNotificationService;
		private readonly string templateId;

		[BindProperty]
		[Required(ErrorMessage = "You must provide details of the help your require")]
		public string HelpSummary { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide an email address")]
		public string EmailAddress { get; set; } = string.Empty;

		public ApplicationHelpModel(IEmailNotificationService emailNotificationService, IOptions<NotifyTemplateSettings> notifyTemplateSettings)
		{
			this.emailNotificationService = emailNotificationService;
			this.templateId = notifyTemplateSettings.Value.HelpWithAnApplicationTemplateId;
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

			// send email
			var personalization = new Dictionary<string, object>();

			personalization.Add("what_do_you_need_help_with", HelpSummary);
			personalization.Add("help_email_address", EmailAddress);

			var message = new MessageDto("", this.templateId)
			{
				Personalisation = personalization
			};
			await this.emailNotificationService.SendAsync(message);

			return RedirectToPage("ThankYou", new { page = nameof(ApplicationHelpModel) });
		}

		public override void PopulateValidationMessages()
		{
			throw new NotImplementedException();
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
	}
}
