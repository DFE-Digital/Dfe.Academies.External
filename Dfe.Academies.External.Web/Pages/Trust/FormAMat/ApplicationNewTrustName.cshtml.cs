using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustNameModel : BaseTrustFamApplicationPageEditModel
	{
		//// MR:- VM props to capture data -

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string? ProposedNameOfTrust { get; set; }

		public ApplicationStatus ApplicationStatus {get; private set;}

		public ApplicationNewTrustNameModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
	        IReferenceDataRetrievalService referenceDataRetrievalService, 
	        IConversionApplicationCreationService conversionApplicationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"../../ApplicationOverview")
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

			// TODO:- "Please provide a valid response" - how ????

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
        {
			return new Dictionary<string, dynamic> { { nameof(NewTrust.FormTrustProposedNameOfTrust), ProposedNameOfTrust ?? string.Empty } };
		}

		///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
        {
			
			ApplicationStatus = conversionApplication.ApplicationStatus;
	        if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
	        {
		        ProposedNameOfTrust = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
	        }
        }
	}
}
