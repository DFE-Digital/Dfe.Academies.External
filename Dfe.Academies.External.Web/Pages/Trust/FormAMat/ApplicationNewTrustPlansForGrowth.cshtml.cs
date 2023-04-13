using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
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
		[Required(ErrorMessage = "You must select an option")]
		public SelectOption GrowthPlansOption { get; set; }

		[BindProperty]
		public string? GrowthPlanDescription { get; set; } = string.Empty;

		[BindProperty]
		public string? NoGrowthPlanDescription { get; set; } = string.Empty;

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
				"ApplicationNewTrustGrowthSummary")
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
			ModelState.Clear();

			if (GrowthPlansOption == SelectOption.Yes && string.IsNullOrWhiteSpace(GrowthPlanDescription))
			{
				ModelState.AddModelError("GrowthPlanDescriptionNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (GrowthPlansOption == SelectOption.No && string.IsNullOrWhiteSpace(NoGrowthPlanDescription))
			{
				ModelState.AddModelError("NoGrowthPlanDescriptionNotEntered", "You must provide details");
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
