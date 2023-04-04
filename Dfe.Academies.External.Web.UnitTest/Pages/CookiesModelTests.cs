using Dfe.Academies.External.Web.Pages;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages;

[Parallelizable(ParallelScope.All)]
internal sealed class CookiesModelTests
{
	[Test]
	public void OnGetAsync___Valid___NullErrors()
	{
		// arrange
		var pageModel = SetupCookiesModel();

		// act
		pageModel.OnGet("\\");

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static CookiesModel SetupCookiesModel(bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new CookiesModel
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
