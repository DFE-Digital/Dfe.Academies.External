using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.JoinAMat
{
	public class ApplicationSchoolTrustConsent : BaseSchoolPageEditModel
	{
		private readonly ISharePointService _sharepoint;
		private readonly ILogger<ApplicationSchoolTrustConsent> _logger;
		
		public ApplicationSchoolTrustConsent(
			IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService conversionApplicationCreationService, 
			ISharePointService sharepointService,
			ILogger<ApplicationSchoolTrustConsent> logger
		) : 
		base(conversionApplicationRetrievalService, referenceDataRetrievalService,conversionApplicationCreationService, 
			"ApplicationSchoolChangesToATrust"){
			_sharepoint = sharepointService;
			_logger = logger;
		}
		
		public ApplicationTypes ApplicationType { get; private set; }

		public string SelectedTrustName { get; private set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions([".doc", ".docx", ".ppt", ".pptx", ".pdf"])]
		[BindProperty]
		public List<IFormFile> TrustConsentFiles { get; set; } = [];

		[BindProperty] 
		public List<string> TrustConsentFileNames { get; set; } = [];
		public bool TrustConsentFileError => !ModelState.IsValid && ModelState.Keys.Contains("TrustConsentFileNotAddedError");
		
		[BindProperty]
		public Guid EntityId { get; set; }
		
		[BindProperty]
		public string ApplicationReference { get; set; }
		
		public bool TrustConsentFileSizeError => !ModelState.IsValid && ModelState.ContainsKey(nameof(TrustConsentFileSizeError));
		public bool TrustConsentFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(TrustConsentFileGenericError));
		
		public bool HasError
		{
			get
			{
				var bools = new[] {TrustConsentFileError};
				return bools.Any(b => b);
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			if ((!TrustConsentFiles.Any()) &&
			    (!TrustConsentFileNames.Any()))
			{
				ModelState.AddModelError("TrustConsentFileNotAddedError", "You must upload a file");
				PopulateValidationMessages();
				return false;
			}

			foreach (var file in TrustConsentFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
			{
				ModelState.AddModelError(nameof(TrustConsentFileSizeError), $"File: {file.FileName} is too large");
				PopulateValidationMessages();
				return false;
			}
			return true;
		}
		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
		{
			string folder = FileUploadConstants.FormatSharepointApplicationDirectory(applicationReference, entityId);
			await _sharepoint.DeleteFileAsync(folder, fileName);
			
			return RedirectToPage("ApplicationSchoolTrustConsent", new {Urn = urn, AppId = appId});
		}
		
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new();
		}

		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			ApplicationReference = applicationDetails.ApplicationReference;
			EntityId = applicationDetails.EntityId;
			SelectedTrustName = applicationDetails.JoinTrustDetails?.TrustName ?? string.Empty;
			
			var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == urn);

			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}

			string folder = FileUploadConstants.FormatSharepointApplicationDirectory(ApplicationReference, EntityId.ToString());
			try
			{
				var files = await _sharepoint.ListFilesAsync(folder);
				TrustConsentFileNames =
					files.Where(file =>
							file.Name.StartsWith(FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName))
						.Select(file => file.Name).ToList();
			}
			catch
			{
				_logger.LogInformation("No Trust consent file(s) directory exists yet for application: {1} :: {2}",
					ApplicationReference, $"{ApplicationReference}_{EntityId}");
			}

			TempDataHelper.StoreSerialisedValue($"{EntityId}-trustConsentFiles", TempData, TrustConsentFileNames);
			
			return Page();
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			
			TrustConsentFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-trustConsentFiles", TempData) ?? new List<string>();
			
			if (!RunUiValidation())
			{
				return Page();
			}


			if (!(await UploadFiles()))
			{
				return Page();
			}
			
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			
			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}

		private async Task<bool> UploadFiles()
		{
			string folder = FileUploadConstants.FormatSharepointApplicationDirectory(ApplicationReference, EntityId.ToString());
			try
			{
				foreach (var file in TrustConsentFiles)
				{
					string fileName = $"{FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName}_{file.FileName}";
					await _sharepoint.UploadFileAsync(folder, fileName, file.OpenReadStream());
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(TrustConsentFileGenericError), "The selected file(s) could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}
		
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
		}
	}
}
