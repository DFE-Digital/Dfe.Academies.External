using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
    public class ApplicationNewTrustSummaryModel : BaseTrustFamApplicationSummaryPageModel
	{
		public ApplicationTypes ApplicationType { get; private set; }

		public string SelectedTrustName { get; private set; }
		
		public List<ApplicationComponentViewModel> FormAMaTComponents { get; set; }

		public TrustComponentViewModel FormAMatTrustComponents { get; set; } = new();
		
		public ApplicationNewTrustSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
												IReferenceDataRetrievalService referenceDataRetrievalService) 
	        : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
        {
        }

		public override async Task<ActionResult> OnGetAsync(int appId)
		{
			LoadAndStoreCachedConversionApplication();
			ApplicationId = appId;
			var application = await ConversionApplicationRetrievalService.GetApplication(appId);
			if (application != null)
			{
				TrustName = application.TrustName;

				this.FormAMaTComponents = await ConversionApplicationRetrievalService.GetFormAMatTrustComponents(appId);
				PopulateUiModel(application);
			}

			return Page();
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
				ApplicationType = conversionApplication.ApplicationType;
				
				TrustComponentViewModel componentsVm = new()
				{
					ApplicationId = conversionApplication.ApplicationId,
					TrustComponents = FormAMaTComponents.Select(c =>
						new ApplicationComponentViewModel(name: c.Name,
							uri: UriFormatter.SetFormAMatComponentUriFromName(c.Name), c.Status)).ToList()
				};

				FormAMatTrustComponents = componentsVm;
			}
        }
	}
}
