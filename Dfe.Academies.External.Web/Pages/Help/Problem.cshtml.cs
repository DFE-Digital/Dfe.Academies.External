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
	public class ProblemModel : BasePageModel
	{
		private readonly IEmailNotificationService emailNotificationService;
		private readonly IConfiguration configuration;
		private readonly NotifyTemplateSettings notifyTemplateSettings;
		private string templateId;

		[BindProperty]
		[Required(ErrorMessage = "You must give details")]
		public string ProblemSummary { get; set; } = string.Empty;

		[BindProperty]
		public string? EmailAddress { get; set; } = null;

		[BindProperty]
		[Required(ErrorMessage = "You must choose an option")]
		public SelectOption? DoYouWantToBeContacted { get; set; } = null;

		public bool ContactdByEmailError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("ContactdByEmailError");
			}
		}

		public ProblemModel(IEmailNotificationService emailNotificationService, IOptions<NotifyTemplateSettings> notifyTemplateSettings, IConfiguration configuration)
		{
			this.emailNotificationService = emailNotificationService;
			this.configuration = configuration;
			this.notifyTemplateSettings = notifyTemplateSettings.Value;
		}

		public void OnGet()
		{
		}
		public bool HasError
		{
			get
			{
				var bools = new[] { ContactdByEmailError };

				return bools.Any(b => b);

			}
		}


		public async Task<IActionResult> OnPostAsync()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			// send email
			var personalization = new Dictionary<string, object>();
			if (DoYouWantToBeContacted == SelectOption.Yes)
			{
				personalization.Add("what_problem_did_you_notice_response", ProblemSummary);
				personalization.Add("problem_email_address", EmailAddress);
				this.templateId = this.notifyTemplateSettings.ProblemWithTheFormResponseNeededTemplateId;
			}
			else
			{
				personalization.Add("what_problem_did_you_notice", ProblemSummary);
				this.templateId = this.notifyTemplateSettings.ProblemWithTheFormNoResponseNeededTemplateId;
			}

			var message = new MessageDto(this.configuration["emailnotifications:supportemail"], this.templateId)
			{
				Personalisation = personalization,
			};

			await this.emailNotificationService.SendAsync(message);

			return RedirectToPage("ThankYou", new { helpTypeId = HelpTypes.Problem });
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

			if (DoYouWantToBeContacted == SelectOption.Yes && string.IsNullOrWhiteSpace(EmailAddress))
			{
				ModelState.AddModelError("ContactdByEmailError", "You must give an email address");
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
