using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustNameModel : BaseTrustFAMApplicationPageEditModel
	{
		//// MR:- VM props to capture data -

		[BindProperty]
		[Required(ErrorMessage = "Please enter the Trust name")]
		public string? ProposedNameOfTrust { get; set; }

		public ApplicationNewTrustNameModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
	        IReferenceDataRetrievalService referenceDataRetrievalService, 
	        IConversionApplicationCreationService conversionApplicationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "ApplicationOverview")
        {
        }

		// using base Get() && Post() funcs as this is a pretty simple page !

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

			// TODO:- "Please provide a valid response" ????

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
        {
			return new Dictionary<string, dynamic> { { nameof(NewTrust.ProposedTrustName), ProposedNameOfTrust ?? string.Empty } };
		}

		///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
        {
	        if (conversionApplication != null && conversionApplication.FormATrust != null)
	        {
		        ProposedNameOfTrust = conversionApplication.FormATrust.ProposedTrustName;
	        }
        }
	}
}
