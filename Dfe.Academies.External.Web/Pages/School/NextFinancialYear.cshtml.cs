using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Sentry.Protocol;
using Dfe.Academies.External.Web.CustomValidators;

namespace Dfe.Academies.External.Web.Pages.School;
public class NextFinancialYearModel : BaseSchoolPageEditModel
{
    public string NFYEndDateFormInputName = "sip_nfyenddate";

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

	[DataType(DataType.Upload)]
	[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
	[BindProperty] 
	public List<IFormFile> ForecastedRevenueFiles { get; set; } = new();
	
	[BindProperty]
	public List<string> ForecastedRevenueFileNames { get; set; }

	[DataType(DataType.Upload)]
	[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
	[BindProperty]
	public List<IFormFile> ForecastedCapitalFiles { get; set; } = new();
	
	[BindProperty]
	public List<string> ForecastedCapitalFileNames { get; set; }
	
	public bool NFYFinancialEndDateError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("NFYFinancialEndDateNotEntered");
		}
	}

	public bool NFYRevenueStatusFileExplainedError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("NFYRevenueStatusExplainedNotEntered");
		}
	}

	public bool NFYCapitalCarryForwardStatusFileExplainedError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("NFYCapitalCarryForwardExplainedNotEntered");
		}
	}

	public bool HasError
	{
		get
		{
			var bools = new[] { NFYFinancialEndDateError,
				NFYRevenueStatusFileExplainedError,
				NFYCapitalCarryForwardStatusFileExplainedError
			};

			return bools.Any(b => b);
		}
	}

	public DateTime NFYFinancialEndDateLocal { get; set; }

	
	private readonly IFileUploadService _fileUploadService;

	public NextFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
		IConversionApplicationCreationService academisationCreationService,
		IFileUploadService fileUploadService)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
			academisationCreationService, "Loans")
	{
		_fileUploadService = fileUploadService;
	}

	public override async Task<ActionResult> OnGetAsync(int urn, int appId)
	{
		var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
		ForecastedRevenueFileNames = await _fileUploadService.GetFiles(
			FileUploadConstants.TopLevelFolderName,
			appId.ToString(), 
			applicationDetails.ApplicationReference,
			FileUploadConstants.NFYForecastedRevenueFilePrefixFieldName);
		
		TempDataHelper.StoreSerialisedValue($"{ApplicationId}-NFYforecastedRevenueFiles", TempData, ForecastedRevenueFileNames);
		
		ForecastedCapitalFileNames = await _fileUploadService.GetFiles(
			FileUploadConstants.TopLevelFolderName,
			appId.ToString(), 
			applicationDetails.ApplicationReference,
			FileUploadConstants.NFYForecastedCapitalFilePrefixFieldName);
		
		TempDataHelper.StoreSerialisedValue($"{ApplicationId}-NFYforecastedCapitalFiles", TempData, ForecastedCapitalFileNames);
		
		return await base.OnGetAsync(urn, appId);
	}

	public override async Task<IActionResult> OnPostAsync()
    {
	    var form = Request.Form;

	    var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
	    var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == Urn);
		// MR:- try and build a date from component parts !!!
		var nfyEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, NFYEndDateFormInputName);
	    string NFYEndDateComponentDay = nfyEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
	    string NFYEndDateComponentMonth = nfyEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
	    string NFYEndDateComponentYear = nfyEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

	    NFYFinancialEndDateLocal = BuildDateTime(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);

	    ForecastedRevenueFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-NFYforecastedRevenueFiles", TempData) ?? new List<string>();
	    ForecastedCapitalFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-NFYforecastedCapitalFiles", TempData) ?? new List<string>();
	    
	    if (!RunUiValidation())
	    {
		    // MR:- date input disappears without below !!
		    RePopDatePickerModel(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);
			return Page();
	    }

		// grab draft application from temp= null
		var draftConversionApplication =
			TempDataHelper.GetSerialisedValue<ConversionApplication>(
				TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		foreach (var file in ForecastedRevenueFiles)
		{
			await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(),
				applicationDetails.ApplicationReference, FileUploadConstants.NFYForecastedRevenueFilePrefixFieldName,
				file);
		}

		foreach (var file in ForecastedCapitalFiles)
		{
			await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(),
				applicationDetails.ApplicationReference, FileUploadConstants.NFYForecastedCapitalFilePrefixFieldName,
				file);
		}
		
		var dictionaryMapper = PopulateUpdateDictionary();
		await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

		// update temp store for next step - application overview
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

	    if (NFYFinancialEndDateLocal == DateTime.MinValue)
	    {
		    ModelState.AddModelError("NFYFinancialEndDateNotEntered", "You must input a valid date");
		    PopulateValidationMessages();
		    return false;
	    }

	    if (NFYRevenueStatus == RevenueType.Deficit && (string.IsNullOrWhiteSpace(NFYRevenueStatusExplained) && !ForecastedRevenueFiles.Any() && !ForecastedRevenueFileNames.Any()))
	    {
		    ModelState.AddModelError("NFYRevenueStatusExplainedNotEntered", "You must provide details or upload a file");
		    PopulateValidationMessages();
		    return false;
	    }

	    if (NFYCapitalCarryForwardStatus == RevenueType.Deficit && (string.IsNullOrWhiteSpace(NFYCapitalCarryForwardExplained) && !ForecastedCapitalFiles.Any() && !ForecastedCapitalFileNames.Any()))
	    {
		    ModelState.AddModelError("NFYCapitalCarryForwardExplainedNotEntered", "You must provide details or upload a file");
		    PopulateValidationMessages();
		    return false;
	    }

	    return true;
    }

    public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string section, string fileName)
    {
	    var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
	    await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelFolderName, appId.ToString(), applicationDetails.ApplicationReference, section, fileName);
	    return RedirectToPage("NextFinancialYear", new {Urn = urn, AppId = appId});
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

    ///<inheritdoc/>
	public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
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

