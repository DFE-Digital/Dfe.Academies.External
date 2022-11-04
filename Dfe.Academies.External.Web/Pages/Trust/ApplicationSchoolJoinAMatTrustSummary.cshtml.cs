using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages.Trust
{
    public class ApplicationSchoolJoinAMatTrustSummaryModel : BaseApplicationPageEditModel
	{
		//// Below are props for UI display
		public ApplicationTypes ApplicationType { get; private set; }

		public string SelectedTrustName { get; private set; }

		// TODO:- summary view model - for binding
		// public List<ApplicationSchoolJoinAMatTrustSummaryViewModel> ViewModel { get; set; } = new();

		public ApplicationSchoolJoinAMatTrustSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService conversionApplicationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				conversionApplicationCreationService, "/ApplicationOverview")
		{
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				// heading 1 - the trust the school is joining
				// sub question - 1a) name of the trust

				//ApplicationSchoolJoinAMatTrustSummaryViewModel heading1 = new(ApplicationSchoolJoinAMatTrustSummaryViewModel.Heading,
				//	"/trust/applicationselecttrust")
				//{
				//	Status = conversionApplication.JoinTrustDetails.TrustName.HasValue ?
				//		SchoolConversionComponentStatus.Complete
				//		: SchoolConversionComponentStatus.NotStarted
				//};

				// heading 2 - details - change - page not yet defined !
				// sub questions 
				// 2a) upload evidence that the trust consents to the school joining 

				// 2b) will there be any changes to the governance

				// 2c) will there be any changes at a local level

				//var vm = new List<ApplicationSchoolJoinAMatTrustSummaryViewModel> { heading1, heading2 };

				//ViewModel = vm;
			}
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
	}
}
