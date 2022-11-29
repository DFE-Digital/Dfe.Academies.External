using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustReasonsSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		//// MR:- VM props to show data
		public List<ApplicationNewTrustReasonsHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationNewTrustReasonsSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
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

		        ApplicationNewTrustReasonsHeadingViewModel heading1 = new(ApplicationNewTrustReasonsHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustReasons")
		        {
			        Status = !string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustReasonForming) ?
				        SchoolConversionComponentStatus.Complete
				        : SchoolConversionComponentStatus.NotStarted
		        };

		        heading1.Sections.Add(new(
			        ApplicationNewTrustReasonsSectionViewModel.WhyForming,
					(!string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustReasonForming) ?
						conversionApplication.FormTrustDetails.FormTrustReasonForming :
						QuestionAndAnswerConstants.NoInfoAnswer)
				));

		        heading1.Sections.Add(new(
			        ApplicationNewTrustReasonsSectionViewModel.WhyForming,
			        (!string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.FormTrustReasonForming) ?
				        conversionApplication.FormTrustDetails.FormTrustReasonForming :
				        QuestionAndAnswerConstants.NoInfoAnswer)
		        ));

				// TODO:- setup VM - 5 sub sections - 1 header

				var vm = new List<ApplicationNewTrustReasonsHeadingViewModel> { heading1 };

				ViewModel = vm;
			}
        }
	}
}
