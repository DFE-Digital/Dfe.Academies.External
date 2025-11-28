using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class PreviousFinancialYearModel : BaseSchoolPageEditModel
	{
		private readonly IFileUploadService _fileUploadService;

		public string PFYEndDateFormInputName = "sip_pfyenddate";

		// MR:- VM props to capture Pfy data
		
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
		[RequiredEnum(ErrorMessage = "You must select a revenue carry forward option")]
		public RevenueType PFYRevenueStatus { get; set; }

		[BindProperty]
		public string? PFYRevenueStatusExplained { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? SchoolPFYRevenueStatusFiles { get; set; } = new();

		[BindProperty]
		public List<string> SchoolPFYRevenueStatusFileNames { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? SchoolPFYCapitalForwardStatusFiles { get; set; } = new();

		[BindProperty]
		public List<string> SchoolPFYCapitalForwardStatusFileNames { get; set; }

		// TODO MR:- below, once file upload whoopsy sorted!
		//string? RevenueStatusFileLink = null,

		[BindProperty]
		[Range(0, 200000000000000, ErrorMessage = "Capital carry forward amount must be greater than 0")]
		[Required(ErrorMessage = "You must provide a capital carry forward amount")]
		public decimal CapitalCarryForward { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must select a capital carry forward option")]
		public RevenueType PFYCapitalCarryForwardStatus { get; set; }

		[BindProperty]
		public string? PFYCapitalCarryForwardExplained { get; set; }

		[BindProperty]
		public Guid EntityId { get; set; }
		
		[BindProperty]
		public string ApplicationReference { get; set; }
		
		public bool PFYFinancialEndDateError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("PFYFinancialEndDateNotEntered");
			}
		}

		public bool PFYRevenueStatusExplainedError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("PFYRevenueStatusExplainedNotEntered");
			}
		}

		public bool PFYCapitalCarryForwardStatusExplainedError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("PFYCapitalCarryForwardExplainedNotEntered");
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

		public bool SchoolPFYRevenueFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("SchoolPFYRevenueFileSizeError");
		public bool SchoolPFYCapitalFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("SchoolPFYCapitalFileSizeError");

		public bool SchoolPFYRevenueFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(SchoolPFYRevenueFileGenericError));
		public bool SchoolPFYCapitalFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(SchoolPFYCapitalFileGenericError));
		
		public DateTime PFYFinancialEndDateLocal { get; set; }

		public PreviousFinancialYearModel(IFileUploadService fileUploadService,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "CurrentFinancialYear")
		{
			_fileUploadService = fileUploadService;
		}

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelSchoolFolderName, entityId, applicationReference, section, fileName);
			return RedirectToPage("PreviousFinancialYear", new { Urn = urn, AppId = appId });
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
			ApplicationReference = applicationDetails?.ApplicationReference;
			SchoolPFYRevenueStatusFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.SchoolPFYRevenueStatusFile);
			SchoolPFYCapitalForwardStatusFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.SchoolPFYCapitalForwardStatusFile);

			TempDataHelper.StoreSerialisedValue($"{appId}-SchoolPFYRevenueStatusFileNames", TempData, SchoolPFYRevenueStatusFileNames);
			TempDataHelper.StoreSerialisedValue($"{appId}-SchoolPFYCapitalForwardStatusFileNames", TempData, SchoolPFYCapitalForwardStatusFileNames);

			return Page();
		}
		
		private async Task<bool> UploadFiles()
		{
			try
			{
				foreach (var file in SchoolPFYRevenueStatusFiles)
				{
					await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
						ApplicationReference, FileUploadConstants.SchoolPFYRevenueStatusFile,
						file);
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(SchoolPFYRevenueFileGenericError), "The selected file could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			try
			{
				foreach (var file in SchoolPFYCapitalForwardStatusFiles)
				{
					await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
						ApplicationReference, FileUploadConstants.SchoolPFYCapitalForwardStatusFile, file);
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(SchoolPFYCapitalFileGenericError), "The selected file could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;

			// MR:- try and build a date from component parts !!!
			var pfyEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PFYEndDateFormInputName);
			string PFYEndDateComponentDay = pfyEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string PFYEndDateComponentMonth = pfyEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string PFYEndDateComponentYear = pfyEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			SchoolPFYRevenueStatusFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-SchoolPFYRevenueStatusFileNames", TempData) ?? new List<string>();
			SchoolPFYCapitalForwardStatusFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-SchoolPFYCapitalForwardStatusFileNames", TempData) ?? new List<string>();

			PFYFinancialEndDateLocal = BuildDateTime(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);

			if (!RunUiValidation())
			{
				// PL:- had to put these back into tempdata or existing file names are removed after not valid scenarios
				TempDataHelper.StoreSerialisedValue($"{EntityId}-SchoolPFYRevenueStatusFileNames", TempData, SchoolPFYRevenueStatusFileNames);
				TempDataHelper.StoreSerialisedValue($"{EntityId}-SchoolPFYCapitalForwardStatusFileNames", TempData, SchoolPFYCapitalForwardStatusFileNames);
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}
			
			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			
			if (!(await UploadFiles()))
			{
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
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

			if (PFYFinancialEndDateLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("PFYFinancialEndDateNotEntered", "You must input a valid date");
				PopulateValidationMessages();
				return false;
			}

			if (PFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYRevenueStatusExplained) && (SchoolPFYRevenueStatusFiles == null || !SchoolPFYRevenueStatusFiles.Any()) && !SchoolPFYRevenueStatusFileNames.Any())
			{
				ModelState.AddModelError("PFYRevenueStatusExplainedNotEntered", "You must provide details or upload a file");
				PopulateValidationMessages();
				return false;
			}

			if (PFYCapitalCarryForwardStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYCapitalCarryForwardExplained) && (SchoolPFYCapitalForwardStatusFiles == null || !SchoolPFYCapitalForwardStatusFiles.Any()) && !SchoolPFYCapitalForwardStatusFileNames.Any())
			{
				ModelState.AddModelError("PFYCapitalCarryForwardExplainedNotEntered", "You must provide details or upload a file");
				PopulateValidationMessages();
				return false;
			}

			if (SchoolPFYRevenueStatusFiles != null)
			{
				foreach (var file in SchoolPFYRevenueStatusFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
				{
					ModelState.AddModelError(nameof(SchoolPFYRevenueFileSizeError), $"File: {file.FileName} is too large");
					PopulateValidationMessages();
					return false;
				}
			}

			if (SchoolPFYCapitalForwardStatusFiles != null)
			{
				foreach (var file in SchoolPFYCapitalForwardStatusFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
				{
					ModelState.AddModelError(nameof(SchoolPFYCapitalFileSizeError), $"File: {file.FileName} is too large");
					PopulateValidationMessages();
					return false;
				}
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

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			PFYEndDate = selectedSchool.PreviousFinancialYear.FinancialYearEndDate.HasValue ?
				selectedSchool.PreviousFinancialYear.FinancialYearEndDate.Value.ToString("dd/MM/yyyy")
				: string.Empty;

			Revenue = selectedSchool.PreviousFinancialYear.Revenue.GetValueOrDefault();
			PFYRevenueStatus = selectedSchool.PreviousFinancialYear.RevenueStatus.GetValueOrDefault();
			PFYRevenueStatusExplained = selectedSchool.PreviousFinancialYear.RevenueStatusExplained; 
			CapitalCarryForward = selectedSchool.PreviousFinancialYear.CapitalCarryForward.GetValueOrDefault();
			PFYCapitalCarryForwardStatus = selectedSchool.PreviousFinancialYear.CapitalCarryForwardStatus.GetValueOrDefault();
			PFYCapitalCarryForwardExplained = selectedSchool.PreviousFinancialYear.CapitalCarryForwardExplained;
			EntityId = selectedSchool.EntityId;
		}

		private void RePopDatePickerModel(string pfyEndDateComponentDay, string pfyEndDateComponentMonth, string pfyEndDateComponentYear)
		{
			PFYEndDateDay = pfyEndDateComponentDay;
			PFYEndDateMonth = pfyEndDateComponentMonth;
			PFYEndDateDateYear = pfyEndDateComponentYear;
		}
	}
}
