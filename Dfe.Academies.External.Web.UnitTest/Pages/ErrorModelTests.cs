using Dfe.Academies.External.Web.Pages;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages;

public class ErrorModelTests
{
	[Test]
	public void ErrorModel___OnGetAsync___Valid()
	{
		// arrange
		var mockLogger = new Mock<ILogger<ErrorModel>>();

		// act
		var pageModel = SetupErrorModel(mockLogger.Object);

		// act
		pageModel.OnGet();

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ErrorModel SetupErrorModel(
		ILogger<ErrorModel> mockLogger,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ErrorModel(mockLogger)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}