using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustImprovementStrategySummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		//// MR:- VM props to show data
		public List<ApplicationNewTrustImprovementStrategyHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationNewTrustImprovementStrategySummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
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

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;

				ApplicationNewTrustImprovementStrategyHeadingViewModel heading1 = new(ApplicationNewTrustImprovementStrategyHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustImprovementStrategy")
				{
					Status = !string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustImprovementSupport) ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				heading1.Sections.Add(new(
					ApplicationNewTrustImprovementStrategySectionViewModel.Support,
					(!string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustImprovementSupport) ?
						conversionApplication.FormTrustDetails.FormTrustImprovementSupport :
						QuestionAndAnswerConstants.NoInfoAnswer))
				);

				heading1.Sections.Add(new(
					ApplicationNewTrustImprovementStrategySectionViewModel.Strategy,
					(!string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustImprovementStrategy) ?
						conversionApplication.FormTrustDetails.FormTrustImprovementStrategy :
						QuestionAndAnswerConstants.NoInfoAnswer))
				);

				heading1.Sections.Add(new(
					ApplicationNewTrustImprovementStrategySectionViewModel.ApprovedSponsor,
					(!string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustImprovementApprovedSponsor)
						? conversionApplication.FormTrustDetails.FormTrustImprovementApprovedSponsor
						: QuestionAndAnswerConstants.NoInfoAnswer))
				);

				var vm = new List<ApplicationNewTrustImprovementStrategyHeadingViewModel> { heading1 };

				ViewModel = vm;
			}
		}
	}
}
