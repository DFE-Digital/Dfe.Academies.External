using Dfe.Academies.External.Web.Constants;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
		public DateTime? TargetDate { get; set; }

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
				string newView = "ConversionTargetDate";
				SetSchoolViewData(appId, selectedSchool, newView);
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationConversionTargetDateModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			if (TargetDateDifferent == SelectOption.Yes && !TargetDate.HasValue)
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
			//TargetDateDifferent = ;
			//TargetDate = ;
			//TargetDateExplained = ;
		}

		private void SetSchoolViewData(int appId, SchoolApplyingToConvert? school, string view)
		{
			SetViewDataProperties(appId, view, school);

			ViewData[FieldConstants.SipApplyingSchoolsId] = school.URN;
		}

		private void SetViewDataProperties(int appId, string nextPage, object entity)
		{
			SetCommonViewDataProperties(nextPage, appId);

			if (appId == 0)
			{
				// create viewData[] pointers for each property within 'object entity'
				SetDataProperties(nextPage, entity);
			}
		}

		private void SetCommonViewDataProperties(string nextPage, int appId)
		{
			ViewData[FieldConstants.CurrentPage] = nextPage;
			ViewData[FieldConstants.SipApplicationId] = appId;
		}

		/// <summary>
		/// Sets the ViewData with the returned view properties
		/// </summary>
		/// <param name="nextPage"></param>
		/// <param name="entity">e.g. school object</param>
		private void SetDataProperties(string nextPage, object entity)
		{
			// MR:- get a dictionary / array / list of view property keys
			// to then populate the value from the entity object
			var viewProperties = GetViewFieldProperties();
			if (!viewProperties.Any())
			{
				return;
			}

			// TODO MR:- hacked this about, might not work !!
			var data = entity.GetType()
				.GetProperties()
				.ToDictionary(property => property.Name, property => property.GetValue(entity));

			foreach (KeyValuePair<string, string> field in viewProperties)
			{
				var fieldName = field.Value; // property name

				if (data.ContainsKey(fieldName))
				{
					ViewData[field.Key] = data[fieldName] switch
					{
						DateTime dateTime => dateTime.ToString(),
						object obj => obj.ToString(),
						_ => string.Empty
					};
				}
				else
				{
					ViewData[field.Key] = string.Empty;
				}
			}
		}

		/// <summary>
		/// Get a list / dictionary / array of view / field names. 
		/// This MUST match the property names of the object you're searching / matching
		/// within SetDataProperties()
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, string> GetViewFieldProperties()
		{
			Dictionary<string, string> viewFields = new Dictionary<string, string>();

			// add 3 keys to dictionary representing conversion date properties / data
			//FieldConstants.SipSchoolConversionTargetDateExplained
			viewFields.Add(FieldConstants.SipSchoolConversionTargetDateExplained, "SchoolConversionTargetDateExplained");

			//FieldConstants.SipSchoolConversionTargetDateDate = SchoolConversionTargetDate
			viewFields.Add(FieldConstants.SipSchoolConversionTargetDateDate, "SchoolConversionTargetDate");

			//FieldConstants.SipSchoolConversionTargetDateDifferent
			viewFields.Add(FieldConstants.SipSchoolConversionTargetDateDifferent, "");

			//field - day = "@ViewData[FieldConstants.SipSchoolConversionTargetDateDate + " - day"]"
			//field - month = "@ViewData[FieldConstants.SipSchoolConversionTargetDateDate + " - month"]"
			//field - year = "@ViewData[FieldConstants.SipSchoolConversionTargetDateDate + " - year"]" >

			return viewFields;
		}
	}
}
