using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Base
{
	public abstract class BaseApplicationSummaryPageModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		protected BaseApplicationSummaryPageModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		public virtual async Task<ActionResult> OnGetAsync(int urn, int appId)
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
			Urn = urn;

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
