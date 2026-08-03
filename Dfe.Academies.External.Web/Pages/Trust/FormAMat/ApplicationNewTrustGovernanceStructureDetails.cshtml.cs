using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	public class ApplicationNewTrustGovernanceStructureDetails : BaseTrustFamApplicationPageEditModel
	{
		private readonly ISharePointService _sharepoint;
		private readonly ILogger<ApplicationNewTrustGovernanceStructureDetails> _logger;
		
		public int Urn { get; private set; }
		public string TrustName { get; private set; } = string.Empty;
		
		[DataType(DataType.Upload)]
		[AllowedExtensions([".doc", ".docx", ".ppt", ".pptx", ".pdf"])]
		[BindProperty]
		public List<IFormFile> GovernanceStructureDetailsFiles { get; private set; } = new();
		[BindProperty]
		public List<string> GovernanceStructureDetailsFileNames { get; private set; } = new();
		
		public ApplicationNewTrustGovernanceStructureDetails(
			IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService, 
			IConversionApplicationService conversionApplicationCreationService, 
			ISharePointService sharepointService,
			ILogger<ApplicationNewTrustGovernanceStructureDetails> logger
		) : base(
				conversionApplicationRetrievalService, 
				referenceDataRetrievalService, 
				conversionApplicationCreationService,
	"ApplicationNewTrustGovernanceSummary"
		) {
			_sharepoint = sharepointService;
			_logger = logger;
		}
		
		public bool GovernanceStructureDetailsFileError => !ModelState.IsValid && ModelState.Keys.Contains("GovernanceStructureDetailFileNotAddedError");
		public bool GovernanceStructureDetailsFileSizeError => !ModelState.IsValid && ModelState.ContainsKey(nameof(GovernanceStructureDetailsFileSizeError));
		public bool GovernanceStructureDetailsFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(GovernanceStructureDetailsFileGenericError));

		[BindProperty]
		public Guid EntityId { get; set; }
	
		[BindProperty]
		public string ApplicationReference { get; set; }
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
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			
			EntityId = applicationDetails.EntityId;
			ApplicationReference = applicationDetails.ApplicationReference;
			TrustName = applicationDetails?.TrustName ?? string.Empty;

			try
			{
				string folder = FileUploadConstants.FormatSharepointApplicationDirectory(ApplicationReference, EntityId.ToString());
				var files = await _sharepoint.ListFilesAsync(folder);
				
				GovernanceStructureDetailsFileNames = files
					.Where(file => file.Name.StartsWith(FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName))
					.Select(file => file.Name)
					.ToList();
				
				TempDataHelper.StoreSerialisedValue($"{EntityId}-governanceStructureDetailsFiles", TempData,
					GovernanceStructureDetailsFileNames);
			}
			catch
			{
				_logger.LogInformation("No Trust directory exists yet for application: {1} :: {2}",
					ApplicationReference, $"{ApplicationReference}_{EntityId}");

			}

			return Page();
		}
		
		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			EntityId = applicationDetails.EntityId;
			ApplicationReference = applicationDetails.ApplicationReference;
			
			GovernanceStructureDetailsFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-governanceStructureDetailsFiles", TempData) ?? new List<string>();

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
			
			return RedirectToPage(NextStepPage, new { appId = ApplicationId});
		}

		private async Task<bool> UploadFiles()
		{
			string folder = FileUploadConstants.FormatSharepointApplicationDirectory(ApplicationReference, EntityId.ToString());
			try
			{
				foreach (var file in GovernanceStructureDetailsFiles)
				{
					string fileName = $"{FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName}_{file.FileName}";
					await _sharepoint.UploadFileAsync(folder, fileName, file.OpenReadStream());
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(GovernanceStructureDetailsFileGenericError), "The selected file could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			return true;
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

			foreach (var file in GovernanceStructureDetailsFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
			{
				ModelState.AddModelError(nameof(GovernanceStructureDetailsFileSizeError), $"File: {file.FileName} is too large");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
		{
			string folder = FileUploadConstants.FormatSharepointApplicationDirectory(applicationReference, entityId);
			await _sharepoint.DeleteFileAsync(folder, fileName);
			
			return RedirectToPage("ApplicationNewTrustGovernanceStructureDetails", new { Urn = urn, AppId = appId });
		}
	}
}
