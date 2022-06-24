using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public void OnGet()
        {
        }

        // TODO MR:- 1 props to bind to - enum - with 3 values
        [Required]
        public ApplicationTypes ApplicationType { get; set; }

        ////joinMAT
        //public bool JoinMAT { get; set; }
        
        ////formNewMAT
        //public bool FormNewMAT { get; set; }
        ////formNewSingleAcademyTrust
        //public bool FormNewSingleAcademyTrust { get; set; }
    }
}
