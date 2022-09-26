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
		private readonly ILogger<PreviousFinancialYearModel> _logger;
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
		[RequiredEnum(ErrorMessage = "You must provide details")]
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
		[RequiredEnum(ErrorMessage = "You must provide details")]
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

		public PreviousFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
										IReferenceDataRetrievalService referenceDataRetrievalService,
										ILogger<PreviousFinancialYearModel> logger,
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
					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::PreviousFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			// MR:- try and build a date from component parts !!!
			var PFYEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PFYEndDateFormInputName);
			var PFYEndDateComponentDay = PFYEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			var PFYEndDateComponentMonth = PFYEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			var PFYEndDateComponentYear = PFYEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var PFYEndDate = BuildDateTime(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);

			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}

			if (PFYEndDate == DateTime.MinValue)
			{
				ModelState.AddModelError("PFYFinancialEndDateNotEntered", "You must give a valid date");
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}
			
			if (PFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYRevenueStatusExplained))
			{
				ModelState.AddModelError("PFYRevenueStatusExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}
			
			if (PFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYCapitalCarryForwardExplained))
			{
				ModelState.AddModelError("PFYCapitalCarryForwardExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication =
					TempDataHelper.GetSerialisedValue<ConversionApplication>(
						TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				var previousFinancialYear = new SchoolFinancialYear(PFYEndDate,
					Revenue,
					PFYRevenueStatus,
					PFYRevenueStatusExplained,
					null,
					CapitalCarryForward,
					PFYCapitalCarryForwardStatus,
					PFYCapitalCarryForwardExplained,
					null);

				var mappingDictionary =
					new Dictionary<string, dynamic> { { "PreviousFinancialYear", previousFinancialYear } };
				await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, mappingDictionary);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("FinancesReview", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::PreviousFinancialYearModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}

		}

		public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
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
