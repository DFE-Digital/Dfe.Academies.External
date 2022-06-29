using System.Threading.Tasks;
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
public class WhatAreYouApplyingToDoModelTests
{
    [Test]
    public async Task WhenOnGetAsync_MissingCaseUrn_ThrowsException()
    {
        // arrange
        var mockAcademisationCreationService = new Mock<IAcademisationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object, mockAcademisationCreationService.Object);

        var routeData = pageModel.RouteData.Values;
        routeData.Add("id", "");

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Error.Message"], Is.EqualTo("An error occurred loading the page, please try again. If the error persists contact the service administrator."));
    }

    // OnPost Success

    // OnPost Fail

    private static WhatAreYouApplyingToDoModel SetupWhatAreYouApplyingToDoModel(
        ILogger<WhatAreYouApplyingToDoModel> mockLogger, IAcademisationCreationService mockAcademisationCreationService, bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new WhatAreYouApplyingToDoModel(mockLogger, mockAcademisationCreationService)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}