using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class CurrentFinancialYearModel : BaseSchoolPageEditModel
{
	private readonly IFileUploadService _fileUploadService;
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
	[Required(ErrorMessage = "You must provide a revenue carry forward amount")]
	public decimal? Revenue { get; set; }

	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must select a revenue carry forward option")]
	public RevenueType CFYRevenueStatus { get; set; }

	[BindProperty]
	public string? CFYRevenueStatusExplained { get; set; }
	
	[BindProperty]
	[Range(0, 200000000000000, ErrorMessage = "Capital carry forward amount must be greater than 0")]
	[Required(ErrorMessage = "You must provide a capital carry forward amount")]
	public decimal? CapitalCarryForward { get; set; }

	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must select a capital carry forward option")]
	public RevenueType CFYCapitalCarryForwardStatus { get; set; }

	[BindProperty]
	public string? CFYCapitalCarryForwardExplained { get; set; }

	[BindProperty]
	public string? PFYRevenueStatusExplained { get; set; }

	[DataType(DataType.Upload)]
	[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
	public List<IFormFile>? SchoolCfyRevenueStatusFiles { get; set; } = new();

	[BindProperty]
	public List<string> SchoolCFYRevenueStatusFileNames { get; set; }

	[DataType(DataType.Upload)]
	[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
	public List<IFormFile>? SchoolCFYCapitalForwardFiles { get; set; } = new();

	[BindProperty]
	public List<string> SchoolCFYCapitalForwardFileNames { get; set; }

	[BindProperty]
	public Guid EntityId { get; set; }
	
	[BindProperty]
	public string ApplicationReference { get; set; }
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
			return !ModelState.IsValid && ModelState.Keys.Contains("PFYCapitalCarryForwardExplainedNotEntered");
		}
	}
	
	public bool SchoolCFYRevenueFileSizeError =>  ModelState.ContainsKey(nameof(SchoolCFYRevenueFileSizeError));

	public bool SchoolCFYRevenueFileGenericError => ModelState.ContainsKey(nameof(SchoolCFYRevenueFileGenericError));
	public bool SchoolCFYCapitalFileGenericError => ModelState.ContainsKey(nameof(SchoolCFYCapitalFileGenericError));
	public bool SchoolCFYCapitalFileSizeError => ModelState.ContainsKey(nameof(SchoolCFYCapitalFileSizeError));
	
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

	public CurrentFinancialYearModel(IFileUploadService fileUploadService, IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
									IReferenceDataRetrievalService referenceDataRetrievalService,
									IConversionApplicationService academisationCreationService) 
        : base(conversionApplicationRetrievalService, referenceDataRetrievalService,
	        academisationCreationService, "NextFinancialYear")
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

		if (selectedSchool != null)
		{
			EntityId = selectedSchool.EntityId;
			PopulateUiModel(selectedSchool);
		}
		ApplicationType = applicationDetails.ApplicationType;
		ApplicationReference = applicationDetails.ApplicationReference;
		SchoolCFYRevenueStatusFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.SchoolCFYRevenueStatusFile);
		SchoolCFYCapitalForwardFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.SchoolCFYCapitalForwardFile);

		TempDataHelper.StoreSerialisedValue($"{EntityId}-SchoolCFYRevenueStatusFileNames", TempData, SchoolCFYRevenueStatusFileNames);
		TempDataHelper.StoreSerialisedValue($"{EntityId}-SchoolCFYCapitalForwardFileNames", TempData, SchoolCFYCapitalForwardFileNames);

		return Page();
	}

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

		SchoolCFYRevenueStatusFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-SchoolCFYRevenueStatusFileNames", TempData) ?? new List<string>();
		SchoolCFYCapitalForwardFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-SchoolCFYCapitalForwardFileNames", TempData) ?? new List<string>();

		if (!RunUiValidation())
		{
			// PL:- had to put these back into tempdata or existing file names are removed after not valid scenarios
			TempDataHelper.StoreSerialisedValue($"{EntityId}-SchoolCFYRevenueStatusFileNames", TempData, SchoolCFYRevenueStatusFileNames);
			TempDataHelper.StoreSerialisedValue($"{EntityId}-SchoolCFYCapitalForwardFileNames", TempData, SchoolCFYCapitalForwardFileNames);

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

		if (!(await UploadFiles()))
		{
			RePopDatePickerModel(CFYEndDateComponentDay, CFYEndDateComponentMonth, CFYEndDateComponentYear);
			return Page();
		}
		
		
		// update temp store for next step
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

		return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
	}

	private async Task<bool> UploadFiles()
	{
		try
		{
			foreach (var file in SchoolCfyRevenueStatusFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
					ApplicationReference, FileUploadConstants.SchoolCFYRevenueStatusFile,
					file);
			}
		}
		catch (FileUploadException)
		{
			ModelState.AddModelError(nameof(SchoolCFYRevenueFileGenericError), "The selected file could not be uploaded – try again");
			PopulateValidationMessages();
			return false;
		}

		try
		{
			foreach (var file in SchoolCFYCapitalForwardFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
					ApplicationReference, FileUploadConstants.SchoolCFYCapitalForwardFile, file);
			}
		}
		catch (FileUploadException)
		{
			ModelState.AddModelError(nameof(SchoolCFYCapitalFileGenericError), "The selected file could not be uploaded – try again");
			PopulateValidationMessages();
			return false;
		}

		return true;
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

		if (CFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(CFYRevenueStatusExplained) && (SchoolCfyRevenueStatusFiles == null || !SchoolCfyRevenueStatusFiles.Any()) && !SchoolCFYRevenueStatusFileNames.Any())
		{
			ModelState.AddModelError("CFYRevenueStatusExplainedNotEntered", "You must provide details or upload a file");
			PopulateValidationMessages();
			return false;
		}

		if (CFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(CFYCapitalCarryForwardExplained) && (SchoolCFYCapitalForwardFiles == null || !SchoolCFYCapitalForwardFiles.Any()) && !SchoolCFYCapitalForwardFileNames.Any())
		{
			ModelState.AddModelError("PFYCapitalCarryForwardExplainedNotEntered", "You must provide details or upload a file");
			PopulateValidationMessages();
			return false;
		}
		
		if (SchoolCfyRevenueStatusFiles != null)
		{
			foreach (var file in SchoolCfyRevenueStatusFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
			{
				ModelState.AddModelError(nameof(SchoolCFYRevenueFileSizeError), $"File: {file.FileName} is too large");
				PopulateValidationMessages();
				return false;
			}
		}

		if (SchoolCFYCapitalForwardFiles != null)
		{
			foreach (var file in SchoolCFYCapitalForwardFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
			{
				ModelState.AddModelError(nameof(SchoolCFYCapitalFileSizeError), $"File: {file.FileName} is too large");
				PopulateValidationMessages();
				return false;
			}
		}

		return true;
	}
	public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
	{
		await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelSchoolFolderName, entityId, applicationReference, section, fileName);
		return RedirectToPage("CurrentFinancialYear", new { Urn = urn, AppId = appId });
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
		var applicationDetails = ConversionApplicationRetrievalService.GetApplication(ApplicationId).Result;
		
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

