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
public class WhatIsYourRoleModelTests
{
    [Test]
    public async Task OnGetAsync_Success()
    {
        // arrange
        var mockAcademisationCreationService = new Mock<IAcademisationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();
        var mockTempDataHelperService = new Mock<ITempDataHelperService>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object, mockTempDataHelperService.Object);

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Error.Message"], Is.EqualTo(null));
    }


    private static WhatIsYourRoleModel SetupWhatIsYourRoleModel(
        ILogger<WhatIsYourRoleModel> mockLogger, 
        IAcademisationCreationService mockAcademisationCreationService,
        ITempDataHelperService mockTempDataHelperService,
        bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new WhatIsYourRoleModel(mockLogger, mockAcademisationCreationService, mockTempDataHelperService)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}