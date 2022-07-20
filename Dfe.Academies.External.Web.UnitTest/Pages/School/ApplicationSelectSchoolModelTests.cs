using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationSelectSchoolModelTests
{
    /// <summary>
    /// "draftConversionApplication" in temp storage
    /// from previous step in the new application wizard
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task ApplicationSelectSchoolModel___OnGetAsync___Valid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectSchoolModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
    }

    [Test]
    public async Task ApplicationSelectSchoolModel___TestSelectedUrnProperty__Valid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectSchoolModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "Wise Owl primary school (587634)";

        // assert
        Assert.That(pageModel.SelectedUrn, Is.EqualTo(587634));
    }

    [Test]
    public async Task ApplicationSelectSchoolModel___TestSelectedUrnProperty__InValid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectSchoolModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "";

        // assert
        Assert.That(pageModel.SelectedUrn, Is.EqualTo(0));
    }

    [Test]
    public async Task ApplicationSelectSchoolModel___TestSelectedSchoolProperty__Valid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectSchoolModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "Wise Owl primary school (587634)";

        // assert
        Assert.That(pageModel.SelectedSchoolName, Is.EqualTo("Wise Owl primary school"));
    }

    [Test]
    public async Task ApplicationSelectSchoolModel___TestSelectedSchoolProperty__InValid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectSchoolModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "";

        // assert
        Assert.That(pageModel.SelectedSchoolName, Is.EqualTo(""));
    }

    private static ApplicationSelectSchoolModel SetupApplicationSelectSchoolModel(
        ILogger<ApplicationSelectSchoolModel> mockLogger,
        IConversionApplicationCreationService mockConversionApplicationCreationService,
        bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new ApplicationSelectSchoolModel(mockLogger, mockConversionApplicationCreationService)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}