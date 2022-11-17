using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class RemoveAContributorConfirmationModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		//// TODO MR:- VM props to capture data -
		public string ContributorName { get; private set; } = string.Empty;

		public RemoveAContributorConfirmationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
													IReferenceDataRetrievalService referenceDataRetrievalService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
        }

		// TODO:- might need to override - passing in app ID via query string? passing in contributor id?
        public void OnGet()
        {
			// TODO:-  need to grab contrbutor name to show on UI
        }

		public override void PopulateValidationMessages()
        {
	        throw new NotImplementedException();
        }

        public override bool RunUiValidation()
        {
			// TODO:- not sure this applies
	        throw new NotImplementedException();
        }

        public override Dictionary<string, dynamic> PopulateUpdateDictionary()
        {
	        throw new NotImplementedException();
        }
	}
}
