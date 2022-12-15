using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class RemoveSchoolModel : BaseSchoolPageEditModel
	{
		public RemoveSchoolModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
								IReferenceDataRetrievalService referenceDataRetrievalService, 
								IConversionApplicationCreationService conversionApplicationCreationService, string nextStepPage) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, nextStepPage)
		{
		}

		/// <summary>
		/// Override as not sending user to another page after submit, leaving them here with confirmation message !
		/// </summary>
		/// <returns></returns>
		public override async Task<IActionResult> OnPostAsync()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			// TODO MR:- call APIPI

			// TODO MR:- set flag to display confirmation - same as add contriibutor !!

			return Page();
		}

		public override bool RunUiValidation()
		{
			// TODO MR:- doesn't apply because just pressing submit button?
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
		
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			// TODO MR:- retrieve school deets from referenceDataRetrievalService to display on screen??
		}
	}
}
