using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class RemoveSchoolSelectionModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must choose an option")]
		public int SelectedUrn { get; set; }
		
		public Dictionary<int, string> ApplicationSchools { get; private set; }

		private string NextStepPage { get; set; } = "/RemoveSchool";

		public RemoveSchoolSelectionModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
			IReferenceDataRetrievalService referenceDataRetrievalService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			ApplicationSchools = new Dictionary<int, string>();
		}

		public async Task<ActionResult> OnGetAsync(int appId)
		{
			//// on load - grab draft application from temp
			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			ApplicationId = appId;

			PopulateUiModel(draftConversionApplication);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			//// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey,
					TempData) ?? new ConversionApplication();

			if (!RunUiValidation())
			{
				PopulateUiModel(draftConversionApplication); // otherwise lose app schools !
				return Page();
			}

			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = SelectedUrn });
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}

		public void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null)
			{
				foreach (var school in conversionApplication.Schools)
				{
					ApplicationSchools.Add(school.URN, school.SchoolName);
				}
			}
		}
	}
}
