using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
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
			IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService, 
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

			return true;
		}
		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string section, string fileName)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelFolderName, EntityId.ToString(), ApplicationReference, section, fileName);
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
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
			SelectedTrustName = applicationDetails.JoinTrustDetails?.TrustName ?? string.Empty;
			
			var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == urn);

			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}

			EntityId = applicationDetails.EntityId;
			ApplicationReference = applicationDetails.ApplicationReference;
			TrustConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, applicationDetails.EntityId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{appId}-trustConsentFiles", TempData, TrustConsentFileNames);
			return Page();
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			
			TrustConsentFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-trustConsentFiles", TempData) ?? new List<string>();
			
			if (!RunUiValidation())
			{
				return Page();
			}

			foreach (var file in TrustConsentFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, applicationDetails.EntityId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName, file);
			}
			
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			
			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
		}
	}
}
