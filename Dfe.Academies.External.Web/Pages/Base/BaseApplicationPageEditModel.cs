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

	// TODO MR:- public async Task OnGetAsync(int appId)

	// TODO MR:- public async Task<IActionResult> OnPostAsync()

	/// <summary>
	/// take application data from API and populate UI controls
	/// </summary>
	/// <param name="conversionApplication"></param>
	public abstract void PopulateUiModel(ConversionApplication? conversionApplication);
}
