using Dfe.Academies.External.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace Dfe.Academies.External.Web.Pages
{
    public enum ApplicationTypes : int {
        [Description("Join a multi-academy trust")]
        JoinMat=1,
        [Description("Form a new multi-academy trust")]
        FormNewMat=2,
        [Description("Form new single academy trust")]
        FormNewSingleAcademyTrust=3
    }

    public class WhatAreYouApplyingToDoModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private const string NextStepPage = "/WhatIsYourRole";

        public WhatAreYouApplyingToDoModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        [RequiredEnum(ErrorMessage = "Select an application type")]
        public ApplicationTypes ApplicationType { get; set; }
        
        public void OnGet()
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
            //var response = await _applicationRepository.AddApplication(applicationTypeSelected);
            //if (!response.Success)
            //{
            //    _logger.AddError();
            //    return Page();
            //}

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
