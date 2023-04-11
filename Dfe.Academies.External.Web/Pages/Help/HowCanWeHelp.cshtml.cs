using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static GovUk.Frontend.AspNetCore.ComponentDefaults;

using System.Dynamic;
using System.Security.Claims;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Help
{
    public class HowCanWeHelpModel :BasePageModel
    {

       [BindProperty]
       [RequiredEnum(ErrorMessage = "You must choose an option")]
       public HelpTypes HelpType {get;set;}

       private const string HelpPage = "Help";

       public IActionResult OnPost()
       {
            if (!RunUiValidation())
			{
				return Page();
			}
           var helpType = Request.Form["HelpType"];
           return RedirectToPage(HelpPage, new {HelpType = helpType});
       }

       public bool RunUiValidation()
		{
			
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				
				return false;
			}

            return true;

        }

        public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

    }

    
}
