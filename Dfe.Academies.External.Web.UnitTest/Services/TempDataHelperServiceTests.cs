using Dfe.Academies.External.Web.Models;
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

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
public class TempDataHelperServiceTests
{
    [Test]
    public void TempDataHelperService___GetNonSerialisedValue___Success()
    {
        // arrange
        var expected = int.MaxValue.ToString();
        var storageKey = "TempDataHelperService___GetNonSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        TempDataHelper.StoreNonSerialisedValue(storageKey, pageModel.TempData, expected);

        // assert - grab value back to see if it's stored
        var storedValue = TempDataHelper.GetNonSerialisedValue(storageKey, pageModel.TempData);

        Assert.AreEqual(storedValue, expected);
    }

    [Test]
    public void TempDataHelperService___StoreNonSerialisedValue___Success()
    {
        // arrange
        var expected = int.MaxValue.ToString();
        var storageKey = "TempDataHelperService___StoreNonSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        TempDataHelper.StoreNonSerialisedValue(storageKey, pageModel.TempData, expected);

        // assert - grab value back to see if it's stored
        var storedValue = TempDataHelper.GetNonSerialisedValue(storageKey, pageModel.TempData);

        Assert.AreEqual(storedValue, expected);
    }

    [Test]
    public void TempDataHelperService___GetSerialisedValue___Success()
    {
        // arrange
        var storageKey = "TempDataHelperService___GetSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplication();

        // act
        TempDataHelper.StoreSerialisedValue(storageKey, pageModel.TempData, conversionApplication);

        // assert - grab value back to see if it's stored
        var storedValue = TempDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.TempData);

        Assert.AreEqual(conversionApplication.Id, storedValue.Id);
        Assert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
        Assert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
        Assert.AreEqual(conversionApplication.Application, storedValue.Application);
        Assert.AreEqual(conversionApplication.TrustName, storedValue.TrustName);
    }

    [Test]
    public void TempDataHelperService___StoreSerialisedValue___Success()
    {
        // arrange
        var storageKey = "TempDataHelperService___StoreSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatAreYouApplyingToDoModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplication();

        // act
        TempDataHelper.StoreSerialisedValue(storageKey, pageModel.TempData, conversionApplication);

        // assert - grab value back to see if it's stored
        var storedValue = TempDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.TempData);

        Assert.AreEqual(conversionApplication.Id, storedValue.Id);
        Assert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
        Assert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
        Assert.AreEqual(conversionApplication.Application, storedValue.Application);
        Assert.AreEqual(conversionApplication.TrustName, storedValue.TrustName);
    }

    private static WhatAreYouApplyingToDoModel SetupWhatIsYourRoleModel(
        ILogger<WhatAreYouApplyingToDoModel> mockLogger,
        IConversionApplicationCreationService mockAcademisationCreationService,
        bool isAuthenticated = false)
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
