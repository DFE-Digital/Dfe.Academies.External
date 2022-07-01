using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class WhatIsYourRoleModel : BasePageModel
    {
        private readonly ILogger<WhatIsYourRoleModel> _logger;
        private readonly IConversionApplicationCreationService _academisationCreationService;
        private readonly ITempDataHelperService _tempDataHelperService;
        private ConversionApplication _draftConversionApplication;
        private const string NextStepPage = "/WhatIsYourRole";

        public WhatIsYourRoleModel(ILogger<WhatIsYourRoleModel> logger,
                                    IConversionApplicationCreationService academisationCreationService,
                                    ITempDataHelperService tempDataHelperService)
        {
            _logger = logger;
            _academisationCreationService = academisationCreationService;
            _tempDataHelperService = tempDataHelperService;
        }

        [BindProperty]
        [RequiredEnum(ErrorMessage = "You must give your role at the school")]
        public SchoolRoles SchoolRole { get; set; }

        [BindProperty]
        public string OtherRoleNotListed { get; set; }

        public async Task OnGetAsync()
        {
            //// on load - grab draft application from temp
            _draftConversionApplication = _tempDataHelperService.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // error messages component consumes ViewData["Errors"]
                ViewData["Errors"] = ConvertModelDictionary();
                return Page();
            }

            if (SchoolRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
            {
                // TODO MR:- manually add a ModelState err

                // error messages component consumes ViewData["Errors"]
                ViewData["Errors"] = ConvertModelDictionary();
                return Page();
            }

            try
            {
                _draftConversionApplication.SchoolRole = SchoolRole;
                _draftConversionApplication.OtherRoleNotListed = OtherRoleNotListed;

                await _academisationCreationService.UpdateDraftApplication(_draftConversionApplication);

                // update temp store for next step
                //// TempData["draftConversionApplication"] = JsonSerializer.Serialize(_draftConversionApplication);
                _tempDataHelperService.StoreSerialisedValue("draftConversionApplication", TempData, _draftConversionApplication);

                return RedirectToPage(NextStepPage);
            }
            catch (Exception ex)
            {
                _logger.LogError("Application::WhatIsYourRoleModel::OnPostAsync::Exception - {Message}", ex.Message);
                return Page();
            }
        }
    }
}
