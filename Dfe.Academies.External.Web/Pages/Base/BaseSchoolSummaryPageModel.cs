using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Base
{
	public abstract class BaseSchoolSummaryPageModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		protected BaseSchoolSummaryPageModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		public virtual async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			// TODO:- refactor below into BasePageEditModel() for use across multiple places
			// MR:- don't need try/catch anymore as we have exception middleware
			//LoadAndStoreCachedConversionApplication();

			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

			// check user access
			try
			{
				if (draftConversionApplication != null)
				{
					base.CheckUserAccess(draftConversionApplication);
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				// re-direct to un-auth page
				return RedirectToPage("../ApplicationAccessException", new { errorMessage = ex.Message });
			}

			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
				SchoolName = selectedSchool.SchoolName;
			}

			return Page();
		}

		/// <summary>
		/// take school data from API and populate UI controls
		/// </summary>
		/// <param name="selectedSchool"></param>
		public abstract void PopulateUiModel(SchoolApplyingToConvert selectedSchool);
	}
}
