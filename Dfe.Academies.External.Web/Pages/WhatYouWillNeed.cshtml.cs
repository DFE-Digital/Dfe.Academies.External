using Dfe.Academies.External.Web.Pages.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class WhatYouWillNeedModel : BasePageModel
    {
        public void OnGet()
        {
        }

        public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
		}
    }
}
