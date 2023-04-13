using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
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

		public string SchoolName { get; set; } = string.Empty;

		protected BaseSchoolSummaryPageModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
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
