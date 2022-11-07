using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
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
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public RevenueType PFYRevenueStatus { get; set; }

		[BindProperty]
		public string? PFYRevenueStatusExplained { get; set; }

		public List<IFormFile>? SchoolPFYRevenueStatusFiles { get; set; } = new();

		[BindProperty]
		public List<string> SchoolPFYRevenueStatusFileNames { get; set; }

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
		[RequiredEnum(ErrorMessage = "You must select an option")]
		public RevenueType PFYCapitalCarryForwardStatus { get; set; }

		[BindProperty]
		public string? PFYCapitalCarryForwardExplained { get; set; }

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

		public DateTime PFYFinancialEndDateLocal { get; set; }

		public PreviousFinancialYearModel(IFileUploadService fileUploadService,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "CurrentFinancialYear")
		{
			_fileUploadService = fileUploadService;
		}

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string section, string fileName, string fileUploadConstant)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{appId}", section, fileName);
			return RedirectToPage("PreviousFinancialYear", new { Urn = urn, AppId = appId });
		}

		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			SchoolPFYRevenueStatusFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{appId}", FileUploadConstants.SchoolPFYRevenueStatusFile);
			SchoolPFYCapitalForwardStatusFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{appId}", FileUploadConstants.SchoolPFYCapitalForwardStatusFile);

			TempDataHelper.StoreSerialisedValue($"{appId}-SchoolPFYRevenueStatusFileNames", TempData, SchoolPFYRevenueStatusFileNames);
			TempDataHelper.StoreSerialisedValue($"{appId}-SchoolPFYRevenueStatusFileNames", TempData, SchoolPFYRevenueStatusFileNames);

			return await base.OnGetAsync(urn, appId);
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;

			// MR:- try and build a date from component parts !!!
			var pfyEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PFYEndDateFormInputName);
			string PFYEndDateComponentDay = pfyEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string PFYEndDateComponentMonth = pfyEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string PFYEndDateComponentYear = pfyEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			SchoolPFYRevenueStatusFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-SchoolPFYRevenueStatusFileNames", TempData) ?? new List<string>();
			SchoolPFYCapitalForwardStatusFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-SchoolPFYCapitalForwardStatusFileNames", TempData) ?? new List<string>();

			PFYFinancialEndDateLocal = BuildDateTime(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);

			if (!RunUiValidation())
			{
				// PL:- had to put these back into tempdata or existing file names are removed after not valid scenarios
				TempDataHelper.StoreSerialisedValue($"{ApplicationId}-SchoolPFYRevenueStatusFileNames", TempData, SchoolPFYRevenueStatusFileNames);
				TempDataHelper.StoreSerialisedValue($"{ApplicationId}-SchoolPFYRevenueStatusFileNames", TempData, SchoolPFYRevenueStatusFileNames);
				// MR:- date input disappears without below !!
				RePopDatePickerModel(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);
				return Page();
			}
			
			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();



			SchoolPFYRevenueStatusFiles?.ForEach(async file =>
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), draftConversionApplication.ApplicationReference, FileUploadConstants.SchoolPFYRevenueStatusFile, file);
			});

			SchoolPFYCapitalForwardStatusFiles?.ForEach(async file =>
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), draftConversionApplication.ApplicationReference, FileUploadConstants.SchoolPFYCapitalForwardStatusFile, file);
			});

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
