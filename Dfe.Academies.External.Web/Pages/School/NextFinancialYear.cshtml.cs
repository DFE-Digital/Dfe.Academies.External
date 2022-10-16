using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class NextFinancialYearModel : BasePageEditModel
{
    private readonly IConversionApplicationCreationService _academisationCreationService;
    public string NFYEndDateFormInputName = "sip_nfyenddate";

    //// MR:- selected school props for UI rendering
    [BindProperty]
    public int ApplicationId { get; set; }

    [BindProperty]
    public int Urn { get; set; }

    public string SchoolName { get; private set; } = string.Empty;

	//// MR:- VM props to capture Nfy data
	[BindProperty]
	public string? NFYEndDate { get; set; }

	[BindProperty] // MR:- don't know whether I need this
	public string? NFYEndDateDay { get; set; }

	[BindProperty] // MR:- don't know whether I need this
	public string? NFYEndDateMonth { get; set; }

	[BindProperty] // MR:- don't know whether I need this
	public string? NFYEndDateDateYear { get; set; }

	[BindProperty]
	[Range(0, 200000000000000, ErrorMessage = "Revenue amount must be greater than 0")]
	[Required(ErrorMessage = "You must provide a revenue amount")]
	public decimal Revenue { get; set; }

	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must select an option")]
	public RevenueType NFYRevenueStatus { get; set; }

	[BindProperty]
	public string? NFYRevenueStatusExplained { get; set; }

	// TODO MR:- below, once file upload whoopsy sorted!
	//string? RevenueStatusFileLink = null,

	[BindProperty]
	[Range(0, 200000000000000, ErrorMessage = "Capital carry forward amount must be greater than 0")]
	[Required(ErrorMessage = "You must provide a capital carry forward amount")]
	public decimal CapitalCarryForward { get; set; }

	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must select an option")]
	public RevenueType NFYCapitalCarryForwardStatus { get; set; }

	[BindProperty]
	public string? NFYCapitalCarryForwardExplained { get; set; }

	// TODO MR:- below, once file upload whoopsy sorted!
	//string? CapitalCarryForwardFileLink = null

	public bool NFYFinancialEndDateError
	{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("NFYFinancialEndDateNotEntered"))
			{
				return true;
			}

			return false;
		}
	}

	public bool NFYRevenueStatusExplainedError
	{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("NFYRevenueStatusExplainedNotEntered"))
			{
				return true;
			}

			return false;
		}
	}

	public bool NFYCapitalCarryForwardStatusExplainedError
	{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("NFYCapitalCarryForwardExplainedNotEntered"))
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
			var bools = new[] { NFYFinancialEndDateError,
				NFYRevenueStatusExplainedError,
				NFYCapitalCarryForwardStatusExplainedError
			};

			return bools.Any(b => b);
		}
	}

	public DateTime NFYFinancialEndDateLocal { get; set; }

	public NextFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
	    IReferenceDataRetrievalService referenceDataRetrievalService,
	    ILogger<NextFinancialYearModel> logger,
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
	    var nfyEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, NFYEndDateFormInputName);
	    string NFYEndDateComponentDay = nfyEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
	    string NFYEndDateComponentMonth = nfyEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
	    string NFYEndDateComponentYear = nfyEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

	    NFYFinancialEndDateLocal = BuildDateTime(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);

	    if (!RunUiValidation())
	    {
		    // MR:- date input disappears without below !!
		    RePopDatePickerModel(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);
			return Page();
	    }
		
		// grab draft application from temp= null
		var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		var dictionaryMapper = PopulateUpdateDictionary();
		await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

		// update temp store for next step - application overview
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

		return RedirectToPage("Loans", new { appId = ApplicationId, urn = Urn });
	}

    ///<inheritdoc/>
    public override bool RunUiValidation()
    {
	    if (!ModelState.IsValid)
	    {
		    PopulateValidationMessages();
		    return false;
	    }

	    if (NFYFinancialEndDateLocal == DateTime.MinValue)
	    {
		    ModelState.AddModelError("NFYFinancialEndDateNotEntered", "You must input a valid date");
		    PopulateValidationMessages();
		    return false;
	    }

	    if (NFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(NFYRevenueStatusExplained))
	    {
		    ModelState.AddModelError("NFYRevenueStatusExplainedNotEntered", "You must provide details");
		    PopulateValidationMessages();
		    return false;
	    }

	    if (NFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(NFYCapitalCarryForwardExplained))
	    {
		    ModelState.AddModelError("NFYCapitalCarryForwardExplainedNotEntered", "You must provide details");
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
		// if 'NFYRevenueStatus' == Surplus, blank out 'PFYRevenueStatusExplained'
		if (NFYRevenueStatus == RevenueType.Surplus)
		{
			NFYRevenueStatusExplained = null;
		}

		// if 'NFYCapitalCarryForwardStatus' == Surplus, blank out 'PFYCapitalCarryForwardExplained'
		if (NFYCapitalCarryForwardStatus == RevenueType.Surplus)
		{
			NFYCapitalCarryForwardExplained = null;
		}

		var nextFinancialYear = new SchoolFinancialYear(NFYFinancialEndDateLocal,
			Revenue,
			NFYRevenueStatus,
			NFYRevenueStatusExplained,
			null,
			CapitalCarryForward,
			NFYCapitalCarryForwardStatus,
			NFYCapitalCarryForwardExplained,
			null);

		return new Dictionary<string, dynamic> { {nameof(SchoolApplyingToConvert.NextFinancialYear), nextFinancialYear} };
	}

	private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
		SchoolName = selectedSchool.SchoolName;
		NFYEndDate = (selectedSchool.NextFinancialYear.FinancialYearEndDate.HasValue ?
			selectedSchool.NextFinancialYear.FinancialYearEndDate.Value.ToString("dd/MM/yyyy")
			: string.Empty);

		// Revenue
		if (selectedSchool.NextFinancialYear.Revenue != null)
		{
			Revenue = selectedSchool.NextFinancialYear.Revenue.Value;
		}

		if (selectedSchool.NextFinancialYear.RevenueStatus != null)
		{
			NFYRevenueStatus = selectedSchool.NextFinancialYear.RevenueStatus.Value;
		}

		NFYRevenueStatusExplained = selectedSchool.NextFinancialYear.RevenueStatusExplained;

		// CCF
		if (selectedSchool.NextFinancialYear.CapitalCarryForward != null)
		{
			CapitalCarryForward = selectedSchool.NextFinancialYear.CapitalCarryForward.Value;
		}

		if (selectedSchool.NextFinancialYear.CapitalCarryForwardStatus != null)
		{
			NFYCapitalCarryForwardStatus = selectedSchool.NextFinancialYear.CapitalCarryForwardStatus.Value;
		}

		NFYCapitalCarryForwardExplained = selectedSchool.NextFinancialYear.CapitalCarryForwardExplained;
	}

	private void RePopDatePickerModel(string nfyEndDateComponentDay, string nfyEndDateComponentMonth, string nfyEndDateComponentYear)
	{
		NFYEndDateDay = nfyEndDateComponentDay;
		NFYEndDateMonth = nfyEndDateComponentMonth;
		NFYEndDateDateYear = nfyEndDateComponentYear;
	}
}

