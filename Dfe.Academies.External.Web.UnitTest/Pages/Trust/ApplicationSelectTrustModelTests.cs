using Dfe.Academies.External.Web.Pages.Trust;
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

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust;

[Parallelizable(ParallelScope.All)]
public class ApplicationSelectTrustModelTests
{
	/// <summary>
	/// "draftConversionApplication" in temp storage
	/// from previous step in the new application wizard
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task OnGetAsync___Valid___NullErrors()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockLogger = new Mock<ILogger<ApplicationSelectTrustModel>>();

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
    public async Task SearchQuery___Valid__SelectedUrnValid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectTrustModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "Wise Owl primary school (587634)";

        // assert
        Assert.That(pageModel.SelectedUkPrn, Is.EqualTo(587634));
    }

    [Test]
    public async Task SearchQuery___Empty___SelectedUrnZero()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectTrustModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "";

        // assert
        Assert.That(pageModel.SelectedUkPrn, Is.EqualTo(0));
    }

    [Test]
    public async Task SearchQuery___Valid___SelectedTrustNameNotEmpty()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectTrustModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "Wise Owl primary school (587634)";

        // assert
        Assert.That(pageModel.SelectedTrustName, Is.EqualTo("Wise Owl primary school"));
    }

    [Test]
    public async Task SearchQuery___Empty__SelectedTrustNameEmpty()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<ApplicationSelectTrustModel>>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupApplicationSelectSchoolModel(mockLogger.Object, mockConversionApplicationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        pageModel.SearchQuery = "";

        // assert
        Assert.That(pageModel.SelectedTrustName, Is.EqualTo(""));
    }

    // TODO MR:- OnPostAsync___ModelIsValid___Invalid
    // when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

    // TODO MR:- OnPostAsync___ModelIsValid___Valid
    // when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static ApplicationSelectTrustModel SetupApplicationSelectSchoolModel(
        ILogger<ApplicationSelectTrustModel> mockLogger,
        IConversionApplicationCreationService mockConversionApplicationCreationService,
        bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new ApplicationSelectTrustModel(mockLogger, mockConversionApplicationCreationService)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}