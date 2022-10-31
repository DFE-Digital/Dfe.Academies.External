using System.Text.Json;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class AdditionalDetails : BaseSchoolPageEditModel
	{
		private readonly IFileUploadService _fileUploadService;
		private readonly IConversionApplicationCreationService _conversionApplicationCreationService;
		
		private List<string> FileGroupingLists { get; set; }
		
		[BindProperty]
		public string TrustBenefitDetails { get; set; }
		
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
		
		public List<IFormFile> ResolutionConsentFiles { get; set; }
		
		[BindProperty]
		public List<string> ResolutionConsentFileNames { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption EqualityAssessment { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SchoolEqualitiesProtectedCharacteristics? DisproportionateProtectedCharacteristics { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption FurtherInformation { get; set; }
		
		public string? FurtherInformationDetails { get; set; }
		
		public bool HasError
		{
			get
			{
				var bools = new[] { OfstedInspectionDetailsNotAdded,
					ExemptionEndDateNotEntered,
					DioceseNameError,
					OfstedInspectedDetailsError,
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
		
		public bool OfstedInspectionDetailsNotAdded
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("OfstedInspectionDetailsNotAdded");
			}
		}

		public bool DioceseNameError => !ModelState.IsValid && ModelState.Keys.Contains("DioceseNameNotAdded");
		public bool OfstedInspectedDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("OfstedInspectionDetailsNotAdded");
		public bool SafeguardingInvestigationsError => !ModelState.IsValid && ModelState.Keys.Contains("SafeguardingDetailsNotAdded");
		public bool LocalAuthorityReorganisationDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("LocalAuthorityReorganisationDetailsNotAdded");
		public bool LocalAuthorityClosurePlanDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("localAuthorityClosurePlanDetailsNotAdded");
		public bool SupportedByFoundationTrustOrBodyError => !ModelState.IsValid && ModelState.Keys.Contains("FoundationTrustOrBodyNameNotAdded");
		public bool ExemptionFromSACREError => !ModelState.IsValid && ModelState.Keys.Contains("exemptionFromSACREEndDateNotAdded");
		public bool EqualityAssessmentError => !ModelState.IsValid && ModelState.Keys.Contains("equalitiesImpactAssessmentOptionNoOptionSelected");
		public bool FurtherInformationError => !ModelState.IsValid && ModelState.Keys.Contains("furtherInformationDetailsNotAdded");
		
		public bool ExemptionEndDateNotEntered => !ModelState.IsValid && ModelState.Keys.Contains("ExemptionEndDateNotEntered");

		public async Task<IActionResult> OnGetRemoveFileAsync(int appId, int urn, string section, string fileName)
		{
			await _fileUploadService.DeleteFile(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{appId}", section, fileName);
			return RedirectToPage("AdditionalDetails", new {Urn = urn, AppId = appId});
		}

		public override async Task OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();
		
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}
						
			OfstedInspected = !string.IsNullOrWhiteSpace(OfstedInspectionDetails) ? SelectOption.Yes : SelectOption.No;
			
			DioceseFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{ApplicationId}", FileUploadConstants.DioceseFilePrefixFieldName);
			FoundationConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{ApplicationId}", FileUploadConstants.FoundationConsentFilePrefixFieldName);
			ResolutionConsentFileNames = await _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, appId.ToString(), $"A2B_{ApplicationId}", FileUploadConstants.ResolutionConsentfilePrefixFieldName);
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			
			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);
			var exemptionEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(Request.Form, ExemptionEndDateName);
			string ExemptionEndDateComponentDay = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string ExemptionEndDateComponentMonth = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string ExemptionEndDateComponentYear = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var dateTime = BuildDateTime(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
			ExemptionEndDate = dateTime == DateTime.MinValue ? null : dateTime;
			if (!RunUiValidation())
			{
				RePopDatePickerModel(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
			
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);
			
			DioceseFiles?.ForEach(async file =>
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.DioceseFilePrefixFieldName, file);
			});
			FoundationConsentFiles?.ForEach(async file =>
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.FoundationConsentFilePrefixFieldName, file);
			});
			ResolutionConsentFiles?.ForEach(async file =>
			{
				await _fileUploadService.UploadFile(FileUploadConstants.TopLevelFolderName, ApplicationId.ToString(), applicationDetails.ApplicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName, file);				
			});

			await _conversionApplicationCreationService.SetAdditionalDetails(
				ApplicationId,
				selectedSchool.id,
				TrustBenefitDetails,
				OfstedInspectionDetails,
				SafeguardingDetails,
				LocalAuthorityReorganisationDetails,
				LocalAuthorityClosurePlanDetails,
				DioceseName,
				FileUploadConstants.DioceseFilePrefixFieldName,
				PartOfFederation == SelectOption.Yes,
				FoundationTrustOrBodyName,
				FileUploadConstants.FoundationConsentFilePrefixFieldName,
				ExemptionEndDate,
				MainFeederSchools,
				FileUploadConstants.ResolutionConsentfilePrefixFieldName,
				DisproportionateProtectedCharacteristics,
				FurtherInformationDetails);
			
			
			
			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			
			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}
		
		public AdditionalDetails(IFileUploadService fileUploadService, IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "application-overview")
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

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			TrustBenefitDetails = selectedSchool.TrustBenefitDetails;
			OfstedInspectionDetails = selectedSchool.TrustBenefitDetails;
			SafeguardingDetails = selectedSchool.SafeguardingDetails;
			LocalAuthorityReorganisation = selectedSchool.LocalAuthorityReorganisationDetails == null
				? SelectOption.No
				: SelectOption.Yes;
			LocalAuthorityReorganisationDetails = selectedSchool.LocalAuthorityReorganisationDetails;
			LocalAuthorityClosurePlans = selectedSchool.LocalAuthorityClosurePlanDetails == null
				? SelectOption.No
				: SelectOption.Yes;
			LocalAuthorityClosurePlanDetails = selectedSchool.LocalAuthorityClosurePlanDetails;
			LinkedToDiocese = DioceseName == null
				? SelectOption.No
				: SelectOption.Yes;
			DioceseName = selectedSchool.DioceseName;

			PartOfFederation = selectedSchool.PartOfFederation ? SelectOption.Yes : SelectOption.No;
			SupportedByFoundationTrustOrBody = FoundationTrustOrBodyName == null
				? SelectOption.No
				: SelectOption.Yes;
			
			ExemptionEndDate = selectedSchool.ExemptionEndDate;
			MainFeederSchools = selectedSchool.MainFeederSchools;
			
			DisproportionateProtectedCharacteristics = selectedSchool.ProtectedCharacteristics;
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
