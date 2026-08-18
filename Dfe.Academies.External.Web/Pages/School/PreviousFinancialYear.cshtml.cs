using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class PreviousFinancialYearModel : BaseSchoolPageEditModel
	{
		private readonly ISharePointService _sharepoint;
		private readonly ILogger<PreviousFinancialYearModel> _logger;

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
		public List<IFormFile>? SchoolPFYRevenueStatusFiles { get; set; } = [];

		[BindProperty]
		public List<string> SchoolPFYRevenueStatusFileNames { get; set; } = [];

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? SchoolPFYCapitalForwardStatusFiles { get; set; } = [];

		[BindProperty]
		public List<string> SchoolPFYCapitalForwardStatusFileNames { get; set; } = [];

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

		public PreviousFinancialYearModel(
			ISharePointService sharepointService,
			ILogger<PreviousFinancialYearModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService
		) : base(
			conversionApplicationRetrievalService,
			referenceDataRetrievalService,
			academisationCreationService, "CurrentFinancialYear"
		)
		{
			_sharepoint = sharepointService;
			_logger = logger;
		}

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
		{
			string folder = FileUploadConstants.FormatSharepointSchoolDirectory(applicationReference, entityId);
			await _sharepoint.DeleteFileAsync(folder, fileName);

			return RedirectToPage("PreviousFinancialYear", new { Urn = urn, AppId = appId });
		}

		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			ApplicationId = appId;
			Urn = urn;

			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
			var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == urn);
			ApplicationReference = applicationDetails?.ApplicationReference;

			if (selectedSchool != null)
			{
				EntityId = selectedSchool.EntityId;
				PopulateUiModel(selectedSchool);
			}

			// Force source-of-truth refresh on GET
			await InitialiseFileNameCollectionsAsync(forceRefreshFromSource: true);

			return Page();
		}

		private async Task<bool> UploadFiles()
		{
			string folder = FileUploadConstants.FormatSharepointSchoolDirectory(ApplicationReference, EntityId.ToString());
			try
			{
				foreach (var file in SchoolPFYRevenueStatusFiles)
				{
					string fileName = $"{FileUploadConstants.SchoolPFYRevenueStatusFile}_{file.FileName}";
					await _sharepoint.UploadFileAsync(folder, fileName, file.OpenReadStream());
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
					string fileName = $"{FileUploadConstants.SchoolPFYCapitalForwardStatusFile}_{file.FileName}";
					await _sharepoint.UploadFileAsync(folder, fileName, file.OpenReadStream());
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

			PFYFinancialEndDateLocal = BuildDateTime(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);

			await InitialiseFileNameCollectionsAsync();

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

		private string RevenueFilesTempDataKey => $"{EntityId}-SchoolPFYRevenueStatusFileNames";
		private string CapitalFilesTempDataKey => $"{EntityId}-SchoolPFYCapitalForwardStatusFileNames";

		private async Task InitialiseFileNameCollectionsAsync(bool forceRefreshFromSource = false)
		{
			SchoolPFYRevenueStatusFileNames ??= [];
			SchoolPFYCapitalForwardStatusFileNames ??= [];

			if (!forceRefreshFromSource)
			{
				SchoolPFYRevenueStatusFileNames =
					TempDataHelper.GetSerialisedValue<List<string>>(RevenueFilesTempDataKey, TempData) ?? [];

				SchoolPFYCapitalForwardStatusFileNames =
					TempDataHelper.GetSerialisedValue<List<string>>(CapitalFilesTempDataKey, TempData) ?? [];
			}

			// Always hydrate from source when forced, otherwise only when temp is empty
			if (forceRefreshFromSource ||
			    (!SchoolPFYRevenueStatusFileNames.Any() && !SchoolPFYCapitalForwardStatusFileNames.Any()))
			{
				try
				{
					string folder = FileUploadConstants.FormatSharepointSchoolDirectory(ApplicationReference, EntityId.ToString());
					var files = await _sharepoint.ListFilesAsync(folder);

					SchoolPFYRevenueStatusFileNames = files
						.Where(file => file.Name.StartsWith(FileUploadConstants.SchoolPFYRevenueStatusFile))
						.Select(file => file.Name)
						.ToList();

					SchoolPFYCapitalForwardStatusFileNames = files
						.Where(file => file.Name.StartsWith(FileUploadConstants.SchoolPFYCapitalForwardStatusFile))
						.Select(file => file.Name)
						.ToList();
				}
				catch
				{
					// If folder/files are gone, explicitly clear collections
					SchoolPFYRevenueStatusFileNames = [];
					SchoolPFYCapitalForwardStatusFileNames = [];

					_logger.LogInformation("No School directory exists yet for application: {ApplicationReference} :: {FolderSuffix}",
						ApplicationReference,
						$"{ApplicationReference}_{EntityId}");
				}
			}

			// Overwrite tempdata with current source state
			TempDataHelper.StoreSerialisedValue(RevenueFilesTempDataKey, TempData, SchoolPFYRevenueStatusFileNames);
			TempDataHelper.StoreSerialisedValue(CapitalFilesTempDataKey, TempData, SchoolPFYCapitalForwardStatusFileNames);
		}
	}
}
