using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustGrowthSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		//// MR:- VM props to show data
		public List<ApplicationNewTrustGrowthHeadingViewModel> ViewModel { get; set; } = new();
		public ApplicationStatus ApplicationStatus {get; private set;}

		public ApplicationNewTrustGrowthSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
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

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			ApplicationStatus = conversionApplication.ApplicationStatus;

			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;

				ApplicationNewTrustGrowthHeadingViewModel heading1 = new(ApplicationNewTrustGrowthHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustPlansForGrowth")
				{
					Status = conversionApplication.FormTrustDetails.FormTrustGrowthPlansYesNo.HasValue ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				heading1.Sections.Add(new(
					ApplicationNewTrustGrowthSectionViewModel.Growth,
					conversionApplication.FormTrustDetails.FormTrustGrowthPlansYesNo.GetStringDescription()
					)
				);
				
				var vm = new List<ApplicationNewTrustGrowthHeadingViewModel> { heading1 };

				ViewModel = vm;
			}
		}
	}
}
