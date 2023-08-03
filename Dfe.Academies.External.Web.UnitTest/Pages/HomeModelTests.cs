using Dfe.Academies.External.Web.Pages;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages;

[Parallelizable(ParallelScope.All)]
internal sealed class HomeModelTests
{
	[Test]
	public void OnGetAsync___Valid___NullErrors()
	{
		// arrange
		var mockConversionApplicationsService = new Mock<IConversionApplicationRetrievalService>();

		var pageModel = SetupHomeModel(mockConversionApplicationsService.Object);

		// act
		pageModel.OnGet(null,null);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	// TODO MR:- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO MR:- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static HomeModel SetupHomeModel(
		IConversionApplicationRetrievalService mockConversionApplicationsService, bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new HomeModel(mockConversionApplicationsService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}

