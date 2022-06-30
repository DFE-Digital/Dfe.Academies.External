using Dfe.Academies.External.Web.Logger;
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
    public void OnGet_Success()
    {
        // arrange
        var mockConversionApplicationsService = new Mock<IConversionApplicationsService>();
        var mockLogger = new Mock<ILoggerClass>();

        var pageModel = SetupHomeModel(mockLogger.Object, mockConversionApplicationsService.Object);

        // act
        pageModel.OnGet();

        // assert
        Assert.That(pageModel.TempData["Error.Message"], Is.EqualTo(null));
    }

    // TODO :- OnGet_Fail()

    private static HomeModel SetupHomeModel(
        ILoggerClass mockLogger, IConversionApplicationsService mockConversionApplicationsService, bool isAuthenticated = false)
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

