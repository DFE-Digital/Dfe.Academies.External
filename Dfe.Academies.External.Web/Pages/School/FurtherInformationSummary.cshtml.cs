using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class FurtherInformationSummaryModel : BaseSchoolSummaryPageModel
	{
		private readonly IFileUploadService _fileUploadService;
	    public List<FurtherInformationSummaryViewModel> ViewModel { get; set; } = new();

	    [BindProperty]
	    public Guid EntityId { get; set; }
	
	    [BindProperty]
	    public string ApplicationReference { get; set; }

	    
	    public FurtherInformationSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		    IReferenceDataRetrievalService referenceDataRetrievalService, IFileUploadService fileUploadService)
		    : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
	    {
		    _fileUploadService = fileUploadService;
	    }

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
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
			// does not apply on this page
			return new();
		}
		
	    private FurtherInformationSummaryViewModel PopulateFurtherInformation(SchoolApplyingToConvert selectedSchool)
	    {
		    var applicationDetails = ConversionApplicationRetrievalService.GetApplication(ApplicationId).Result;
		    EntityId = selectedSchool.EntityId;
		    ApplicationReference = applicationDetails.ApplicationReference;
		    var sectionStarted = !string.IsNullOrEmpty(selectedSchool.TrustBenefitDetails);
			// Heading
			FurtherInformationSummaryViewModel FISheading = new(FurtherInformationSummaryViewModel.AdditionalDetailsHeading,
				"/school/AdditionalDetails")
		    {
			    Status = sectionStarted ?
				    SchoolConversionComponentStatus.Complete
				    : SchoolConversionComponentStatus.NotStarted
		    };
			FISheading.Sections.Add(new(
					FurtherInformationSectionViewModel.SchoolTrustBenefit,
				!string.IsNullOrWhiteSpace(selectedSchool.TrustBenefitDetails) ?
					selectedSchool.TrustBenefitDetails : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.OfstedInspection,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.OfstedInspectionDetails) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.SafeguardingInvestigations,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.SafeguardingDetails) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.LocalAuthorityReorganisation,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.LocalAuthorityClosurePlanDetails) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.LocalAuthorityClosurePlans,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.LocalAuthorityClosurePlanDetails) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.Diocese,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.DioceseName) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.Federation,
				sectionStarted ?
					((selectedSchool.PartOfFederation) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.FoundationTrustOrBody,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.FoundationTrustOrBodyName) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.ExemptionSACRE,
				sectionStarted ?
					((selectedSchool.ExemptionEndDate.HasValue) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.MainFeederSchools,
				!string.IsNullOrWhiteSpace(selectedSchool.MainFeederSchools) ?
					selectedSchool.MainFeederSchools : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			var fileNames = _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, EntityId.ToString(), ApplicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName).Result;
			
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.Resolution,
				fileNames.Any() ? fileNames.First() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.EqualitiesImpactAssessment,
				sectionStarted ?
					((selectedSchool.ProtectedCharacteristics.HasValue) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);

			FISheading.Sections.Add(new(
				FurtherInformationSectionViewModel.FurtherInformation,
				sectionStarted ?
					(!string.IsNullOrWhiteSpace(selectedSchool.FurtherInformation) ? "Yes" : "No") : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
			return FISheading;
	    }

	    public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	    {
		    ViewModel = new List<FurtherInformationSummaryViewModel> { PopulateFurtherInformation(selectedSchool) };
	    }
	}
}
