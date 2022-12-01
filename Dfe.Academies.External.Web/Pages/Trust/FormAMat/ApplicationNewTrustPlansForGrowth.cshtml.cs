using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	public class ApplicationNewTrustPlansForGrowthModel : BaseTrustFamApplicationPageEditModel
	{
		public string TrustName { get; private set; } = string.Empty;

		// MR:- VM props to capture data

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption GrowthPlansOption { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string GrowthPlanDescription { get; set; } = string.Empty;

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string NoGrowthPlanDescription { get; set; } = string.Empty;

		public bool GrowthPlanDescriptionError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("GrowthPlanDescriptionNotEntered");
			}
		}

		public bool NoGrowthPlanDescriptionError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("NoGrowthPlanDescriptionNotEntered");
			}
		}

		public ApplicationNewTrustPlansForGrowthModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
												IReferenceDataRetrievalService referenceDataRetrievalService,
												IConversionApplicationCreationService conversionApplicationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"ApplicationNewTrustGrowthPlans")
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
			var growthPlanDescription = GrowthPlansOption == SelectOption.Yes ? GrowthPlanDescription : null;
			var noGrowthPlansDescription = GrowthPlansOption == SelectOption.No ? NoGrowthPlanDescription : null;

			return new Dictionary<string, dynamic>
			{
				{ nameof(NewTrust.FormTrustGrowthPlansYesNo), GrowthPlansOption == SelectOption.Yes },
				{ nameof(NewTrust.FormTrustPlanForGrowth), growthPlanDescription!},
				{ nameof(NewTrust.FormTrustPlansForNoGrowth), noGrowthPlansDescription!},
			};
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				GrowthPlansOption = conversionApplication.FormTrustDetails.FormTrustGrowthPlansYesNo.GetValueOrDefault() ? SelectOption.Yes : SelectOption.No;
				GrowthPlanDescription = conversionApplication.FormTrustDetails.FormTrustPlanForGrowth ?? string.Empty;
				NoGrowthPlanDescription = conversionApplication.FormTrustDetails.FormTrustPlansForNoGrowth ?? string.Empty;
			}
		}
	}
}
