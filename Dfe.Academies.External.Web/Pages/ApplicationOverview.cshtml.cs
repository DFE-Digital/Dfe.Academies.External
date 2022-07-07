using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages;

public class ApplicationOverviewModel : BasePageModel
{
    private readonly ILogger<ApplicationOverviewModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;
    private ConversionApplication _draftConversionApplication;

    public ApplicationOverviewModel(ILogger<ApplicationOverviewModel> logger,
        IConversionApplicationCreationService academisationCreationService)
    {
        _logger = logger;
        _academisationCreationService = academisationCreationService;
    }

    public async Task OnGetAsync()
    {
        //// on load - grab draft application from temp
        _draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return Page();
    }

    public override void PopulateValidationMessages()
    {
        throw new NotImplementedException();
    }
}

