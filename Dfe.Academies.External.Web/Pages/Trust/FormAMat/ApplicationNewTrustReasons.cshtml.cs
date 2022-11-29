using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustReasonsModel : BaseTrustFamApplicationPageEditModel
	{
		public string TrustName { get; private set; } = string.Empty;

		// MR:- VM props to capture data

		public ApplicationNewTrustReasonsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
												IReferenceDataRetrievalService referenceDataRetrievalService, 
												IConversionApplicationCreationService conversionApplicationCreationService, 
												string nextStepPage) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, nextStepPage)
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
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// TODO:-
			throw new NotImplementedException();
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				// TODO:-
				//OpeningDate = conversionApplication.FormTrustDetails.FormTrustOpeningDate.ToString();
				//TrustApproverName = conversionApplication.FormTrustDetails.TrustApproverName;
				//TrustApproverEmail = conversionApplication.FormTrustDetails.TrustApproverEmail;
			}
		}
	}
}
