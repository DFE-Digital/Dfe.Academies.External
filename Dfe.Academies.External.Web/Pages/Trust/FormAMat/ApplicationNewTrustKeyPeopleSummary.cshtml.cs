using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustKeyPersonSummaryModel : BaseTrustFamApplicationSummaryPageModel
    {
	    [BindProperty]
	    public List<NewTrustKeyPerson> NewTrustKeyPeople { get; set; } = new();
		public ApplicationNewTrustKeyPersonSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
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

		        NewTrustKeyPeople = conversionApplication.FormTrustDetails.KeyPeople;
	        }
        }
	}
}
