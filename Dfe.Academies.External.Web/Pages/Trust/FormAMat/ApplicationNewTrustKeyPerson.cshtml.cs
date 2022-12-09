using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	public class ApplicationNewTrustKeyPersonModel : BaseTrustFamApplicationPageEditModel
	{
		public string TrustKeyPersonDobDate = "sip_formtrustkeypersondate";
		public string TrustName { get; private set; } = string.Empty;

		[BindProperty]
		public string TrustKeyPersonDob { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string TrustKeyPersonDobDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string TrustKeyPersonDobMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string TrustKeyPersonDobYear { get; set; }

		[BindProperty]
		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "You must input a valid name")]
		[Required(ErrorMessage = "Name is required")]
		public string TrustKeyPersonName { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must provide support details")]
		public string TrustKeyPersonBiography { get; set; } = string.Empty;

		public DateTime TrustKeyPersonDobLocal { get; set; }

		[BindProperty]
		public bool TrustKeyPersonMember { get; set; }

		[BindProperty]
		public bool TrustKeyPersonTrustee { get; set; }

		[BindProperty]
		public bool TrustKeyPersonCeo { get; set; }

		[BindProperty]
		public bool TrustKeyPersonChair { get; set; }

		[BindProperty]
		public bool TrustKeyPersonFinancialDirector { get; set; }

		[BindProperty]
		public bool TrustKeyPersonOther { get; set; }


		public bool TrustKeyPersonDobError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustKeyPersonDobNotEntered");
			}
		}

		public ApplicationNewTrustKeyPersonModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
												IReferenceDataRetrievalService referenceDataRetrievalService,
												IConversionApplicationCreationService conversionApplicationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"ApplicationNewTrustSummary")
		{
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, TrustKeyPersonDobDate);
			string day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			TrustKeyPersonDobLocal = BuildDateTime(day, month, year);

			if (!RunUiValidation())
			{
				// MR:- date input disappears without below !!
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var roles = new List<NewTrustKeyPersonRole>();

			if (TrustKeyPersonCeo)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.CEO, string.Empty));
			}

			if (TrustKeyPersonChair)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Chair, string.Empty));
			}

			if (TrustKeyPersonFinancialDirector)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.FinancialDirector, "Time in role ToDo"));
			}

			if (TrustKeyPersonTrustee)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Trustee, string.Empty));
			}

			if (TrustKeyPersonMember)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Member, string.Empty));
			}

			if (TrustKeyPersonOther)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Other, string.Empty));
			}

			var newKeyPerson = new NewTrustKeyPerson(TrustKeyPersonName, TrustKeyPersonDobLocal,
				TrustKeyPersonBiography, roles);

			await ConversionApplicationCreationService.CreateKeyPerson(ApplicationId, newKeyPerson);

			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			// need to go onto next step in process 'reasons for conversion page'
			return RedirectToPage(NextStepPage, new { appId = ApplicationId });
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			if (TrustKeyPersonDobLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("TrustKeyPersonDobLocalNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				return false;
			}

			// date not less < today
			if (TrustKeyPersonDobLocal <= DateTime.Now.Date)
			{
				ModelState.AddModelError("TrustKeyPersonDobLocalEntered", "Opening date must be in the future");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new Dictionary<string, dynamic>
			{
			};
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
			}
		}

		private void RePopDatePickerModel(string openingDateDay, string openingDateMonth, string openingDateYear)
		{
			TrustKeyPersonDobDay = openingDateDay;
			TrustKeyPersonDobMonth = openingDateMonth;
			TrustKeyPersonDobYear = openingDateYear;
		}
	}
}
