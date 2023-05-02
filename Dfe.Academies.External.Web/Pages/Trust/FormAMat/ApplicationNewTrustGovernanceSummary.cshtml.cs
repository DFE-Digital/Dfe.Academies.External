using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustGovernanceSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		private readonly ILogger<ApplicationNewTrustGovernanceSummaryModel> _logger;

		//// MR:- VM props to show data
		public List<ApplicationNewTrustGovernanceHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationStatus ApplicationStatus {get; private set;}

		private readonly IFileUploadService _fileUploadService;
		public ApplicationNewTrustGovernanceSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService, IFileUploadService fileUploadService,
			ILogger<ApplicationNewTrustGovernanceSummaryModel> logger) 
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

		///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			ApplicationStatus = conversionApplication.ApplicationStatus;
			
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				List<string> result = new List<string>();
				string files = string.Empty;

				try
				{
					result = _fileUploadService.GetFiles(FileUploadConstants.TopLevelApplicationFolderName, conversionApplication.EntityId.ToString(), conversionApplication.ApplicationReference, FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName).Result;
					files = result.Aggregate(string.Empty, (current, fileName) => current + (fileName + "\n"));
				}
				catch (Exception ex)
				{
					_logger.LogError("ApplicationNewTrustGovernanceSummaryModel::PopulateUiModel::Exception - {Message}", ex.Message);
				}
								
				ApplicationNewTrustGovernanceHeadingViewModel heading1 = new(ApplicationNewTrustGovernanceHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustGovernanceStructureDetails")
				{
					Status = result.Any() ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				heading1.Sections.Add(new(
					ApplicationNewTrustGovernanceSectionViewModel.StructureDocument,
					result.Any() ?
						files :
						QuestionAndAnswerConstants.NoInfoAnswer));

				var vm = new List<ApplicationNewTrustGovernanceHeadingViewModel> { heading1 };

				ViewModel = vm;
			}
		}
	}
}
