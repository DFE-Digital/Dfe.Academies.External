using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class DeclarationModel : BasePageEditModel
	{
	    private readonly IConversionApplicationCreationService _academisationCreationService;

	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

	    //// MR:- VM props to capture declaration data - only 2
		[BindProperty]
		public bool SchoolDeclarationTeacherChair { get; set; }

		[BindProperty]
		public bool SchoolDeclarationBodyAgree { get; set; }

		public DeclarationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				return Page();
			}

			// according to dan both values have to be true!
			if (!SchoolDeclarationTeacherChair || !SchoolDeclarationBodyAgree)
			{
				ModelState.AddModelError("Declarationconfirmation", "You must confirm the declaration");
				PopulateValidationMessages();
				return Page();
			}

			//// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.DeclarationIAmTheChairOrHeadteacher), SchoolDeclarationTeacherChair },
				{ nameof(SchoolApplyingToConvert.DeclarationBodyAgree), SchoolDeclarationBodyAgree }
			};

			await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage("DeclarationSummary", new { appId = ApplicationId, urn = Urn });
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// TODO:- move code to here !!
			throw new NotImplementedException();
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

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

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
