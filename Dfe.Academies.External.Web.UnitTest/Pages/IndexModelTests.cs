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

[Parallelizable(ParallelScope.All)]
internal sealed class IndexModelTests
{
	[Test]
	public void IndexModel___OnGet___Valid()
	{
		// arrange
		var mockLogger = new Mock<ILogger<IndexModel>>();
		var pageModel = SetupIndexModel(mockLogger.Object);

		// act
		pageModel.OnGet();

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static IndexModel SetupIndexModel(
		ILogger<IndexModel> mockLogger,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new IndexModel(mockLogger)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}