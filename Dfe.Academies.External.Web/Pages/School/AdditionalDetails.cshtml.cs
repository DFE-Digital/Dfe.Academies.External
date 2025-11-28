using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class AdditionalDetails : BaseSchoolPageEditModel
	{
		private readonly IFileUploadService _fileUploadService;
		private readonly IConversionApplicationService _conversionApplicationCreationService;
		
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string? TrustBenefitDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? OfstedInspected { get; set; }
		
		[BindProperty]
		public string? OfstedInspectionDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? SafeguardingInvestigations { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? LocalAuthorityReorganisation { get; set; }
		
		[BindProperty]
		public string? LocalAuthorityReorganisationDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? LocalAuthorityClosurePlans { get; set; }
		
		[BindProperty]
		public string? LocalAuthorityClosurePlanDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? LinkedToDiocese { get; set; }
		
		[BindProperty]
		public string? DioceseName { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? DioceseFiles { get; set; } = new();
		
		[BindProperty]
		public List<string> DioceseFileNames { get; set; }
		
		//No additional text box
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? PartOfFederation { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? SupportedByFoundationTrustOrBody { get; set; }
		
		[BindProperty]
		public string? FoundationTrustOrBodyName { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? FoundationConsentFiles { get; set; } = new();
		
		[BindProperty]
		public List<string> FoundationConsentFileNames { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? ExemptionFromSACRE { get; set; }
		
		[BindProperty]
		public DateTimeOffset? ExemptionEndDate { get; set; }
		
		public string ExemptionEndDateFormInputName = "exemptionenddate";
 
		[BindProperty]
		public string? ExemptionEndDateName { get; set; }

		[BindProperty] 
		public string? ExemptionEndDateDay { get; set; }

		[BindProperty]
		public string? ExemptionEndDateMonth { get; set; }

		[BindProperty] 
		public string? ExemptionEndDateYear { get; set; }
		
		[BindProperty]
		[Required(ErrorMessage = "Please provide a list of your main feeder schools")]

		public string MainFeederSchools { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		[BindProperty]
		public List<IFormFile> ResolutionConsentFiles { get; set; }
		
		[BindProperty]
		public List<string> ResolutionConsentFileNames { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? EqualityAssessment { get; set; }
		
		[BindProperty]
		public SchoolEqualitiesProtectedCharacteristics? DisproportionateProtectedCharacteristics { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? FurtherInformation { get; set; }
		
		[BindProperty]
		public string? FurtherInformationDetails { get; set; }
		
		public new string SchoolName { get; private set; } = string.Empty;
		
		[BindProperty]
		public Guid EntityId { get; set; }
	
		[BindProperty]
		public string ApplicationReference { get; set; }
		
		public bool HasError
		{
			get
			{
				var bools = new[] {
					TrustBenefitDetailsError,
					OfstedInspectedDetailsError,
					ExemptionEndDateNotEntered,
					DioceseNameError,
					DioceseFileNotAddedError,
					SafeguardingInvestigationsError, 
					LocalAuthorityReorganisationDetailsError, 
					LocalAuthorityClosurePlanDetailsError,
					SupportedByFoundationTrustOrBodyError,
					ExemptionFromSACREError,
					EqualityAssessmentError,
					FurtherInformationError
				};

				return bools.Any(b => b);
			}
		}
		public bool TrustBenefitDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("TrustBenefitDetailsNotAdded");
		public bool OfstedInspectedDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("OfstedInspectionDetailsNotAdded");
		public bool SafeguardingInvestigationsError => !ModelState.IsValid && ModelState.Keys.Contains("SafeguardingDetailsNotAdded");
		public bool DioceseNameError => !ModelState.IsValid && ModelState.Keys.Contains("DioceseNameNotAdded");
		public bool LocalAuthorityReorganisationDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("LocalAuthorityReorganisationDetailsNotAdded");
		public bool LocalAuthorityClosurePlanDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("localAuthorityClosurePlanDetailsNotAdded");
		public bool SupportedByFoundationTrustOrBodyError => !ModelState.IsValid && ModelState.Keys.Contains("FoundationTrustOrBodyNameNotAdded");
		public bool FoundationConsentFileError => !ModelState.IsValid && ModelState.Keys.Contains("FoundationConsentFileNotAddedError");
		public bool FoundationConsentFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("FoundationConsentFileSizeError");
		public bool ResolutionConsentFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("ResolutionConsentFileSizeError");
		
		public bool DioceseFileNotAddedError => !ModelState.IsValid && ModelState.Keys.Contains("DioceseFileNotAddedError");
		public bool DioceseFileSizeError => !ModelState.IsValid && ModelState.Keys.Contains("DioceseFileSizeError");
		public bool DioceseFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(DioceseFileGenericError));
		public bool FoundationConsentFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(FoundationConsentFileGenericError));
		public bool ResolutionConsentFileGenericError => !ModelState.IsValid && ModelState.ContainsKey(nameof(ResolutionConsentFileGenericError));
		public bool ExemptionFromSACREError => !ModelState.IsValid && ModelState.Keys.Contains("exemptionFromSACREEndDateNotAdded");
		public bool EqualityAssessmentError => !ModelState.IsValid && ModelState.Keys.Contains("equalitiesImpactAssessmentOptionNoOptionSelected");
		public bool FurtherInformationError => !ModelState.IsValid && ModelState.Keys.Contains("furtherInformationDetailsNotAdded");
		
		public bool ExemptionEndDateNotEntered => !ModelState.IsValid && ModelState.Keys.Contains("ExemptionEndDateNotEntered");
		public bool MainFeederSchoolsError => !ModelState.IsValid && ModelState.Keys.Contains("MainFeederSchoolsDetailsNotAdded");

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string entityId, string applicationReference, string section, string fileName)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelSchoolFolderName, entityId, applicationReference, section, fileName);
			return RedirectToPage("AdditionalDetails", new { Urn = urn, AppId = appId });
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
				PopulateUiModel(selectedSchool);
			}
			ApplicationReference = applicationDetails.ApplicationReference;
			
			DioceseFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.DioceseFilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{EntityId}-dioceseFiles", TempData, DioceseFileNames);
			FoundationConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.FoundationConsentFilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{EntityId}-foundationConsentFiles", TempData, FoundationConsentFileNames);
			ResolutionConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{EntityId}-resolutionConsentFiles", TempData, ResolutionConsentFileNames);
			return Page();
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == Urn);
			var exemptionEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(Request.Form, ExemptionEndDateName);
			string ExemptionEndDateComponentDay = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string ExemptionEndDateComponentMonth = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string ExemptionEndDateComponentYear = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var dateTime = BuildDateTime(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
			ExemptionEndDate = dateTime == DateTime.MinValue ? null : dateTime;

			DioceseFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-dioceseFiles", TempData) ?? new List<string>();
			FoundationConsentFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-foundationConsentFiles", TempData) ?? new List<string>();
			ResolutionConsentFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{EntityId}-resolutionConsentFiles", TempData) ?? new List<string>();
			
			if (!RunUiValidation())
			{
				RePopDatePickerModel(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			if (!(await UploadFiles()))
			{
				RePopDatePickerModel(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
				return Page();
			}
			
			SetBindedProperties();

			var dioceseFolderIdentifier = (DioceseFileNames.Any() || DioceseFiles.Any())
				? FileUploadConstants.DioceseFilePrefixFieldName
				: null;

			var foundationConsentFolderIdentifier = (FoundationConsentFileNames.Any() || FoundationConsentFiles.Any())
				? FileUploadConstants.FoundationConsentFilePrefixFieldName
				: null;

			var resolutionConsentFolderIdentifier = (ResolutionConsentFileNames.Any() || ResolutionConsentFiles.Any())
				? FileUploadConstants.ResolutionConsentfilePrefixFieldName
				: null;
			
			await _conversionApplicationCreationService.SetAdditionalDetails(
				ApplicationId,
				selectedSchool.id,
				TrustBenefitDetails,
				OfstedInspectionDetails,
				SafeguardingInvestigations == SelectOption.Yes,
				LocalAuthorityReorganisationDetails,
				LocalAuthorityClosurePlanDetails,
				DioceseName,
				dioceseFolderIdentifier,
				PartOfFederation == SelectOption.Yes,
				FoundationTrustOrBodyName,
				foundationConsentFolderIdentifier,
				ExemptionEndDate,
				MainFeederSchools,
				resolutionConsentFolderIdentifier,
				DisproportionateProtectedCharacteristics,
				FurtherInformationDetails);
			
			
			
			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			
			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}
		
		public AdditionalDetails(IFileUploadService fileUploadService, IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationService conversionApplicationCreationService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "FurtherInformationSummary")
		{
			_fileUploadService = fileUploadService;
			_conversionApplicationCreationService = conversionApplicationCreationService;
		}
		
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public override bool RunUiValidation()
		{
			if (OfstedInspected == SelectOption.Yes && string.IsNullOrWhiteSpace(OfstedInspectionDetails))
			{
				ModelState.AddModelError("OfstedInspectionDetailsNotAdded", "You must enter Ofsted inspection details");
			}
			if (LocalAuthorityReorganisation == SelectOption.Yes && string.IsNullOrWhiteSpace(LocalAuthorityReorganisationDetails))
			{
				ModelState.AddModelError("LocalAuthorityReorganisationDetailsNotAdded", "You must enter details of the reorganisation");
			}
			if (LocalAuthorityClosurePlans == SelectOption.Yes && string.IsNullOrWhiteSpace(LocalAuthorityClosurePlanDetails))
			{
				ModelState.AddModelError("localAuthorityClosurePlanDetailsNotAdded", "You must enter details of the closure plans");
			}

			if (LinkedToDiocese == SelectOption.Yes && string.IsNullOrWhiteSpace(DioceseName))
			{
				ModelState.AddModelError("DioceseNameNotAdded", "You must enter the name of the diocese");
			}
			if (SupportedByFoundationTrustOrBody == SelectOption.Yes && string.IsNullOrWhiteSpace(FoundationTrustOrBodyName))
			{
				ModelState.AddModelError("FoundationTrustOrBodyNameNotAdded", "You must enter the name of the body");
			}
			
			if (ExemptionFromSACRE == SelectOption.Yes && (!ExemptionEndDate.HasValue ||
			    ExemptionEndDate.Value == DateTimeOffset.MinValue))
			{
				ModelState.AddModelError("exemptionFromSACREEndDateNotAdded", "You must enter a valid date");
			}

			if (EqualityAssessment == SelectOption.Yes && DisproportionateProtectedCharacteristics == null)
			{
				ModelState.AddModelError("equalitiesImpactAssessmentOptionNoOptionSelected", "You must select an equalities impact assessment option");
			}

			if (FurtherInformation == SelectOption.Yes && string.IsNullOrWhiteSpace(FurtherInformationDetails))
			{
				ModelState.AddModelError("furtherInformationDetailsNotAdded", "You must provide details");
			}

			foreach (var file in ResolutionConsentFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
			{
				ModelState.AddModelError(nameof(ResolutionConsentFileSizeError), $"File: {file.FileName} is too large");
			}

			if (FoundationConsentFiles != null)
			{
				foreach (var file in FoundationConsentFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
				{
					ModelState.AddModelError(nameof(FoundationConsentFileSizeError), $"File: {file.FileName} is too large");
				}
			}

			if (DioceseFiles != null)
			{
				foreach (var file in DioceseFiles.Where(file => file.Length >= FileUploadConstants.MaxFileUploadSizeInBytes))
				{
					ModelState.AddModelError(nameof(DioceseFileSizeError), $"File: {file.FileName} is too large");
				}
			}

			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}
			
			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			throw new NotImplementedException();
		}

		private void SetBindedProperties()
		{
			OfstedInspectionDetails = OfstedInspected == SelectOption.No ? null : OfstedInspectionDetails;
			LocalAuthorityReorganisationDetails = LocalAuthorityReorganisation == SelectOption.No ? null : LocalAuthorityReorganisationDetails;
			LocalAuthorityClosurePlanDetails = LocalAuthorityClosurePlans == SelectOption.No ? null : LocalAuthorityClosurePlanDetails;
			DioceseName = LinkedToDiocese == SelectOption.No ? null : DioceseName;
			FoundationTrustOrBodyName = SupportedByFoundationTrustOrBody == SelectOption.No ? null : FoundationTrustOrBodyName;
			FurtherInformationDetails = FurtherInformation == SelectOption.No ? null : FurtherInformationDetails;
			DisproportionateProtectedCharacteristics = EqualityAssessment == SelectOption.No ? null : DisproportionateProtectedCharacteristics;
		}

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			var sectionStarted = !string.IsNullOrEmpty(selectedSchool.TrustBenefitDetails);

			SchoolName = selectedSchool.SchoolName;
			TrustBenefitDetails = selectedSchool.TrustBenefitDetails;
			OfstedInspectionDetails = selectedSchool.OfstedInspectionDetails;

			OfstedInspected = selectedSchool.OfstedInspectionDetails == null
				? (sectionStarted ? SelectOption.No : null)
				: SelectOption.Yes;

			SafeguardingInvestigations = (sectionStarted ? selectedSchool.Safeguarding.GetEnumValue() : null);
			LocalAuthorityReorganisation = selectedSchool.LocalAuthorityReorganisationDetails == null
				? (sectionStarted ? SelectOption.No : null)
				: SelectOption.Yes;
			LocalAuthorityReorganisationDetails = selectedSchool.LocalAuthorityReorganisationDetails;

			LocalAuthorityClosurePlans = selectedSchool.LocalAuthorityClosurePlanDetails == null
				? (sectionStarted ? SelectOption.No : null)
				: SelectOption.Yes;

			LocalAuthorityClosurePlanDetails = selectedSchool.LocalAuthorityClosurePlanDetails;
			DioceseName = selectedSchool.DioceseName;
			LinkedToDiocese = selectedSchool.DioceseName == null
				? (sectionStarted ? SelectOption.No : null)
				: SelectOption.Yes;

			PartOfFederation = (sectionStarted ? selectedSchool.PartOfFederation.GetEnumValue() : null);
			FoundationTrustOrBodyName = selectedSchool.FoundationTrustOrBodyName;
			SupportedByFoundationTrustOrBody = selectedSchool.FoundationTrustOrBodyName == null
				? (sectionStarted ? SelectOption.No : null)
				: SelectOption.Yes;

			ExemptionEndDate = selectedSchool.ExemptionEndDate;
			ExemptionFromSACRE = ExemptionEndDate.HasValue ? SelectOption.Yes : (sectionStarted ? SelectOption.No : null);
			
			MainFeederSchools = selectedSchool.MainFeederSchools;
			
			DisproportionateProtectedCharacteristics = selectedSchool.ProtectedCharacteristics;
			EqualityAssessment = DisproportionateProtectedCharacteristics.HasValue ? SelectOption.Yes : (sectionStarted ? SelectOption.No : null);
			FurtherInformation = selectedSchool.FurtherInformation == null 
				? (sectionStarted ? SelectOption.No : null)
				: SelectOption.Yes;
			FurtherInformationDetails = selectedSchool.FurtherInformation;
			EntityId = selectedSchool.EntityId;

		}
		
		private void RePopDatePickerModel(string exemptionEndDateDay, string exemptionEndDateMonth, string exemptionEndDateYear)
		{
			ExemptionEndDateDay = exemptionEndDateDay;
			ExemptionEndDateMonth = exemptionEndDateMonth;
			ExemptionEndDateYear = exemptionEndDateYear;
		}

		private async Task<bool> UploadFiles()
		{
			try
			{
				foreach (var file in DioceseFiles)
				{
					await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
						ApplicationReference, FileUploadConstants.DioceseFilePrefixFieldName, file);
				}
			}
			catch (FileUploadException)
			{
				
				ModelState.AddModelError(nameof(DioceseFileGenericError), "The selected file could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			try
			{
				foreach (var file in FoundationConsentFiles)
				{
					await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
						ApplicationReference, FileUploadConstants.FoundationConsentFilePrefixFieldName, file);
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(FoundationConsentFileGenericError), "The selected file could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			try
			{
				foreach (var file in ResolutionConsentFiles)
				{
					await _fileUploadService.UploadFile(FileUploadConstants.TopLevelSchoolFolderName, EntityId.ToString(),
						ApplicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName, file);
				}
			}
			catch (FileUploadException)
			{
				ModelState.AddModelError(nameof(ResolutionConsentFileGenericError), "The selected file could not be uploaded – try again");
				PopulateValidationMessages();
				return false;
			}

			return true;
		}
	}
}
