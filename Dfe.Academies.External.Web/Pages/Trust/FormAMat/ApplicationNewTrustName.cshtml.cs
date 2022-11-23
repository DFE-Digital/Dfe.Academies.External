using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustNameModel : BaseApplicationPageEditModel
	{
		//// MR:- VM props to capture data -
		[BindProperty]
		public string? ProposedNameOfTrust { get; set; }

		public ApplicationNewTrustNameModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
	        IReferenceDataRetrievalService referenceDataRetrievalService, 
	        IConversionApplicationCreationService conversionApplicationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "nextStepPage")
        {
        }

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public override bool RunUiValidation()
        {
	        if (!ModelState.IsValid)
	        {
		        PopulateValidationMessages();
		        return false;
	        }

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
	        if (conversionApplication != null)
	        {
		        ProposedNameOfTrust = ;
	        }
        }
	}
}
