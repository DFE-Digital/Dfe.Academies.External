using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BaseApplicationPageEditModel : BasePageEditModel
{
	public readonly IConversionApplicationCreationService ConversionApplicationCreationService;

	[BindProperty] public int ApplicationId { get; set; }

	public string NextStepPage { get; private set; }

	protected BaseApplicationPageEditModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
										IReferenceDataRetrievalService referenceDataRetrievalService,
										IConversionApplicationCreationService conversionApplicationCreationService,
										string nextStepPage)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
	{
		ConversionApplicationCreationService = conversionApplicationCreationService;
		NextStepPage = nextStepPage;
	}

	public async Task OnGetAsync(int appId)
	{
		//// on load - grab draft application from temp
		var conversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		//// MR:- Need to drop into this pages cache here ready for post / server callback !
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, conversionApplication);

		PopulateUiModel(conversionApplication);
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!RunUiValidation())
		{
			return Page();
		}

		// TODO :- API calling / data saving

		return RedirectToPage(NextStepPage, new { appId = ApplicationId });
	}

	/// <summary>
	/// take application data from API and populate UI controls
	/// </summary>
	/// <param name="conversionApplication"></param>
	public abstract void PopulateUiModel(ConversionApplication? conversionApplication);
}
