using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustOpeningDateModel : BaseTrustFamApplicationPageEditModel
	{
		public string OpeningDateDate = "sip_formtrustopeningdate";

		public string TrustName { get; private set; } = string.Empty;

		public ApplicationStatus ApplicationStatus {get; private set;}

		// MR:- VM props to capture data

		/// <summary>
		/// Full 'Date' representation of date selected
		/// i.e. day + month + year entered !
		/// </summary>
		[BindProperty]
		public string? OpeningDate { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? OpeningDateDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? OpeningDateMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? OpeningDateYear { get; set; }

		[BindProperty]
		[RegularExpression(@"^[A-ZÀ-ÖØ-Þ][a-zà-öø-ÿA-ZÀ-ÖØ-Þ'’-]*(\s[A-ZÀ-ÖØ-Þ][a-zà-öø-ÿA-ZÀ-ÖØ-Þ'’-]*)*$", ErrorMessage = "You must input a valid name")]
		[Required(ErrorMessage = "Name is required")]
		public string? TrustApproverName { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Email address is required")]
		[EmailAddress(ErrorMessage = "You must input a valid email address")]
		public string? TrustApproverEmail { get; set; }

		public DateTime OpeningDateLocal { get; set; }

		public bool HasError
		{
			get
			{
				bool[] bools = new[] { OpeningDateError };

				return bools.Any(b => b);
			}
		}

		public bool OpeningDateError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustOpeningDateNotEntered");
			}
		}

		public ApplicationNewTrustOpeningDateModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationService conversionApplicationCreationService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "ApplicationNewTrustOpeningDateSummary")
		{
		}

		/// <summary>
		/// Override Post because of datepicker madness
		/// </summary>
		/// <returns></returns>
		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, OpeningDateDate);
			string day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			OpeningDateLocal = BuildDateTime(day, month, year);

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

			var dictionaryMapper = PopulateUpdateDictionary();
			await ConversionApplicationCreationService.PutApplicationFormAMatDetails(ApplicationId, dictionaryMapper);
			
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

			if (OpeningDateLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("TrustOpeningDateNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				return false;
			}

			// date not less < today
			if (OpeningDateLocal <= DateTime.Now.Date)
			{
				ModelState.AddModelError("TrustOpeningDateNotEntered", "Opening date must be in the future");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(NewTrust.FormTrustOpeningDate), OpeningDateLocal  },
				{ nameof(NewTrust.TrustApproverName), TrustApproverName ?? string.Empty },
				{ nameof(NewTrust.TrustApproverEmail), TrustApproverEmail ?? string.Empty },
			};
		}

		///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			ApplicationStatus = conversionApplication.ApplicationStatus;
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				OpeningDate = conversionApplication.FormTrustDetails.FormTrustOpeningDate.ToString();
				TrustApproverName = conversionApplication.FormTrustDetails.TrustApproverName;
				TrustApproverEmail = conversionApplication.FormTrustDetails.TrustApproverEmail;
			}
		}

		private void RePopDatePickerModel(string openingDateDay, string openingDateMonth, string openingDateYear)
		{
			OpeningDateDay = openingDateDay;
			OpeningDateMonth = openingDateMonth;
			OpeningDateYear = openingDateYear;
		}
	}
}
