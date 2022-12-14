using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.SummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust.JoinAMat
{
	public class ApplicationSchoolJoinAMatTrustSummaryModel : BaseApplicationSummaryPageModel
	{
		private readonly IFileUploadService _fileUploadService;
		private readonly ILogger _logger;

		//// Below are props for UI display
		public ApplicationTypes ApplicationType { get; private set; }

		public string SelectedTrustName { get; private set; }

		public List<ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationSchoolJoinAMatTrustSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService, IFileUploadService fileUploadService,
			ILogger<ApplicationSchoolJoinAMatTrustSummaryModel> logger)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_fileUploadService = fileUploadService;
			_logger = logger;
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
				ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel headingChangeTrustDetails
					= new(ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel.HeadingChangeTrustDetails,
					"/trust/joinamat/applicationschooltrustconsent")
					{
						Status = conversionApplication.JoinTrustDetails != null && conversionApplication.JoinTrustDetails.ChangesToTrust.HasValue ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
					};

				List<string> trustConsentFileNames = new List<string>();
				try
				{
					trustConsentFileNames = _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, conversionApplication.ApplicationId.ToString(), conversionApplication.ApplicationReference, FileUploadConstants.JoinAMatTrustConsentFilePrefixFieldName).Result;
				}
				catch (Exception ex)
				{
					_logger.LogError("ApplicationSchoolJoinAMatTrustSummaryModel::PopulateUiModel::Exception - {Message}", ex.Message);
				}
				
				// sub questions 
				// 2a) upload evidence that the trust consents to the school joining = ApplicationSchoolJoinAMatTrustSummarySectionViewModel.TrustConsentEvidenceDoc
				headingChangeTrustDetails.Sections.Add(new(ApplicationSchoolJoinAMatTrustSummarySectionViewModel.TrustConsentEvidenceDoc,
					!string.IsNullOrWhiteSpace(conversionApplication.JoinTrustDetails?.TrustName) ? 
					string.Join("\n", trustConsentFileNames) : QuestionAndAnswerConstants.NoAnswer));

				// 2b) will there be any changes to the governance = ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToTrustGovernance
				headingChangeTrustDetails.Sections.Add(new(
						ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToTrustGovernance,
						conversionApplication.JoinTrustDetails != null && conversionApplication.JoinTrustDetails.ChangesToTrust.HasValue ? conversionApplication.JoinTrustDetails.ChangesToTrust.Value.GetDescription() : string.Empty)
				);

				// 2c) will there be any changes at a local level = ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToLaGovernance
				headingChangeTrustDetails.Sections.Add(new(
						ApplicationSchoolJoinAMatTrustSummarySectionViewModel.ChangesToLaGovernance,
						conversionApplication.JoinTrustDetails is { ChangesToLaGovernance: { } }
						? conversionApplication.JoinTrustDetails.ChangesToLaGovernance.GetStringDescription() : QuestionAndAnswerConstants.NoAnswer
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
