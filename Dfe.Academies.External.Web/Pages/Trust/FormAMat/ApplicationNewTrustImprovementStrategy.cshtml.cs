using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	public class ApplicationNewTrustImprovementStrategyModel : BaseTrustFamApplicationPageEditModel
	{
		public string TrustName { get; private set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide support details")]
		public string ImprovementSupport { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide strategy details")]
		public string ImprovementStrategy { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide sponsor details")]
		public string ImprovementApprovedSponsor { get; set; } = string.Empty;

		public ApplicationNewTrustImprovementStrategyModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
												IReferenceDataRetrievalService referenceDataRetrievalService,
												IConversionApplicationService conversionApplicationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"ApplicationNewTrustImprovementStrategySummary")
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
				{ nameof(NewTrust.FormTrustImprovementSupport), ImprovementSupport },
				{ nameof(NewTrust.FormTrustImprovementStrategy), ImprovementStrategy },
				{ nameof(NewTrust.FormTrustImprovementApprovedSponsor), ImprovementApprovedSponsor },
			};
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				ImprovementSupport = conversionApplication.FormTrustDetails.FormTrustImprovementSupport ?? string.Empty;
				ImprovementStrategy = conversionApplication.FormTrustDetails.FormTrustImprovementStrategy ?? string.Empty;
				ImprovementApprovedSponsor = conversionApplication.FormTrustDetails.FormTrustImprovementApprovedSponsor ?? string.Empty;
			}
		}
	}
}
