using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	/// <summary>
	/// replica of TrustOpeningDateReview.cshtml
	/// </summary>
	public class ApplicationNewTrustOpeningDateSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		//// MR:- VM props to show data
		public List<ApplicationNewTrustOpeningDateHeadingViewModel> ViewModel { get; set; } = new();

		public ApplicationNewTrustOpeningDateSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
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

        ///<inheritdoc/>
		public override void PopulateUiModel(ConversionApplication? conversionApplication)
        {
	        if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
	        {
		        TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;

				ApplicationNewTrustOpeningDateHeadingViewModel heading1 = new(ApplicationNewTrustOpeningDateHeadingViewModel.Heading, // heading = 'Details'
					"/Trust/FormAMat/ApplicationNewTrustOpeningDate")
				{
					Status = conversionApplication.FormTrustDetails.FormTrustOpeningDate.HasValue ?
						SchoolConversionComponentStatus.Complete
						: SchoolConversionComponentStatus.NotStarted
				};

				heading1.Sections.Add(new(
					ApplicationNewTrustOpeningDateSectionViewModel.OpeningDate,
					!conversionApplication.FormTrustDetails.FormTrustOpeningDate.HasValue ?
						QuestionAndAnswerConstants.NoInfoAnswer :
						conversionApplication.FormTrustDetails.FormTrustOpeningDate.Value.ToString("dd/MM/yyyy")
				));

				heading1.Sections.Add(new(
					ApplicationNewTrustOpeningDateSectionViewModel.ApproverFullname,
					(string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.TrustApproverName) ?
						QuestionAndAnswerConstants.NoInfoAnswer :
						conversionApplication.FormTrustDetails.TrustApproverName)
				));

				heading1.Sections.Add(new(
					ApplicationNewTrustOpeningDateSectionViewModel.ApproverEmail,
					(string.IsNullOrWhiteSpace(conversionApplication.FormTrustDetails.TrustApproverEmail) ?
						QuestionAndAnswerConstants.NoInfoAnswer :
						conversionApplication.FormTrustDetails.TrustApproverEmail)
				));

				var vm = new List<ApplicationNewTrustOpeningDateHeadingViewModel> { heading1 };

				ViewModel = vm;
			}
		}
	}
}
