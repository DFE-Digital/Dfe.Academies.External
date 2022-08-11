using Dfe.Academies.External.Web.Pages;
using Dfe.Academies.External.Web.Services;
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
internal sealed class HomeModelTests
{
    [Test]
    public void HomeModel___OnGet___Valid()
    {
        // arrange
        var mockConversionApplicationsService = new Mock<IConversionApplicationRetrievalService>();
		var mockLogger = new Mock<ILogger<HomeModel>>();

		var pageModel = SetupHomeModel(mockLogger.Object, mockConversionApplicationsService.Object);

        // act
        pageModel.OnGet();

        // assert
        Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
    }

    // TODO :- HomeModel___OnGet___InValid() i.e. API failure

    private static HomeModel SetupHomeModel(
	    ILogger<HomeModel> mockLogger, IConversionApplicationRetrievalService mockConversionApplicationsService, bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new HomeModel(mockConversionApplicationsService, mockLogger)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}

