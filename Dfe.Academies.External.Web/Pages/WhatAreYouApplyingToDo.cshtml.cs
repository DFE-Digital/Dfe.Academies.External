using Dfe.Academies.External.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Pages
{
    public class WhatAreYouApplyingToDoModel : PageModel
    {
        private readonly ILogger<WhatAreYouApplyingToDoModel> _logger;
        private readonly IAcademisationCreationService _academisationCreationService;
        private const string NextStepPage = "/WhatIsYourRole";

        public WhatAreYouApplyingToDoModel(ILogger<WhatAreYouApplyingToDoModel> logger, IAcademisationCreationService academisationCreationService)
        {
            _logger = logger;
            _academisationCreationService = academisationCreationService;
        }

        [BindProperty]
        [RequiredEnum(ErrorMessage = "Select an application type")]
        public ApplicationTypes ApplicationType { get; set; }

        public async Task OnGetAsync()
        {
            // like on load
        }

        public IActionResult OnPostAsync()
        {
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

            var applicationTypeSelected = ApplicationType;

            // TODO MR:- call out to API to create stub application?
            try
            {
                var newApplication = _academisationCreationService.CreateNewApplication(applicationTypeSelected);
                // TODO MR:- plop newApplication.Id somewhere??
            }
            catch (Exception ex)
            {
                _logger.LogError("Application::WhatAreYouApplyingToDoModel::OnPostAsync::Exception - {Message}", ex.Message);
                return Page();
            }
            
            switch (applicationTypeSelected)
            {
                case ApplicationTypes.JoinMat:
                    TempData["applicationTypeSelected"] = applicationTypeSelected;
                    return RedirectToPage(NextStepPage);
                case ApplicationTypes.FormNewMat:
                    TempData["applicationTypeSelected"] = applicationTypeSelected;
                    return RedirectToPage(NextStepPage);
                case ApplicationTypes.FormNewSingleAcademyTrust:
                    TempData["applicationTypeSelected"] = applicationTypeSelected;
                    return RedirectToPage(NextStepPage);
            }

            return Page();
        }
    }
}
