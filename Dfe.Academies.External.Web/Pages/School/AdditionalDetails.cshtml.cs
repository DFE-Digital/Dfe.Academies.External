using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.CustomValidators;
using Dfe.Academies.External.Web.Enums;
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
		private readonly IConversionApplicationCreationService _conversionApplicationCreationService;
		
		[BindProperty]
		public string? TrustBenefitDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption OfstedInspected { get; set; }
		
		[BindProperty]
		public string? OfstedInspectionDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SafeguardingInvestigations { get; set; }
		
		[BindProperty]
		public string? SafeguardingDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption LocalAuthorityReorganisation { get; set; }
		
		[BindProperty]
		public string? LocalAuthorityReorganisationDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption LocalAuthorityClosurePlans { get; set; }
		
		[BindProperty]
		public string? LocalAuthorityClosurePlanDetails { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption LinkedToDiocese { get; set; }
		
		[BindProperty]
		public string? DioceseName { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? DioceseFiles { get; set; } = new();
		
		[BindProperty]
		public List<string> DioceseFileNames { get; set; }
		
		//No additional text box
		[BindProperty]
		public SelectOption PartOfFederation { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SupportedByFoundationTrustOrBody { get; set; }
		
		[BindProperty]
		public string? FoundationTrustOrBodyName { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		public List<IFormFile>? FoundationConsentFiles { get; set; } = new();
		
		[BindProperty]
		public List<string> FoundationConsentFileNames { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption ExemptionFromSACRE { get; set; }
		
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
		public string MainFeederSchools { get; set; }

		[DataType(DataType.Upload)]
		[AllowedExtensions(new[] { ".doc", ".docx", ".ppt", ".pptx", ".pdf" })]
		[BindProperty]
		public List<IFormFile> ResolutionConsentFiles { get; set; }
		
		[BindProperty]
		public List<string> ResolutionConsentFileNames { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption EqualityAssessment { get; set; }
		
		[BindProperty]
		public SchoolEqualitiesProtectedCharacteristics? DisproportionateProtectedCharacteristics { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption FurtherInformation { get; set; }
		
		[BindProperty]
		public string? FurtherInformationDetails { get; set; }
		
		public new string SchoolName { get; private set; } = string.Empty;
		
		public bool HasError
		{
			get
			{
				var bools = new[] {
					TrustBenefitDetailsError,
					OfstedInspectedDetailsError,
					ExemptionEndDateNotEntered,
					DioceseNameError,
					DioceseFileError,
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
		public bool ResolutionConsentFileError => !ModelState.IsValid && ModelState.Keys.Contains("ResolutionConsentFileNotAddedError");
		public bool DioceseFileError => !ModelState.IsValid && ModelState.Keys.Contains("DioceseFileNotAddedError");
		
		public bool ExemptionFromSACREError => !ModelState.IsValid && ModelState.Keys.Contains("exemptionFromSACREEndDateNotAdded");
		public bool EqualityAssessmentError => !ModelState.IsValid && ModelState.Keys.Contains("equalitiesImpactAssessmentOptionNoOptionSelected");
		public bool FurtherInformationError => !ModelState.IsValid && ModelState.Keys.Contains("furtherInformationDetailsNotAdded");
		
		public bool ExemptionEndDateNotEntered => !ModelState.IsValid && ModelState.Keys.Contains("ExemptionEndDateNotEntered");
		public bool MainFeederSchoolsError => !ModelState.IsValid && ModelState.Keys.Contains("MainFeederSchoolsDetailsNotAdded");

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string section, string fileName)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{appId}", section, fileName);
			return RedirectToPage("AdditionalDetails", new {Urn = urn, AppId = appId});
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
						
			OfstedInspected = !string.IsNullOrWhiteSpace(OfstedInspectionDetails) ? SelectOption.Yes : SelectOption.No;
			
			DioceseFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.DioceseFilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{appId}-dioceseFiles", TempData, DioceseFileNames);
			FoundationConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.FoundationConsentFilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{appId}-foundationConsentFiles", TempData, FoundationConsentFileNames);
			ResolutionConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName);
			TempDataHelper.StoreSerialisedValue($"{appId}-resolutionConsentFiles", TempData, ResolutionConsentFileNames);
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

			DioceseFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-dioceseFiles", TempData) ?? new List<string>();
			FoundationConsentFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-foundationConsentFiles", TempData) ?? new List<string>();
			ResolutionConsentFileNames = TempDataHelper.GetSerialisedValue<List<string>>($"{ApplicationId}-resolutionConsentFiles", TempData) ?? new List<string>();
			
			if (!RunUiValidation())
			{
				RePopDatePickerModel(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			foreach (var file in DioceseFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.DioceseFilePrefixFieldName, file);
			}

			foreach (var file in FoundationConsentFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.FoundationConsentFilePrefixFieldName, file);
			}

			foreach (var file in ResolutionConsentFiles)
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName, file);
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
				SafeguardingDetails,
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
		
		public AdditionalDetails(IFileUploadService fileUploadService, IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "FurtherInformationSummary")
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
			if (string.IsNullOrWhiteSpace(TrustBenefitDetails))
			{
				ModelState.AddModelError("TrustBenefitDetailsNotAdded", "You must enter details of the trust benefit");
				PopulateValidationMessages();
				return false;
			}
			if (OfstedInspected == SelectOption.Yes && string.IsNullOrWhiteSpace(OfstedInspectionDetails))
			{
				ModelState.AddModelError("OfstedInspectionDetailsNotAdded", "You must enter Ofsted inspection details");
				PopulateValidationMessages();
				return false;
			}
			if (SafeguardingInvestigations == SelectOption.Yes && string.IsNullOrWhiteSpace(SafeguardingDetails))
			{
				ModelState.AddModelError("SafeguardingDetailsNotAdded", "You must enter safeguarding investigation details");
				PopulateValidationMessages();
				return false;
			}
			if (LocalAuthorityReorganisation == SelectOption.Yes && string.IsNullOrWhiteSpace(LocalAuthorityReorganisationDetails))
			{
				ModelState.AddModelError("LocalAuthorityReorganisationDetailsNotAdded", "You must enter details of the reorganisation");
				PopulateValidationMessages();
				return false;
			}
			if (LocalAuthorityClosurePlans == SelectOption.Yes && string.IsNullOrWhiteSpace(LocalAuthorityClosurePlanDetails))
			{
				ModelState.AddModelError("localAuthorityClosurePlanDetailsNotAdded", "You must enter details of the closure plans");
				PopulateValidationMessages();
				return false;
			}

			if (LinkedToDiocese == SelectOption.Yes && string.IsNullOrWhiteSpace(DioceseName))
			{
				ModelState.AddModelError("DioceseNameNotAdded", "You must enter the name of the diocese");
				PopulateValidationMessages();
				return false;
			}
			if (SupportedByFoundationTrustOrBody == SelectOption.Yes && string.IsNullOrWhiteSpace(FoundationTrustOrBodyName))
			{
				ModelState.AddModelError("FoundationTrustOrBodyNameNotAdded", "You must enter the name of the body");
				PopulateValidationMessages();
				return false;
			}
			
			if (ExemptionFromSACRE == SelectOption.Yes && (!ExemptionEndDate.HasValue ||
			    ExemptionEndDate.Value == DateTimeOffset.MinValue))
			{
				ModelState.AddModelError("exemptionFromSACREEndDateNotAdded", "You must enter a valid date");
				PopulateValidationMessages();
				return false;
			}

			if (string.IsNullOrWhiteSpace(MainFeederSchools))
			{
				ModelState.AddModelError("MainFeederSchoolsDetailsNotAdded", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (EqualityAssessment == SelectOption.Yes && DisproportionateProtectedCharacteristics == null)
			{
				ModelState.AddModelError("equalitiesImpactAssessmentOptionNoOptionSelected", "You must select an equalities impact assessment option");
				PopulateValidationMessages();
				return false;
			}

			if (FurtherInformation == SelectOption.Yes && string.IsNullOrWhiteSpace(FurtherInformationDetails))
			{
				ModelState.AddModelError("furtherInformationDetailsNotAdded", "You must provide details");
				PopulateValidationMessages();
				return false;
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
			SchoolName = selectedSchool.SchoolName;
			TrustBenefitDetails = selectedSchool.TrustBenefitDetails;
			OfstedInspectionDetails = selectedSchool.OfstedInspectionDetails;
			OfstedInspected = selectedSchool.OfstedInspectionDetails == null
				? SelectOption.No
				: SelectOption.Yes;
			SafeguardingDetails = selectedSchool.SafeguardingDetails;
			LocalAuthorityReorganisation = selectedSchool.LocalAuthorityReorganisationDetails == null
				? SelectOption.No
				: SelectOption.Yes;
			LocalAuthorityReorganisationDetails = selectedSchool.LocalAuthorityReorganisationDetails;
			LocalAuthorityClosurePlans = selectedSchool.LocalAuthorityClosurePlanDetails == null
				? SelectOption.No
				: SelectOption.Yes;
			LocalAuthorityClosurePlanDetails = selectedSchool.LocalAuthorityClosurePlanDetails;
			DioceseName = selectedSchool.DioceseName;
			LinkedToDiocese = selectedSchool.DioceseName == null
				? SelectOption.No
				: SelectOption.Yes;

			PartOfFederation = selectedSchool.PartOfFederation ? SelectOption.Yes : SelectOption.No;
			FoundationTrustOrBodyName = selectedSchool.FoundationTrustOrBodyName;
			SupportedByFoundationTrustOrBody = selectedSchool.FoundationTrustOrBodyName == null
				? SelectOption.No
				: SelectOption.Yes;

			ExemptionEndDate = selectedSchool.ExemptionEndDate;
			ExemptionFromSACRE = ExemptionEndDate.HasValue ? SelectOption.Yes : SelectOption.No;
			
			MainFeederSchools = selectedSchool.MainFeederSchools;
			
			DisproportionateProtectedCharacteristics = selectedSchool.ProtectedCharacteristics;
			EqualityAssessment = DisproportionateProtectedCharacteristics.HasValue ? SelectOption.Yes : SelectOption.No;
			FurtherInformation = selectedSchool.FurtherInformation == null 
				? SelectOption.No 
				: SelectOption.Yes;
			FurtherInformationDetails = selectedSchool.FurtherInformation;

		}
		
		private void RePopDatePickerModel(string exemptionEndDateDay, string exemptionEndDateMonth, string exemptionEndDateYear)
		{
			ExemptionEndDateDay = exemptionEndDateDay;
			ExemptionEndDateMonth = exemptionEndDateMonth;
			ExemptionEndDateYear = exemptionEndDateYear;
		}
	}
}
