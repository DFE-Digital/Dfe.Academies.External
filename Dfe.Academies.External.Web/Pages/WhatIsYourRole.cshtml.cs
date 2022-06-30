using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Dfe.Academies.External.Web.Pages
{
    public class WhatIsYourRoleModel : PageModel
    {
        private readonly ILogger<WhatIsYourRoleModel> _logger;
        private readonly IAcademisationCreationService _academisationCreationService;
        private ConversionApplication draftConversionApplication;
        private const string NextStepPage = "/WhatIsYourRole";

        public WhatIsYourRoleModel(ILogger<WhatIsYourRoleModel> logger, IAcademisationCreationService academisationCreationService)
        {
            _logger = logger;
            _academisationCreationService = academisationCreationService;
        }

        [BindProperty]
        [RequiredEnum(ErrorMessage = "You must give your role at the school")]
        public SchoolRoles SchoolRole { get; set; }

        [BindProperty]
        public string OtherRoleNotListed { get; set; }

        public async Task OnGetAsync()
        {
            // like on load - grab draft application from temp
            // TODO MR:- hate this code !!!!!
            draftConversionApplication = 
                JsonSerializer.Deserialize<ConversionApplication>(TempData["draftConversionApplication"]?.ToString() ?? string.Empty) ?? new ConversionApplication();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO MR:-
            // if (SchoolRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))
            // manually add a ModelState err

            if (!ModelState.IsValid)
            {
                // error messages component consumes ViewData["Errors"]
                var errorList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).FirstOrDefault()?.ToString()
                );

                ViewData["Errors"] = errorList;
                return Page();
            }

            try
            {
                draftConversionApplication.SchoolRole = SchoolRole;
                draftConversionApplication.OtherRoleNotListed = OtherRoleNotListed;

                await _academisationCreationService.UpdateDraftApplication(draftConversionApplication);

                // update temp store for next step
                TempData["draftConversionApplication"] = JsonSerializer.Serialize(draftConversionApplication);

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
