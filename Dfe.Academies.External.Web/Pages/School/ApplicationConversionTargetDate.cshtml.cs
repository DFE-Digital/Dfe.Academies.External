using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationConversionTargetDateModel : BaseSchoolPageEditModel
	{
		public string SchoolConversionTargetDateDate = "sip_ctddiferentdatevalue";

		// MR:- VM props to capture data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption? TargetDateDifferent { get; set; }

		/// <summary>
		/// Full 'Date' representation of date selected
		/// i.e. day + month + year entered !
		/// </summary>
		[BindProperty]
		public string? TargetDate { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TargetDateDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TargetDateMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TargetDateYear { get; set; }

		[BindProperty]
		public string? TargetDateExplained { get; set; }

		public DateTime TargetDateLocal { get; set; }

		public bool HasError
		{
			get
			{
				var bools = new[] { SchoolConversionTargetDateError, TargetDateExplainedError };

				return bools.Any(b => b);
			}
		}

		public bool SchoolConversionTargetDateError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("SchoolConversionTargetDateNotEntered");
			}
		}

		public bool TargetDateExplainedError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TargetDateExplainedNotEntered");
			}
		}

		public ApplicationConversionTargetDateModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "ApplicationJoinTrustReasons")
		{}

		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;
			//var id = Convert.ToInt32(form["ApplicationId"]);

			// MR:- try and build a date from component parts !!!
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, SchoolConversionTargetDateDate);
			string day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			TargetDateLocal = BuildDateTime(day, month, year);

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
			await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			// need to go onto next step in process 'reasons for conversion page'
			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
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

			if (TargetDateDifferent == SelectOption.Yes && TargetDateLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("SchoolConversionTargetDateNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				return false;
			}

			if (TargetDateDifferent == SelectOption.Yes && string.IsNullOrWhiteSpace(TargetDateExplained))
			{
				ModelState.AddModelError("TargetDateExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// if no specific date the school wants to convert, blank out 'SchoolConversionTargetDate' && 'SchoolConversionTargetDateExplained'
			if (TargetDateDifferent == SelectOption.No)
			{
				return new Dictionary<string, dynamic>
				{
					{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateSpecified), Convert.ToBoolean(TargetDateDifferent) },
					{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDate), null },
					{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateExplained), null }
				};
			}
			else
			{
				return new Dictionary<string, dynamic>
				{
					{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateSpecified), Convert.ToBoolean(TargetDateDifferent) },
					{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDate), TargetDateLocal },
					{ nameof(SchoolApplyingToConvert.SchoolConversionTargetDateExplained), TargetDateExplained }
				};
			}
		}

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			var conversionDateSpecified = selectedSchool.SchoolConversionTargetDateSpecified.GetEnumValue();

			if (conversionDateSpecified.HasValue)
			{
				TargetDateDifferent = conversionDateSpecified.Value;
			}

			TargetDate = selectedSchool.SchoolConversionTargetDate.ToString();
			TargetDateExplained = selectedSchool.SchoolConversionTargetDateExplained;
		}

		private void RePopDatePickerModel(string worksPlannedDateDay, string worksPlannedDateMonth, string worksPlannedDateYear)
		{
			TargetDateDay = worksPlannedDateDay;
			TargetDateMonth = worksPlannedDateMonth;
			TargetDateYear = worksPlannedDateYear;
		}
	}
}
