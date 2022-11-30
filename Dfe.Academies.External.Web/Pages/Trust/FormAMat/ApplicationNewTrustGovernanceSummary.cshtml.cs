using Dfe.Academies.External.Web.Enums;
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

		public ApplicationNewTrustGovernanceSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
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

				ApplicationNewTrustGovernanceHeadingViewModel heading1 = new(ApplicationNewTrustGovernanceHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustGovernance")
				{
					Status = !string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustReasonForming) ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				// TODO MR:- only upload doc, how to check?

				heading1.Sections.Add(new(
					ApplicationNewTrustGovernanceSectionViewModel.StructureDocument,
					(!string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustReasonForming) ?
						conversionApplication.FormTrustDetails.FormTrustReasonForming :
						QuestionAndAnswerConstants.NoInfoAnswer))
				);

				var vm = new List<ApplicationNewTrustGovernanceHeadingViewModel> { heading1 };

				ViewModel = vm;
			}
		}
	}
}
