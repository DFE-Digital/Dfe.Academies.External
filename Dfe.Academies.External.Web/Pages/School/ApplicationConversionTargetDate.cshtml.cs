using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationConversionTargetDateModel : BasePageEditModel
    {
	    private readonly ILogger<ApplicationConversionTargetDateModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    private const string NextStepPage = "ApplicationJoinTrustReasons";
	    public string SchoolConversionTargetDateDate = "sip_ctddiferentdatevalue";

		//// MR:- selected school props for UI rendering
		[BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture data

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption TargetDateDifferent { get; set; }

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

		public bool HasError {
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
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolConversionTargetDateNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool TargetDateExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("TargetDateExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public ApplicationConversionTargetDateModel(ILogger<ApplicationConversionTargetDateModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			try
			{
				LoadAndStoreCachedConversionApplication();

				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
				ApplicationId = appId;
				Urn = urn;

				// Grab other values from API
				if (selectedSchool != null)
				{
					// TODO MR:- grab existing data from API endpoint to populate VM - applicationId && SchoolId combination !

					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationConversionTargetDateModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			//var id = Convert.ToInt32(form["ApplicationId"]);

			// MR:- try and build a date from component parts !!!
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, SchoolConversionTargetDateDate);
			var day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			var month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			var year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var targetDate = BuildDateTime(day, month, year);

			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			if (TargetDateDifferent == SelectOption.Yes && targetDate == DateTime.MinValue)
			{
				ModelState.AddModelError("SchoolConversionTargetDateNotEntered", "You must select a conversion date");
				PopulateValidationMessages();
				//TargetDate = $"{day}/{month}/{year}"; // MR:- CAN'T set this in this scenario as taghelper expects REAL datetime, not invalid one
				TargetDateDay = day;
				TargetDateMonth = month;
				TargetDateYear = year;
				return Page();
			}

			if (TargetDateDifferent == SelectOption.Yes && string.IsNullOrWhiteSpace(TargetDateExplained))
			{
				ModelState.AddModelError("TargetDateExplainedNotEntered", "You must explain why you want to convert on this date");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// MR:- call API endpoint to log data
				await _academisationCreationService.ApplicationSchoolTargetConversionDate(ApplicationId, Urn, targetDate, TargetDateExplained);

				// update temp store for next step
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				// need to go onto next step in process 'reasons for conversion page'
				return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationConversionTargetDateModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		/// <summary>
		/// Error messages component consumes ViewData["Errors"] so populate it
		/// </summary>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			// TODO MR:- bind below from API data
			// TargetDateDifferent = selectedSchool.; // MR:- not implemented in API 01/09/2022 
			// TargetDate = selectedSchool.SchoolConversionTargetDate;
			// TargetDateExplained = selectedSchool.SchoolConversionTargetDateExplained;
		}
	}
}
