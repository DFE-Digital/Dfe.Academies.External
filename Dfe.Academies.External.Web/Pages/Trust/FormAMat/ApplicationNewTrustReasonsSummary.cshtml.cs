using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustReasonsSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		//// MR:- VM props to show data
		// TODO:-
		
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

				// TODO:- setup VM
	        }
        }
	}
}
