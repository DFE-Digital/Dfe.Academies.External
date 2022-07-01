using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

public class ApplicationOverviewModel : BasePageModel
{
    private readonly ILogger<ApplicationOverviewModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;
    private readonly ITempDataHelperService _tempDataHelperService;
    private ConversionApplication _draftConversionApplication;
    private const string NextStepPage = "/ApplicationOverview";

    public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger,
        IConversionApplicationCreationService academisationCreationService,
        ITempDataHelperService tempDataHelperService)
    {
        _logger = logger;
        _academisationCreationService = academisationCreationService;
        _tempDataHelperService = tempDataHelperService;
    }

    public async Task OnGetAsync()
    {
        //// on load - grab draft application from temp
        _draftConversionApplication = _tempDataHelperService.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return Page();
    }
}

