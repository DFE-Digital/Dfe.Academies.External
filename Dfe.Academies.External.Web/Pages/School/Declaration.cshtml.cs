using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class DeclarationModel : BaseSchoolPageEditModel
	{
	    // MR:- VM props to capture declaration data - only 2
		[BindProperty]
		public bool SchoolDeclarationTeacherChair { get; set; }

		[BindProperty]
		public bool SchoolDeclarationBodyAgree { get; set; }

		public DeclarationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "DeclarationSummary")
		{}
		
		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			// according to dan both values have to be true!
			if (!SchoolDeclarationTeacherChair || !SchoolDeclarationBodyAgree)
			{
				ModelState.AddModelError("Declarationconfirmation", "You must confirm the declaration");
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
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.DeclarationIAmTheChairOrHeadteacher), SchoolDeclarationTeacherChair },
				{ nameof(SchoolApplyingToConvert.DeclarationBodyAgree), SchoolDeclarationBodyAgree }
			};
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			if (selectedSchool.DeclarationIAmTheChairOrHeadteacher != null)
			{
				SchoolDeclarationTeacherChair = selectedSchool.DeclarationIAmTheChairOrHeadteacher.Value;
			}

			if (selectedSchool.DeclarationBodyAgree != null)
			{
				SchoolDeclarationBodyAgree = selectedSchool.DeclarationBodyAgree.Value;
			}

			// TODO:- DeclarationSignedByName = ???;
		}
	}
}
