using System.ComponentModel.DataAnnotations;
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

		// TODO:- top one ??

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string ReasonVision { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string GeoAreas { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string Freedom { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string ImproveTeaching { get; set; } = string.Empty;

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
			return new Dictionary<string, dynamic>
			{
				// TODO:- what is top one ??
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
				// TODO:- what is top one ??
				// ReasonVision = conversionApplication.FormTrustDetails.;
				ReasonVision = conversionApplication.FormTrustDetails.FormTrustReasonVision;
				GeoAreas = conversionApplication.FormTrustDetails.FormTrustReasonGeoAreas;
				Freedom = conversionApplication.FormTrustDetails.FormTrustReasonFreedom;
				ImproveTeaching = conversionApplication.FormTrustDetails.FormTrustReasonImproveTeaching;
			}
		}
	}
}
