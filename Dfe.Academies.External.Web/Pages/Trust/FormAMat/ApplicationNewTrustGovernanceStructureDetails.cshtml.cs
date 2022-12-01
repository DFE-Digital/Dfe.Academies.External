using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	public class ApplicationNewTrustGovernanceStructureDetails : BaseTrustFamApplicationPageEditModel
	{
		public int Urn { get; private set; }
		public string TrustName { get; private set; } = string.Empty;
		
		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		[BindProperty]
		public List<IFormFile> GovernanceStructureDetailsFiles { get; private set; } = new();
		[BindProperty]
		public List<string> GovernanceStructureDetailsFileNames { get; private set; } = new();

		private readonly IFileUploadService _fileUploadService;
		
		public ApplicationNewTrustGovernanceStructureDetails(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService, IFileUploadService fileUploadService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "applicationnewtrustsummary")
		{
			_fileUploadService = fileUploadService;
		}
		
		public bool GovernanceStructureDetailsFileError => !ModelState.IsValid && ModelState.Keys.Contains("GovernanceStructureDetailFileNotAddedError");

		public bool HasError
		{
			get
			{
				var bools = new[] {GovernanceStructureDetailsFileError};

				return bools.Any(b => b);
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		
		public override async Task<ActionResult> OnGetAsync(int appId)
		{
			LoadAndStoreCachedConversionApplication();

			ApplicationId = appId;

			// Grab other values from API
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
			TrustName = applicationDetails?.TrustName ?? string.Empty;

			GovernanceStructureDetailsFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{appId}-governanceStructureDetailsFiles", TempData, GovernanceStructureDetailsFileNames);
			return Page();
		}
		
		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			
			GovernanceStructureDetailsFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-governanceStructureDetailsFiles", TempData) ?? new List<string>();
			
			if (!RunUiValidation())
			{
				return Page();
			}

			foreach (var file in GovernanceStructureDetailsFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName, file);
			}
			
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			
			return RedirectToPage(NextStepPage, new { appId = ApplicationId});
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			throw new NotImplementedException();
		}


		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			if ((!GovernanceStructureDetailsFiles.Any()) &&
			    (!GovernanceStructureDetailsFileNames.Any()))
			{
				ModelState.AddModelError("GovernanceStructureDetailFileNotAddedError", "You must upload a file");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, string section, string fileName)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{appId}", section, fileName);
			return RedirectToPage("ApplicationNewTrustGovernanceStructureDetails", new {AppId = appId});
		}
	}
}
