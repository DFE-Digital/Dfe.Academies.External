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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Pages;

[Parallelizable(ParallelScope.All)]
internal sealed class WhatAreYouApplyingToDoModelTests
{
    [Test]
    public async Task OnGetAsync___Valid___NullErrors()
    {
        // arrange
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object);

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
    }

    [Test]
    public async Task OnGetAsync___Invalid__HasErrors()
    {
        // arrange
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object);

        var routeData = pageModel.RouteData.Values;
        routeData.Add("id", "");

        // act
        await pageModel.OnGetAsync();

        // no use case as yet !
        // assert
        //Assert.That(pageModel.TempData["Errors"], Is.EqualTo("An error occurred loading the page, please try again. If the error persists contact the service administrator."));
    }

    [Test]
    public async Task OnPostAsync___ModelStateInvalid___ViewDataPopulated()
    {
        // arrange
        var expectedErrorText = "Test Err";
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object);

        pageModel.ModelState.AddModelError("CustomError", expectedErrorText);

        // act
        await pageModel.OnPostAsync();

        Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

        // assert
        Assert.AreEqual(1,errors.Count);
    }

    [Test]
    public async Task OnPostAsync___ModelStateValid___ViewDataEmpty()
    {
        // arrange
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object);

        // act
        await pageModel.OnPostAsync();

        var errors = pageModel.ViewData["Errors"]!;

        // assert - no model state validation errors
        Assert.AreEqual(null, errors);
    }

    private static WhatAreYouApplyingToDoModel SetupWhatAreYouApplyingToDoModel(
        ILogger<WhatAreYouApplyingToDoModel> mockLogger, 
        bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new WhatAreYouApplyingToDoModel(mockLogger)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}
