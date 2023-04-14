using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models.Notifications;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Dfe.Academies.External.Web.Pages.Help
{
	public class ApplicationHelpModel : BasePageModel
	{
		private readonly IEmailNotificationService emailNotificationService;
		private readonly IConfiguration configuration;
		private readonly string templateId;

		[BindProperty]
		[Required(ErrorMessage = "You must give details")]
		public string HelpSummary { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must give an email address")]
		public string EmailAddress { get; set; } = string.Empty;

		public ApplicationHelpModel(IEmailNotificationService emailNotificationService, IOptions<NotifyTemplateSettings> notifyTemplateSettings, IConfiguration configuration)
		{
			this.emailNotificationService = emailNotificationService;
			this.configuration = configuration;
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

			var message = new MessageDto(this.configuration["emailnotifications:supportemail"], this.templateId)
			{
				Personalisation = personalization,
			};
			await this.emailNotificationService.SendAsync(message);

			return RedirectToPage("ThankYou", new { helpTypeId = HelpTypes.ApplicationHelp });
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
