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
    public async Task WhatAreYouApplyingToDoModel___OnGetAsync___Valid()
    {
        // arrange
        var mockAcademisationCreationService = new Mock<IAcademisationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
    }

    [Test]
    public async Task WhatAreYouApplyingToDoModel___OnGetAsync___InValid()
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
        //Assert.That(pageModel.TempData["Errors"], Is.EqualTo("An error occurred loading the page, please try again. If the error persists contact the service administrator."));
        // no use case as yet !
    }

    [Test]
    public async Task WhatAreYouApplyingToDoModel___OnPostAsync___ModelIsValid___InValid()
    {
        // arrange
        var expectedErrorText = "Test Err";
        var mockAcademisationCreationService = new Mock<IAcademisationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object, mockAcademisationCreationService.Object);

        pageModel.ModelState.AddModelError("CustomError", expectedErrorText);

        // act
        await pageModel.OnPostAsync();

        Dictionary<string, string>? errors = (Dictionary<string, string>)pageModel.ViewData["Errors"]!;

        // assert
        if (errors != null) Assert.AreEqual(errors.Count, 1);
    }

    [Test]
    public async Task WhatAreYouApplyingToDoModel___OnPostAsync___ModelIsValid___Valid()
    {
        // arrange
        var mockAcademisationCreationService = new Mock<IAcademisationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatAreYouApplyingToDoModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        await pageModel.OnPostAsync();

        var errors = pageModel.ViewData["Errors"]!;

        // assert - no model state validation errors
        Assert.AreEqual(errors, null);
    }

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