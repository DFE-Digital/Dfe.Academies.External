using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class PreviousFinancialYearModel : BasePageEditModel
	{
		private readonly IConversionApplicationCreationService _academisationCreationService;
		public string PFYEndDateFormInputName = "sip_pfyenddate";

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture Pfy data
		
		[BindProperty]
		public string? PFYEndDate { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? PFYEndDateDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? PFYEndDateMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? PFYEndDateDateYear { get; set; }

		[BindProperty]
		[Range(0, 200000000000000, ErrorMessage = "Revenue amount must be greater than 0")]
		[Required(ErrorMessage = "You must provide a revenue amount")]
		public decimal Revenue { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public RevenueType PFYRevenueStatus { get; set; }

		[BindProperty]
		public string? PFYRevenueStatusExplained { get; set; }
		
		// TODO MR:- below, once file upload whoopsy sorted!
		//string? RevenueStatusFileLink = null,

		[BindProperty]
		[Range(0, 200000000000000, ErrorMessage = "Capital carry forward amount must be greater than 0")]
		[Required(ErrorMessage = "You must provide a capital carry forward amount")]
		public decimal CapitalCarryForward { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public RevenueType PFYCapitalCarryForwardStatus { get; set; }

		[BindProperty]
		public string? PFYCapitalCarryForwardExplained { get; set; }

		// TODO MR:- below, once file upload whoopsy sorted!
		//string? CapitalCarryForwardFileLink = null

		public bool PFYFinancialEndDateError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("PFYFinancialEndDateNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool PFYRevenueStatusExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("PFYRevenueStatusExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool PFYCapitalCarryForwardStatusExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("PFYCapitalCarryForwardExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool HasError
		{
			get
			{
				var bools = new[] { PFYFinancialEndDateError,
					PFYRevenueStatusExplainedError,
					PFYCapitalCarryForwardStatusExplainedError
				};

				return bools.Any(b => b);
			}
		}

		public DateTime PFYFinancialEndDateLocal { get; set; }

		public PreviousFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
										IReferenceDataRetrievalService referenceDataRetrievalService,
										IConversionApplicationCreationService academisationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
	        _academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			// MR:- try and build a date from component parts !!!
			var pfyEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PFYEndDateFormInputName);
			string PFYEndDateComponentDay = pfyEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string PFYEndDateComponentMonth = pfyEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string PFYEndDateComponentYear = pfyEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			PFYFinancialEndDateLocal = BuildDateTime(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);

			if (!RunUiValidation())
			{
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}
			
			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = PopulateUpdateDictionary();
			await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage("CurrentFinancialYear", new { appId = ApplicationId, urn = Urn });
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			if (PFYFinancialEndDateLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("PFYFinancialEndDateNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				return false;
			}

			if (PFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYRevenueStatusExplained))
			{
				ModelState.AddModelError("PFYRevenueStatusExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (PFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYCapitalCarryForwardExplained))
			{
				ModelState.AddModelError("PFYCapitalCarryForwardExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// if 'PFYRevenueStatus' == Surplus, blank out 'PFYRevenueStatusExplained'
			if (PFYRevenueStatus == RevenueType.Surplus)
			{
				PFYRevenueStatusExplained = null;
			}

			// if 'PFYCapitalCarryForwardStatus' == Surplus, blank out 'PFYCapitalCarryForwardExplained'
			if (PFYCapitalCarryForwardStatus == RevenueType.Surplus)
			{
				PFYCapitalCarryForwardExplained = null;
			}

			var previousFinancialYear = new SchoolFinancialYear(PFYFinancialEndDateLocal,
				Revenue,
				PFYRevenueStatus,
				PFYRevenueStatusExplained,
				null,
				CapitalCarryForward,
				PFYCapitalCarryForwardStatus,
				PFYCapitalCarryForwardExplained,
				null);

			return new Dictionary<string, dynamic> { { nameof(SchoolApplyingToConvert.PreviousFinancialYear), previousFinancialYear } };
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			PFYEndDate = (selectedSchool.PreviousFinancialYear.FinancialYearEndDate.HasValue ?
				selectedSchool.PreviousFinancialYear.FinancialYearEndDate.Value.ToString("dd/MM/yyyy")
				: string.Empty);

			// Revenue
			if (selectedSchool.PreviousFinancialYear.Revenue != null)
			{
				Revenue = selectedSchool.PreviousFinancialYear.Revenue.Value;
			}

			if (selectedSchool.PreviousFinancialYear.RevenueStatus != null)
			{
				PFYRevenueStatus = selectedSchool.PreviousFinancialYear.RevenueStatus.Value;
			}

			PFYRevenueStatusExplained = selectedSchool.PreviousFinancialYear.RevenueStatusExplained;

			// CCF
			if (selectedSchool.PreviousFinancialYear.CapitalCarryForward != null)
			{
				CapitalCarryForward = selectedSchool.PreviousFinancialYear.CapitalCarryForward.Value;
			}

			if (selectedSchool.PreviousFinancialYear.CapitalCarryForwardStatus != null)
			{
				PFYCapitalCarryForwardStatus = selectedSchool.PreviousFinancialYear.CapitalCarryForwardStatus.Value;
			}

			PFYCapitalCarryForwardExplained = selectedSchool.PreviousFinancialYear.CapitalCarryForwardExplained;
		}

		private void RePopDatePickerModel(string pfyEndDateComponentDay, string pfyEndDateComponentMonth, string pfyEndDateComponentYear)
		{
			PFYEndDateDay = pfyEndDateComponentDay;
			PFYEndDateMonth = pfyEndDateComponentMonth;
			PFYEndDateDateYear = pfyEndDateComponentYear;
		}
	}
}
