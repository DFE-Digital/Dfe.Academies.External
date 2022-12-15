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

		public string SchoolNameForDisplay { get; set; }

		public RemoveSchoolModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
								IReferenceDataRetrievalService referenceDataRetrievalService, 
								IConversionApplicationCreationService conversionApplicationCreationService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"ApplicationOverview") 
		{}

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

			// TODO MR:- call APIPI
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
			SchoolNameForDisplay = selectedSchool.SchoolName;

			// TODO MR:- retrieve school deets from referenceDataRetrievalService to display on screen??
			SchoolRegisteredAddress = "the shed";
		}
	}
}
