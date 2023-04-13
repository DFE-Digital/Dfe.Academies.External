using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustReasonsModel : BaseTrustFamApplicationPageEditModel
	{
		public string TrustName { get; private set; } = string.Empty;

		// MR:- VM props to capture data

		[BindProperty]
		[Required(ErrorMessage = "You must provide reason for forming details")]
		public string ReasonForming { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide vision details")]
		public string ReasonVision { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide geographical details")]
		public string GeoAreas { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide freedom details")]
		public string Freedom { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide teaching improvement details")]
		public string ImproveTeaching { get; set; } = string.Empty;

		public ApplicationNewTrustReasonsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
												IReferenceDataRetrievalService referenceDataRetrievalService, 
												IConversionApplicationCreationService conversionApplicationCreationService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, 
				"ApplicationNewTrustReasonsSummary")
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
			return new Dictionary<string, dynamic>
			{
				{ nameof(NewTrust.FormTrustReasonForming), ReasonForming },
				{ nameof(NewTrust.FormTrustReasonVision), ReasonVision },
				{ nameof(NewTrust.FormTrustReasonGeoAreas), GeoAreas },
				{ nameof(NewTrust.FormTrustReasonFreedom), Freedom },
				{ nameof(NewTrust.FormTrustReasonImproveTeaching), ImproveTeaching },
			};
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				ReasonForming = conversionApplication.FormTrustDetails.FormTrustReasonForming ?? string.Empty;
				ReasonVision = conversionApplication.FormTrustDetails.FormTrustReasonVision ?? string.Empty;
				GeoAreas = conversionApplication.FormTrustDetails.FormTrustReasonGeoAreas ?? string.Empty;
				Freedom = conversionApplication.FormTrustDetails.FormTrustReasonFreedom ?? string.Empty;
				ImproveTeaching = conversionApplication.FormTrustDetails.FormTrustReasonImproveTeaching ?? string.Empty;
			}
		}
	}
}
