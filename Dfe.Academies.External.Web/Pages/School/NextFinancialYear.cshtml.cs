using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Exceptions;

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
	[RequiredEnum(ErrorMessage = "You must select an revenue carry forward option")]
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
	[RequiredEnum(ErrorMessage = "You must select a capital carry forward option")]
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
	
	[BindProperty]
	public Guid EntityId { get; set; }
	
	[BindProperty]
	public string ApplicationReference { get; set; }
	
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

	public bool ForecastedRevenueFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("ForecastedRevenueFileSizeError");
	public bool ForecastedCapitalFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("ForecastedCapitalFileSizeError");

	public bool SchoolCapitalFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(SchoolCapitalFileGenericError));
	public bool SchoolRevenueFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(SchoolRevenueFileGenericError));
	public DateTime NFYFinancialEndDateLocal { get; set; }

	
	private readonly IFileUploadService _fileUploadService;

	public NextFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
		IConversionApplicationService academisationCreationService,
		IFileUploadService fileUploadService)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
			academisationCreationService, "Loans")
	{
		_fileUploadService = fileUploadService;
	}

	public override async Task<ActionResult> OnGetAsync(int urn, int appId)
	{

		LoadAndStoreCachedConversionApplication();
		
		ApplicationId = appId;
		Urn = urn;
		
		// Grab other values from API
		var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
		var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == urn);
		ApplicationType = applicationDetails.ApplicationType;
		if (selectedSchool != null)
		{
			EntityId = selectedSchool.EntityId;
			PopulateUiModel(selectedSchool);
		}
		ApplicationReference = applicationDetails.ApplicationReference;
		
		ForecastedRevenueFileNames = await _fileUploadService.GetFiles(
			FileUploadConstants.TopLevelSchoolFolderName,
			EntityId.ToString(), 
			ApplicationReference,
			FileUploadConstants.NFYForecastedRevenueFilePrefixFieldName);
		
		TempDataHelper.StoreSerialisedValue($"{EntityId}-NFYforecastedRevenueFiles", TempData, ForecastedRevenueFileNames);
		
		ForecastedCapitalFileNames = await _fileUploadService.GetFiles(
			FileUploadConstants.TopLevelSchoolFolderName,
			EntityId.ToString(), 
			ApplicationReference,
			FileUploadConstants.NFYForecastedCapitalFilePrefixFieldName);
		
		TempDataHelper.StoreSerialisedValue($"{EntityId}-NFYforecastedCapitalFiles", TempData, ForecastedCapitalFileNames);
		return Page();
	}
	
	private async Task<bool> UploadFiles()
	{
		try
		{
			foreach (var file in ForecastedRevenueFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
					ApplicationReference, FileUploadConstants.NFYForecastedRevenueFilePrefixFieldName,
					file);
			}
		}
		catch (FileUploadException)
		{
			ModelState.AddModelError(nameof(SchoolRevenueFileGenericError), "The selected file could not be uploaded – try again");
			PopulateValidationMessages();
			return false;
		}

		try
		{
			foreach (var file in ForecastedCapitalFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
					ApplicationReference, FileUploadConstants.NFYForecastedCapitalFilePrefixFieldName, file);
			}
		}
		catch (FileUploadException)
		{
			ModelState.AddModelError(nameof(SchoolCapitalFileGenericError), "The selected file could not be uploaded – try again");
			PopulateValidationMessages();
			return false;
		}

		return true;
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

	    ForecastedRevenueFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{selectedSchool.EntityId}-NFYforecastedRevenueFiles", TempData) ?? new List<string>();
	    ForecastedCapitalFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{selectedSchool.EntityId}-NFYforecastedCapitalFiles", TempData) ?? new List<string>();
	    
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

		if (!(await UploadFiles()))
		{
			RePopDatePickerModel(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);
			return Page();
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

	    foreach (var file in ForecastedRevenueFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
	    {
		    ModelState.AddModelError("ForecastedRevenueFileSizeError", $"File: {file.FileName} is too large");
		    PopulateValidationMessages();
		    return false;
	    }
	    
	    foreach (var file in ForecastedCapitalFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
	    {
		   ModelState.AddModelError("ForecastedCapitalFileSizeError", $"File: {file.FileName} is too large");
		   PopulateValidationMessages();
		   return false;
	    }
	    
	    return true;
    }

    public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
    {
	    await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelSchoolFolderName, entityId, applicationReference, section, fileName);
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
		
		Revenue = selectedSchool.NextFinancialYear.Revenue.GetValueOrDefault();
		NFYRevenueStatus = selectedSchool.NextFinancialYear.RevenueStatus.GetValueOrDefault();
		NFYRevenueStatusExplained = selectedSchool.NextFinancialYear.RevenueStatusExplained; 
		CapitalCarryForward = selectedSchool.NextFinancialYear.CapitalCarryForward.GetValueOrDefault();
		NFYCapitalCarryForwardStatus = selectedSchool.NextFinancialYear.CapitalCarryForwardStatus.GetValueOrDefault();
		NFYCapitalCarryForwardExplained = selectedSchool.NextFinancialYear.CapitalCarryForwardExplained;
		EntityId = selectedSchool.EntityId;
	}

	private void RePopDatePickerModel(string nfyEndDateComponentDay, string nfyEndDateComponentMonth, string nfyEndDateComponentYear)
	{
		NFYEndDateDay = nfyEndDateComponentDay;
		NFYEndDateMonth = nfyEndDateComponentMonth;
		NFYEndDateDateYear = nfyEndDateComponentYear;
	}
}

