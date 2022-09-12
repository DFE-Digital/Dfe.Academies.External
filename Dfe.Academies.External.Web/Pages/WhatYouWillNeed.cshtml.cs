using Dfe.Academies.External.Web.Pages.Base;

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
