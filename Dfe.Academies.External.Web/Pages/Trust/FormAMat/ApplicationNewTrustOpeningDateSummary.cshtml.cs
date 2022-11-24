using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	/// <summary>
	/// replica of TrustOpeningDateReview.cshtml
	/// </summary>
	public class ApplicationNewTrustOpeningDateSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
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

				// TODO:- pop summary VM - same as other summary pages
				//ApplicationPreOpeningSupportGrantHeadingViewModel heading1 = new(ApplicationPreOpeningSupportGrantHeadingViewModel.Heading, // heading = 'Details'
				//	"/Trust/FormAMat/ApplicationNewTrustOpeningDate")
				//{
				//	Status = conversionApplication.FormTrustDetails.FormTrustOpeningDate.HasValue ?
				//		SchoolConversionComponentStatus.Complete
				//		: SchoolConversionComponentStatus.NotStarted
				//};

				//heading1.Sections.Add(new(
				//	ApplicationPreOpeningSupportGrantSectionViewModel.FundsSchoolOrTrust,
				//	!conversionApplication.FormTrustDetails.FormTrustOpeningDate.HasValue ?
				//		QuestionAndAnswerConstants.NoInfoAnswer :
				//		conversionApplication.FormTrustDetails.FormTrustOpeningDate.Value.ToString("dd/MM/yyyy") ?? string.Empty
				//));

				//var vm = new List<ApplicationPreOpeningSupportGrantHeadingViewModel> { heading1 };

				//ViewModel = vm;
			}
		}
	}
}
