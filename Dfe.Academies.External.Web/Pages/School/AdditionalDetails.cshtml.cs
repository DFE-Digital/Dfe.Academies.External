using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
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

		[BindProperty] public List<IFormFile>? DioceseFiles { get; set; } = new List<IFormFile>();
		
		//No additional text box
		[BindProperty]
		public bool PartOfFederation { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption SupportedByFoundationTrustOrBody { get; set; }
		
		[BindProperty]
		public string? FoundationTrustOrBodyName { get; set; }
		
		[BindProperty]
		public List<IFormFile>? FoundationConsentFiles { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption ExemptionFromSACRE { get; set; }
		
		[BindProperty]
		public DateTimeOffset ExemptionEndDate { get; set; }
		
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
		
		[BindProperty]
		public List<IFormFile> ResolutionConsentFiles { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption EqualityAssessment { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SchoolEqualitiesProtectedCharacteristics DisproportionateProtectedCharacteristics { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption FurtherInformation { get; set; }
		
		public string? FurtherInformationDetails { get; set; }
		
		public bool HasError
		{
			get
			{
				var bools = new[] { OfstedInspectionDetailsNotAdded,ExemptionEndDateNotEntered };

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

		public bool ExemptionEndDateNotEntered => !ModelState.IsValid && ModelState.Keys.Contains("ExemptionEndDateNotEntered");

		public override Task OnGetAsync(int urn, int appId)
		{
			OfstedInspected = !string.IsNullOrWhiteSpace(OfstedInspectionDetails) ? SelectOption.Yes : SelectOption.No;

			_fileUploadService.GetFileNames("sip_changestotrustconsent", appId.ToString(),
				"2E1744ED8976EB11A81200224880B3B5", "example");
			return base.OnGetAsync(urn, appId);
		}

		public override async Task<IActionResult> OnPostAsync()
		{
	
			var exemptionEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(Request.Form, ExemptionEndDateName);
			string ExemptionEndDateComponentDay = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string ExemptionEndDateComponentMonth = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string ExemptionEndDateComponentYear = exemptionEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			ExemptionEndDate = BuildDateTime(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
			if (!RunUiValidation())
			{
				RePopDatePickerModel(ExemptionEndDateComponentDay, ExemptionEndDateComponentMonth, ExemptionEndDateComponentYear);
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = PopulateUpdateDictionary();
			await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}
		
		public AdditionalDetails(IFileUploadService fileUploadService, IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "FurtherInformationSummary")
		{
			_fileUploadService = fileUploadService;
		}

		public override void PopulateValidationMessages()
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

			if (OfstedInspected == SelectOption.Yes && string.IsNullOrWhiteSpace(OfstedInspectionDetails))
			{
				ModelState.AddModelError("OfstedInspectionDetailsNotAdded", "You must provide details");
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
			OfstedInspectionDetails = selectedSchool.LocalAuthority.LaClosurePlanDetails;
		}
		
		private void RePopDatePickerModel(string exemptionEndDateDay, string exemptionEndDateMonth, string exemptionEndDateYear)
		{
			ExemptionEndDateDay = exemptionEndDateDay;
			ExemptionEndDateMonth = exemptionEndDateMonth;
			ExemptionEndDateYear = exemptionEndDateYear;
		}
	}
}
