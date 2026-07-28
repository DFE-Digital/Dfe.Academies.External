using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustGovernanceSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		private readonly ILogger<ApplicationNewTrustGovernanceSummaryModel> _logger;
		private readonly ISharePointService _sharepoint;
		
		//// MR:- VM props to show data
		public List<ApplicationNewTrustGovernanceHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationStatus ApplicationStatus {get; private set;}


		public ApplicationNewTrustGovernanceSummaryModel(
			IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService, 
			ISharePointService sharepointService,
			ILogger<ApplicationNewTrustGovernanceSummaryModel> logger) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_sharepoint = sharepointService;
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
		public override async Task PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication == null || conversionApplication.FormTrustDetails == null)
			{
				return;
			}

			ApplicationStatus = conversionApplication.ApplicationStatus;
			TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
			Guid entityId = conversionApplication.EntityId;
			string reference = conversionApplication.ApplicationReference;
			
			List<string> result = [];
			string trustFiles = string.Empty;

			try
			{
				string folder = FileUploadConstants.FormatSharepointApplicationDirectory(conversionApplication.ApplicationReference, conversionApplication.EntityId.ToString());
				var files = await _sharepoint.ListFilesAsync(folder);
				result = files.Where(file => file.Name.StartsWith(FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName)).Select(file => file.Name).ToList();
				trustFiles = string.Join("\n", result);
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
					trustFiles :
					QuestionAndAnswerConstants.NoInfoAnswer));

			var vm = new List<ApplicationNewTrustGovernanceHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
