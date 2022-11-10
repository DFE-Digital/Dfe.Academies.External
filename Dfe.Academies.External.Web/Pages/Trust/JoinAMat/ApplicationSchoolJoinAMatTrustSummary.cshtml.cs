using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.SummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust
{
<<<<<<<< HEAD:Dfe.Academies.External.Web/Pages/Trust/JoinAMat/ApplicationSchoolTrustSummary.cshtml.cs
    public class ApplicationSchoolTrustSummaryModel : BaseApplicationSummaryPageModel
	{
========
    public class ApplicationSchoolJoinAMatTrustSummaryModel : BaseApplicationSummaryPageModel
    {
	    private readonly IFileUploadService _fileUploadService;
>>>>>>>> master:Dfe.Academies.External.Web/Pages/Trust/JoinAMat/ApplicationSchoolJoinAMatTrustSummary.cshtml.cs
		//// Below are props for UI display
		public ApplicationTypes ApplicationType { get; private set; }

		public string SelectedTrustName { get; private set; }

		public List<ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel> ViewModel { get; set; } = new();

<<<<<<<< HEAD:Dfe.Academies.External.Web/Pages/Trust/JoinAMat/ApplicationSchoolTrustSummary.cshtml.cs
		public ApplicationSchoolTrustSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
========
		public ApplicationSchoolJoinAMatTrustSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService, IFileUploadService fileUploadService)
>>>>>>>> master:Dfe.Academies.External.Web/Pages/Trust/JoinAMat/ApplicationSchoolJoinAMatTrustSummary.cshtml.cs
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_fileUploadService = fileUploadService;
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				SelectedTrustName = conversionApplication.JoinTrustDetails?.TrustName ?? string.Empty;

				// heading 1 - the trust the school is joining
				ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel headingChangeTrustName 
					= new(ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel.HeadingTrustSchoolIsJoining,
					"/trust/joinamat/applicationselecttrust")
				{
					Status = !string.IsNullOrWhiteSpace(conversionApplication.JoinTrustDetails?.TrustName) ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				// sub question - 1a) name of the trust
				headingChangeTrustName.Sections.Add(new(ApplicationSchoolJoinAMatTrustSummarySectionViewModel.NameOfTheTrust,
						(!string.IsNullOrWhiteSpace(conversionApplication.JoinTrustDetails?.TrustName) ?
							conversionApplication.JoinTrustDetails?.TrustName :
							QuestionAndAnswerConstants.NoAnswer) ?? string.Empty
						)
				);

				// heading 2 - details
				// TODO:- change link - page not yet defined !
				ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel headingChangeTrustDetails 
					= new(ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel.HeadingChangeTrustDetails,
					"/trust/joinamat/trustconsent")
				{
					Status = conversionApplication.JoinTrustDetails != null && conversionApplication.JoinTrustDetails.ChangesToTrust.HasValue ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				var trustConsentFileNames = _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, conversionApplication.ApplicationId.ToString(), conversionApplication.ApplicationReference, FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName).Result;
				// sub questions 
				// 2a) upload evidence that the trust consents to the school joining = ApplicationSchoolJoinAMatTrustSummarySectionViewModel.TrustConsentEvidenceDoc
				headingChangeTrustDetails.Sections.Add(new(ApplicationSchoolJoinAMatTrustSummarySectionViewModel.TrustConsentEvidenceDoc,
					!string.IsNullOrWhiteSpace(conversionApplication.JoinTrustDetails?.TrustName) ? string.Join("\n", trustConsentFileNames) : QuestionAndAnswerConstants.NoAnswer));

				// 2b) will there be any changes to the governance = ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToTrustGovernance
				headingChangeTrustDetails.Sections.Add(new(
						ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToTrustGovernance,
						conversionApplication.JoinTrustDetails?.ChangesToTrust.GetStringDescription() ?? string.Empty
					)
				);

				// 2c) will there be any changes at a local level = ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToLaGovernance
				headingChangeTrustDetails.Sections.Add(new(
						ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToLaGovernance,
						conversionApplication.JoinTrustDetails?.ChangesToLaGovernance.GetStringDescription() ?? string.Empty
					)
				);

				var vm = new List<ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel> { headingChangeTrustName, headingChangeTrustDetails };

				ViewModel = vm;
			}
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}
	}
}
