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

internal sealed class ViewDataHelperTests
{
    [Test]
    public void ViewDataHelper___GetNonSerialisedValue___Success()
    {
        // arrange
        var expected = int.MaxValue.ToString();
        var storageKey = "ViewDataHelper___GetNonSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        ViewDataHelper.StoreNonSerialisedValue(storageKey, pageModel.ViewData, expected);

        // assert - grab value back to see if it's stored
        var storedValue = ViewDataHelper.GetNonSerialisedValue(storageKey, pageModel.ViewData);

        Assert.AreEqual(storedValue, expected);
    }

    [Test]
    public void ViewDataHelper___StoreNonSerialisedValue___Success()
    {
        // arrange
        var expected = int.MaxValue.ToString();
        var storageKey = "ViewDataHelper___StoreNonSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        ViewDataHelper.StoreNonSerialisedValue(storageKey, pageModel.ViewData, expected);

        // assert - grab value back to see if it's stored
        var storedValue = ViewDataHelper.GetNonSerialisedValue(storageKey, pageModel.ViewData);

        Assert.AreEqual(storedValue, expected);
    }

    [Test]
    public void ViewDataHelper___GetSerialisedValue___Success()
    {
        // arrange
        var storageKey = "ViewDataHelper___GetSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        ViewDataHelper.StoreSerialisedValue(storageKey, pageModel.ViewData, conversionApplication);

        // assert - grab value back to see if it's stored
        var storedValue = ViewDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.ViewData);

        Assert.AreEqual(conversionApplication.ApplicationId, storedValue.ApplicationId);
        Assert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
        Assert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
        Assert.AreEqual(conversionApplication.Application, storedValue.Application);
    }

    [Test]
    public void TempDataHelperService___StoreSerialisedValue___Success()
    {
        // arrange
        var storageKey = "ViewDataHelper___StoreSerialisedValue___Success";
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        ViewDataHelper.StoreSerialisedValue(storageKey, pageModel.ViewData, conversionApplication);

        // assert - grab value back to see if it's stored
        var storedValue = ViewDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.ViewData);

        Assert.AreEqual(conversionApplication.ApplicationId, storedValue.ApplicationId);
        Assert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
        Assert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
        Assert.AreEqual(conversionApplication.Application, storedValue.Application);
    }

    private static WhatIsYourRoleModel SetupWhatIsYourRoleModel(
        ILogger<WhatIsYourRoleModel> mockLogger,
        IConversionApplicationCreationService mockAcademisationCreationService,
        bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new WhatIsYourRoleModel(mockLogger, mockAcademisationCreationService)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}