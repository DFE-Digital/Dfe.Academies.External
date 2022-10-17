using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class CurrentFinancialYearModel : BaseSchoolPageEditModel
{
	public string CFYEndDateFormInputName = "sip_cfyenddate";

	[BindProperty]
	public string? CFYEndDate { get; set; }

	[BindProperty] 
	public string? CFYEndDateDay { get; set; }

	[BindProperty]
	public string? CFYEndDateMonth { get; set; }

	[BindProperty] 
	public string? CFYEndDateDateYear { get; set; }

	[BindProperty]
	[Range(0, 200000000000000, ErrorMessage = "Revenue amount must be greater than 0")]
	[Required(ErrorMessage = "You must provide a revenue amount")]
	public decimal? Revenue { get; set; }

	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must select an option")]
	public RevenueType CFYRevenueStatus { get; set; }

	[BindProperty]
	public string? CFYRevenueStatusExplained { get; set; }
	
	[BindProperty]
	[Range(0, 200000000000000, ErrorMessage = "Capital carry forward amount must be greater than 0")]
	[Required(ErrorMessage = "You must provide a capital carry forward amount")]
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
			return !ModelState.IsValid && ModelState.Keys.Contains("CFYFinancialEndDateNotEntered");
		}
	}

	public bool CFYRevenueStatusExplainedError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("CFYRevenueStatusExplainedNotEntered");
		}
	}

	public bool CFYCapitalCarryForwardStatusExplainedError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("CFYCapitalCarryForwardExplainedNotEntered");
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

	public DateTime CFYFinancialEndDateLocal { get; set; }

	public CurrentFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
									IReferenceDataRetrievalService referenceDataRetrievalService,
									IConversionApplicationCreationService academisationCreationService) 
        : base(conversionApplicationRetrievalService, referenceDataRetrievalService,
	        academisationCreationService, "NextFinancialYear")
    {}

	/// <summary>
	/// different overload because of datepicker stuff!!
	/// </summary>
	/// <returns></returns>
	public override async Task<IActionResult> OnPostAsync()
	{
		var form = Request.Form;

		// MR:- try and build a date from component parts !!!
		var cfyEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, CFYEndDateFormInputName);
		string CFYEndDateComponentDay = cfyEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
		string CFYEndDateComponentMonth = cfyEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
		string CFYEndDateComponentYear = cfyEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

		CFYFinancialEndDateLocal = BuildDateTime(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);

		if (!RunUiValidation())
		{
			// MR:- date input disappears without below !!
			RePopDatePickerModel(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);
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

		return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
	}

	///<inheritdoc/>
	public override bool RunUiValidation()
	{
		if (!ModelState.IsValid)
		{
			PopulateValidationMessages();
			return false;
		}

		if (CFYFinancialEndDateLocal == DateTime.MinValue)
		{
			ModelState.AddModelError("CFYFinancialEndDateNotEntered", "You must input a valid date");
			PopulateValidationMessages();
			return false;
		}

		if (CFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(CFYRevenueStatusExplained))
		{
			ModelState.AddModelError("CFYRevenueStatusExplainedNotEntered", "You must provide details");
			PopulateValidationMessages();
			return false;
		}

		if (CFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(CFYCapitalCarryForwardExplained))
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
		// if 'CFYRevenueStatus' == Surplus, blank out 'CFYRevenueStatusExplained'
		if (CFYRevenueStatus == RevenueType.Surplus)
		{
			CFYRevenueStatusExplained = null;
		}

		// if 'CFYCapitalCarryForwardStatus' == Surplus, blank out 'CFYCapitalCarryForwardExplained'
		if (CFYCapitalCarryForwardStatus == RevenueType.Surplus)
		{
			CFYCapitalCarryForwardExplained = null;
		}

		var currentFinancialYear = new SchoolFinancialYear(CFYFinancialEndDateLocal,
			Revenue,
			CFYRevenueStatus,
			CFYRevenueStatusExplained,
			null,
			CapitalCarryForward,
			CFYCapitalCarryForwardStatus,
			CFYCapitalCarryForwardExplained,
			null);

		return new Dictionary<string, dynamic> { {nameof(SchoolApplyingToConvert.CurrentFinancialYear), currentFinancialYear} };
	}

	public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
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

