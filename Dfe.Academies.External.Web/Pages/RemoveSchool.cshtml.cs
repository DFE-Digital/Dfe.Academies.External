using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class RemoveSchoolModel : BaseSchoolPageEditModel
	{
		[BindProperty]
		public bool ShowConfirmationBox { get; set; }

		public string? SchoolRegisteredAddress { get; set; }

		public string SchoolNameForDisplay { get; set; } = string.Empty;

		public RemoveSchoolModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
								IReferenceDataRetrievalService referenceDataRetrievalService, 
								IConversionApplicationCreationService conversionApplicationCreationService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"ApplicationOverview") 
		{}

		/// <summary>
		/// had to override because we need to await PopulateUiModel()
		/// </summary>
		/// <param name="urn"></param>
		/// <param name="appId"></param>
		/// <returns></returns>
		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
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
				await PopulateUiModelAsync(selectedSchool);
			}

			return Page();
		}

		/// <summary>
		/// Override as not sending user to another page after submit, leaving them here with confirmation message !
		/// </summary>
		/// <returns></returns>
		public override async Task<IActionResult> OnPostAsync()
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			if (!RunUiValidation())
			{
				return Page();
			}

			// TODO MR:- call API func
			// await ConversionApplicationCreationService.RemoveSchoolFromApplication(ApplicationId, Urn);

			// MR:- set flag to display confirmation - same as add contributor !!
			ShowConfirmationBox = true;

			// MR:- lose SchoolName on PostBack
			var selectedSchool = draftConversionApplication.Schools.FirstOrDefault(school => school.URN == Urn);
			SchoolNameForDisplay = selectedSchool?.SchoolName ?? string.Empty;

			return Page();
		}

		public override bool RunUiValidation()
		{
			// MR:- doesn't apply because just pressing submit button?
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			throw new NotImplementedException();
		}

		private async Task PopulateUiModelAsync(SchoolApplyingToConvert selectedSchool)
		{
			SchoolNameForDisplay = selectedSchool.SchoolName;
			var selectedSchoolFullDetails = await this.ReferenceDataRetrievalService.GetSchool(Urn);

			SchoolRegisteredAddress = selectedSchoolFullDetails.Address.FullAddress;
		}
	}
}
