using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public enum SchoolRoles : int
    {
        [Description("The chair of the school's governors")]
        Chair = 1,
        [Description("A headteacher acting on their behalf")]
        Headteacher = 2,
        [Description("Something else")]
        Other = 3
    }

    public class WhatIsYourRoleModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private const string NextStepPage = "/WhatIsYourRole";

        [BindProperty]
        [RequiredEnum(ErrorMessage = "You must give your role at the school")]
        public SchoolRoles SchoolRole { get; set; }

        // TODO MR:- pop this through JS on razor if type NOT 'Other'
        [BindProperty]
        [Required]
        public string OtherRoleNotListed { get; set; }

        public WhatIsYourRoleModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

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


            // TODO MR:- call out to API to update application?
            //var response = await _applicationRepository.AddApplication(applicationTypeSelected);
            //if (!response.Success)
            //{
            //    _logger.AddError();
            //    return Page();
            //}

            return Page();
        }
    }
}
