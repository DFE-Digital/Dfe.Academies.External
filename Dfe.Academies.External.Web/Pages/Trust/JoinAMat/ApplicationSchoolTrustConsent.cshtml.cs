using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.JoinAMat
{
	public class ApplicationSchoolTrustConsent : BaseSchoolPageEditModel
	{
		private readonly IFileUploadService _fileUploadService;
		
		public ApplicationSchoolTrustConsent(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationService conversionApplicationCreationService, 
			IFileUploadService fileUploadService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "ApplicationSchoolChangesToATrust")
		{
			_fileUploadService = fileUploadService;
		}
		public ApplicationTypes ApplicationType { get; private set; }

		public string SelectedTrustName { get; private set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		[BindProperty]
		public List<IFormFile> TrustConsentFiles { get; set; } = new();

		[BindProperty] 
		public List<string> TrustConsentFileNames { get; set; } = new();
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
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelApplicationFolderName, entityId, applicationReference, section, fileName);
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
			EntityId = applicationDetails.EntityId;
			ApplicationReference = applicationDetails.ApplicationReference;
			SelectedTrustName = applicationDetails.JoinTrustDetails?.TrustName ?? string.Empty;
			
			var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == urn);

			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}

			EntityId = applicationDetails.EntityId;
			ApplicationReference = applicationDetails.ApplicationReference;
			TrustConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelApplicationFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName);
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
			try
			{
				foreach (var file in TrustConsentFiles)
				{
					await _fileUploadService.UploadFile(FileUploadConstants.TopLevelApplicationFolderName,
						EntityId.ToString(), ApplicationReference,
						FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName, file);
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(TrustConsentFileGenericError), "The selected file could not be uploaded – try again");
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
