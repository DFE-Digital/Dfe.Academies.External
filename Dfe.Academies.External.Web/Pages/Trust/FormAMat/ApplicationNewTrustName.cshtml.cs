﻿using System.ComponentModel.DataAnnotations;
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
		[Required(ErrorMessage = "Please enter the Trust name")]
		public string? ProposedNameOfTrust { get; set; }

		public ApplicationNewTrustNameModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
	        IReferenceDataRetrievalService referenceDataRetrievalService, 
	        IConversionApplicationCreationService conversionApplicationCreationService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "ApplicationOverview")
        {
        }

		// TODO:- overrride post() ???
		// on save() -> the user is redirected to Your Application page (ApplicationOverview)

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
	        // does not apply on this page
	        return new();
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