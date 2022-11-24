using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Base
{
	public abstract class BaseTrustFamApplicationSummaryPageModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		public string TrustName { get; set; } = string.Empty;

		protected BaseTrustFamApplicationSummaryPageModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		public virtual async Task<ActionResult> OnGetAsync(int appId)
		{
			// MR:- don't need try/catch anymore as we have exception middleware
			LoadAndStoreCachedConversionApplication();

			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("../ApplicationAccessException");
			}

			ApplicationId = appId;
			
			var conversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			PopulateUiModel(conversionApplication);

			return Page();
		}

		/// <summary>
		/// take application data from API and populate UI controls
		/// </summary>
		/// <param name="conversionApplication"></param>
		public abstract void PopulateUiModel(ConversionApplication? conversionApplication);
	}
}
