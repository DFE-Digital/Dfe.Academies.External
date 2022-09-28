using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.School
{
	 public class CurrentFinancialYearModel : BasePageEditModel
	{
		private readonly ILogger<CurrentFinancialYearModel> _logger;
		private readonly IConversionApplicationCreationService _academisationCreationService;
		public string CFYEndDateFormInputName = "sip_cfyenddate";
		
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		[BindProperty]
		public string? CFYEndDate { get; set; }

		[BindProperty] 
		public string? CFYEndDateDay { get; set; }

		[BindProperty]
		public string? CFYEndDateMonth { get; set; }

		[BindProperty] 
		public string? CFYEndDateDateYear { get; set; }

		[BindProperty]
		public decimal? Revenue { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public RevenueType CFYRevenueStatus { get; set; }

		[BindProperty]
		public string? CFYRevenueStatusExplained { get; set; }
		
		[BindProperty]
		public decimal? CapitalCarryForward { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public RevenueType CFYCapitalCarryForwardStatus { get; set; }

		[BindProperty]
		public string? CFYCapitalCarryForwardExplained { get; set; }

		public bool CFYFinancialEndDateError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("CFYFinancialEndDateNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool CFYRevenueStatusExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("CFYRevenueStatusExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool CFYCapitalCarryForwardStatusExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("CFYCapitalCarryForwardExplainedNotEntered"))
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
				var bools = new[] { CFYFinancialEndDateError,
					CFYRevenueStatusExplainedError,
					CFYCapitalCarryForwardStatusExplainedError
				};

				return bools.Any(b => b);
			}
		}

		public CurrentFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
										IReferenceDataRetrievalService referenceDataRetrievalService,
										ILogger<CurrentFinancialYearModel> logger,
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
				_logger.LogError("School::CurrentFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			// MR:- try and build a date from component parts !!!
			var CFYEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, CFYEndDateFormInputName);
			var CFYEndDateComponentDay = CFYEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			var CFYEndDateComponentMonth = CFYEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			var CFYEndDateComponentYear = CFYEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var CFYEndDate = BuildDateTime(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);

			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);
				return Page();
			}

			if (CFYEndDate == DateTime.MinValue)
			{
				ModelState.AddModelError("CFYFinancialEndDateNotEntered", "You must give a valid date");
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);
				return Page();
			}

			if (CFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(CFYRevenueStatusExplained))
			{
				ModelState.AddModelError("CFYRevenueStatusExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);
				return Page();
			}

			if (CFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(CFYCapitalCarryForwardExplained))
			{
				ModelState.AddModelError("PFYCapitalCarryForwardExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				// MR:- date input disappears without below !!
				RePopDatePickerModel(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				var currentFinancialYear = new SchoolFinancialYear(CFYEndDate, 
																		Revenue, 
																		CFYRevenueStatus, 
																		CFYRevenueStatusExplained, 
																		null,
																		CapitalCarryForward,
																		CFYCapitalCarryForwardStatus,
																		CFYCapitalCarryForwardExplained,
																		null);

				var propertiesToPopulate =
					new Dictionary<string, dynamic>
					{
						{nameof(SchoolApplyingToConvert.CurrentFinancialYear), currentFinancialYear}
					};
				
				await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, propertiesToPopulate);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("NextFinancialYear", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::CurrentFinancialYearModel::OnPostAsync::Exception - {Message}", ex.Message);
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
			CFYEndDate = (selectedSchool.CurrentFinancialYear.FinancialYearEndDate.HasValue ?
				selectedSchool.CurrentFinancialYear.FinancialYearEndDate.Value.ToString("dd/MM/yyyy")
				: string.Empty);
			// Revenue
			Revenue = selectedSchool.CurrentFinancialYear.Revenue;
			if (selectedSchool.CurrentFinancialYear.RevenueStatus != null)
			{
				CFYRevenueStatus = selectedSchool.CurrentFinancialYear.RevenueStatus.Value;
			}

			CFYRevenueStatusExplained = selectedSchool.CurrentFinancialYear.RevenueStatusExplained;
			// CCF
			CapitalCarryForward = selectedSchool.CurrentFinancialYear.CapitalCarryForward;
			if (selectedSchool.CurrentFinancialYear.CapitalCarryForwardStatus != null)
			{
				CFYCapitalCarryForwardStatus = selectedSchool.CurrentFinancialYear.CapitalCarryForwardStatus.Value;
			}

			CFYCapitalCarryForwardExplained = selectedSchool.CurrentFinancialYear.CapitalCarryForwardExplained;
		}

		private void RePopDatePickerModel(string cfyEndDateComponentDay, string cfyEndDateComponentMonth, string cfyEndDateComponentYear)
		{
			CFYEndDateDay = cfyEndDateComponentDay;
			CFYEndDateMonth = cfyEndDateComponentMonth;
			CFYEndDateDateYear = cfyEndDateComponentYear;
		}
	}
}
