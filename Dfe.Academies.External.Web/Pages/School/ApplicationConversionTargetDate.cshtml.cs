using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Dfe.Academies.External.Web.Constants;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationConversionTargetDateModel : BasePageEditModel
    {
	    private readonly ILogger<ApplicationConversionTargetDateModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    private const string NextStepPage = "ApplicationJoinTrustReasons";

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

				// MR:- code from a2c-sip for date picker - set up ViewData[] dictionary with object values in it
				// string newView = "ConversionTargetDate";
				// SetSchoolViewData(appId, selectedSchool, newView);
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationConversionTargetDateModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			//var id = Convert.ToInt32(form["ApplicationId"]);
			form.TryGetValue($"{FieldConstants.SipSchoolConversionTargetDateDate}-day", out StringValues day);
			form.TryGetValue($"{FieldConstants.SipSchoolConversionTargetDateDate}-month", out StringValues month);
			form.TryGetValue($"{FieldConstants.SipSchoolConversionTargetDateDate}-year", out StringValues year);

			// MR:- try and build a date from component parts !!!
			var targetDate = BuildDateTime(day,month,year);

			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			if ((TargetDateDifferent == SelectOption.Yes && !targetDate.HasValue) 
			    || (TargetDateDifferent == SelectOption.Yes && targetDate.HasValue && targetDate.Value != DateTime.MinValue))
			{
				ModelState.AddModelError("SchoolConversionTargetDateNotEntered", "You must select a conversion date");
				PopulateValidationMessages();
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

				// TODO MR:- call API endpoint to log data
				// await _academisationCreationService.UpdateSchoolConversionDate(TargetDate, TargetDateExplained);

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
			// TargetDateDifferent = selectedSchool.;
			// TargetDate = selectedSchool.;
			// TargetDateExplained = selectedSchool.;
		}

		//private void SetSchoolViewData(int appId, SchoolApplyingToConvert? school, string viewName)
		//{
		//	SetViewDataProperties(appId, viewName, school);

		//	// MR:- not sure I need this??
		//	ViewData[FieldConstants.SipApplyingSchoolsId] = school.URN;
		//}

		//private void SetViewDataProperties(int appId, string nextPage, object? entity)
		//{
		//	// MR:- not sure I need this??
		//	SetCommonViewDataProperties(nextPage, appId);

		//	if (appId != 0)
		//	{
		//		// create ViewData[] pointers for each property within 'object entity'
		//		SetDataProperties(entity);
		//	}
		//}

		///// <summary>
		///// MR:- not sure I need this??
		///// </summary>
		///// <param name="nextPage"></param>
		///// <param name="appId"></param>
		//private void SetCommonViewDataProperties(string nextPage, int appId)
		//{
		//	ViewData[FieldConstants.CurrentPage] = nextPage;
		//	ViewData[FieldConstants.SipApplicationId] = appId;
		//}

		///// <summary>
		///// Sets the ViewData[] with the returned view properties
		///// </summary>
		///// <param name="entity">e.g. school object</param>
		//private void SetDataProperties(object? entity)
		//{
		//	// MR:- get a dictionary / array / list of view property keys
		//	var viewProperties = GetViewFieldProperties();
		//	if (!viewProperties.Any())
		//	{
		//		return;
		//	}

		//	var data = entity.GetType()
		//		.GetProperties()
		//		.ToDictionary(property => property.Name, property => property.GetValue(entity));

		//	// populate the ViewData[] value from the entity object
		//	foreach (KeyValuePair<string, string> field in viewProperties)
		//	{
		//		var fieldName = field.Value; // property name

		//		if (data.ContainsKey(fieldName))
		//		{
		//			ViewData[field.Key] = data[fieldName] switch
		//			{
		//				DateTime dateTime => dateTime.ToString(CultureInfo.CurrentCulture),
		//				object obj => obj.ToString(),
		//				_ => string.Empty
		//			};
		//		}
		//		else
		//		{
		//			ViewData[field.Key] = string.Empty;
		//		}
		//	}
		//}

		///// <summary>
		///// Get a list / dictionary / array of view / field names. 
		///// This MUST match the property names of the object you're searching / matching
		///// within SetDataProperties()
		///// </summary>
		///// <returns></returns>
		//private Dictionary<string, string> GetViewFieldProperties()
		//{
		//	// add keys into dictionary representing conversion date properties / data
		//	// what are consumed by govuk-date tag helper
		//	Dictionary<string, string> viewFields = new()
		//	{
		//		{ FieldConstants.SipSchoolConversionTargetDateExplained, "SchoolConversionTargetDateExplained" },

		//		{ FieldConstants.SipSchoolConversionTargetDateDate, "SchoolConversionTargetDate" },

		//		// FieldConstants.SipSchoolConversionTargetDateDifferent - NOT in SchoolApplyingToConvert
		//		// as NOT in API
		//		{ FieldConstants.SipSchoolConversionTargetDateDifferent, "" },

		//		// MR:- setting up 3 below as consumed by govuk-date tag helper
		//		// value for below will be 'dd' component of "SchoolConversionTargetDate"
		//		{ $"{FieldConstants.SipSchoolConversionTargetDateDate}-day", "" },

		//		// value for below will be 'MM' component of "SchoolConversionTargetDate"
		//		{ $"{FieldConstants.SipSchoolConversionTargetDateDate}-month", "" },

		//		// value for below will be 'yyyy' component of "SchoolConversionTargetDate"
		//		{ $"{FieldConstants.SipSchoolConversionTargetDateDate}-year", "" }
		//	};

		//	return viewFields;
		//}

		public DateTime? BuildDateTime(string day, string month, string year)
		{
			string dateString = $"{day}/{month}/{year}"; 
			string format = "dd/MM/yyyy";
			
			DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,
				DateTimeStyles.None, out DateTime newDate);

			return newDate;
		}
    }
}
