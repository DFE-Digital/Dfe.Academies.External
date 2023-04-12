using Microsoft.AspNetCore.Mvc;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Pages.Base;


namespace Dfe.Academies.External.Web.Pages.Help
{
    public class HowCanWeHelpModel :BasePageModel
    {

       [BindProperty]
       [RequiredEnum(ErrorMessage = "You must choose an option")]
       public HelpTypes HelpType {get;set;}

       public IActionResult OnPost()
       {
            if (!RunUiValidation())
			{
				return Page();
			}
        
           return RedirectToPage(HelpType.ToString());
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
