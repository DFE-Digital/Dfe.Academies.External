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
		//// MR:- VM props to show data
		public List<ApplicationNewTrustGovernanceHeadingViewModel> ViewModel { get; set; } = new();

		private readonly IFileUploadService _fileUploadService;
		public ApplicationNewTrustGovernanceSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService, IFileUploadService fileUploadService) 
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
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;

				var result = _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, conversionApplication.ApplicationId.ToString(), conversionApplication.ApplicationReference, FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName).Result;
				var files = result.Aggregate(string.Empty, (current, fileName) => current + (fileName + "\n"));

				
				ApplicationNewTrustGovernanceHeadingViewModel heading1 = new(ApplicationNewTrustGovernanceHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustGovernanceStructureDetails")
				{
					Status = result.Any() ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				// TODO MR:- only upload doc, how to check?


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
