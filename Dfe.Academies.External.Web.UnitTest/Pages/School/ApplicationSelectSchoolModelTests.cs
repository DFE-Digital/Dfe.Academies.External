using System.Threading.Tasks;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

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
	public void OnGetAsync___Valid___NullErrors()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int urn = 101934;

		// act
		var pageModel = SetupApplicationSelectSchoolModel(mockConversionApplicationRetrievalService.Object,
															mockReferenceDataRetrievalService.Object,
															mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		pageModel.OnGetAsync(conversionApplication.ApplicationId, urn);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	[Test]
	public void SearchQuery___Valid__SelectedUrnValid()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int urn = 101934;

		// act
		var pageModel = SetupApplicationSelectSchoolModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		pageModel.OnGetAsync(conversionApplication.ApplicationId, urn);

		pageModel.SearchQuery = "Wise Owl primary school (587634)";

		// assert
		Assert.That(pageModel.SelectedUrn, Is.EqualTo(587634));
	}

	[Test]
	public void SearchQuery___Empty___SelectedUrnZero()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int urn = 101934;

		// act
		var pageModel = SetupApplicationSelectSchoolModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		pageModel.OnGetAsync(conversionApplication.ApplicationId, urn);

		pageModel.SearchQuery = "";

		// assert
		Assert.That(pageModel.SelectedUrn, Is.EqualTo(0));
	}

	[Test]
	public void SearchQuery___Valid___SelectedSchoolNameNotEmpty()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int urn = 101934;

		// act
		var pageModel = SetupApplicationSelectSchoolModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		pageModel.OnGetAsync(conversionApplication.ApplicationId, urn);

		pageModel.SearchQuery = "Wise Owl primary school (587634)";

		// assert
		Assert.That(pageModel.SelectedSchoolName, Is.EqualTo("Wise Owl primary school"));
	}

	[Test]
	public void SearchQuery___Empty__SelectedSchoolNameEmpty()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int urn = 101934;

		// act
		var pageModel = SetupApplicationSelectSchoolModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		pageModel.OnGetAsync(conversionApplication.ApplicationId, urn);

		pageModel.SearchQuery = "";

		// assert
		Assert.That(pageModel.SelectedSchoolName, Is.EqualTo(""));
	}

	[Test]
	public void RunUiValidation_SearchQueryEmpty_AddsInvalidSchoolErrorAndReturnsFalse()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = null;
		pageModel.ModelState.AddModelError("SearchQuery", "You must give the name of the school");

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("InvalidSchool"), Is.True);
	}

	[Test]
	public void RunUiValidation_ModelStateValid_ReturnsTrue()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = "Test School (123456)";
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public void OnPostFind_ReturnsRedirectToSchoolSearchResults()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = "Wise Owl";

		var result = pageModel.OnPostFind();

		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		var redirect = (RedirectToPageResult)result;
		Assert.That(redirect.PageName, Is.EqualTo("SchoolSearchResults"));
	}

	[Test]
	public void SelectedSchoolName_SearchQueryWithLeadingTrailingSpaces_TrimsCorrectly()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = "  St Mary's Academy (400001)  ";

		Assert.That(pageModel.SelectedSchoolName, Is.EqualTo("St Mary's Academy"));
	}

	[Test]
	public void SelectedUrn_SearchQueryWithSpacesAroundParentheses_ReturnsCorrectUrn()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = "  St Mary's Academy ( 400001 )  ";

		Assert.That(pageModel.SelectedUrn, Is.EqualTo(400001));
	}

	[Test]
	public void SelectedSchoolName_SearchQueryNull_ReturnsEmptyString()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = null;

		Assert.That(pageModel.SelectedSchoolName, Is.EqualTo(string.Empty));
	}

	[Test]
	public void SelectedUrn_SearchQueryNull_ReturnsZero()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = null;

		Assert.That(pageModel.SelectedUrn, Is.EqualTo(0));
	}

	[Test]
	public void SelectedUrn_SearchQueryWhitespaceOnly_ReturnsZero()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = "   ";

		Assert.That(pageModel.SelectedUrn, Is.EqualTo(0));
		Assert.That(pageModel.SelectedSchoolName, Is.EqualTo(string.Empty));
	}

	[Test]
	public void PopulateUpdateDictionary_ReturnsEmptyDictionary()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var result = pageModel.PopulateUpdateDictionary();

		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	[Test]
	public void PopulateUiModel_WhenConversionApplicationIsNull_DoesNotThrow()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		Assert.That(() => pageModel.PopulateUiModel(null), Throws.Nothing);
	}

	[Test]
	public void PopulateUiModel_WhenConversionApplicationIsNotNull_DoesNotThrow()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		Assert.That(() => pageModel.PopulateUiModel(conversionApplication), Throws.Nothing);
	}

	[Test]
	public async Task OnPostAddSchool_WhenValidationFails_ReturnsPageResult()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.ApplicationId = 1;
		pageModel.SearchQuery = null;
		pageModel.ModelState.AddModelError("SearchQuery", "Required");

		var result = await pageModel.OnPostAddSchool();

		Assert.That(result, Is.InstanceOf<PageResult>());
	}

	[Test]
	public async Task OnPostAddSchool_WhenValidationSucceeds_CallsAddSchoolToApplicationAndRedirects()
	{
		const int appId = 10;
		const int urn = 123456;
		const string schoolName = "Test Academy";
		var mockCreationService = new Mock<IConversionApplicationService>();
		mockCreationService
			.Setup(x => x.AddSchoolToApplication(appId, urn, schoolName))
			.Returns(Task.CompletedTask);

		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			mockCreationService.Object);
		pageModel.ApplicationId = appId;
		pageModel.SearchQuery = $"{schoolName} ({urn})";
		pageModel.ModelState.Clear();

		var result = await pageModel.OnPostAddSchool();

		mockCreationService.Verify(
			x => x.AddSchoolToApplication(appId, urn, schoolName),
			Times.Once);
		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		var redirect = (RedirectToPageResult)result;
		Assert.That(redirect.PageName, Is.EqualTo("/ApplicationOverview"));
		Assert.That(redirect.RouteValues, Is.Not.Null); 
		var routeValues = redirect.RouteValues!;
		Assert.That(routeValues["appId"], Is.Not.Null);
		Assert.That(routeValues["appId"], Is.EqualTo(appId));
	}

	[Test]
	public void CorrectSchoolConfirmation_Default_IsFalse()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		Assert.That(pageModel.CorrectSchoolConfirmation, Is.False);
	}

	[Test]
	public void RunUiValidation_ModelStateInvalidWithOtherError_ReturnsFalseAndPopulatesValidationMessages()
	{
		var pageModel = SetupApplicationSelectSchoolModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SearchQuery = "Some School (111111)";
		pageModel.ModelState.AddModelError("CorrectSchoolConfirmation", "You must confirm");

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("InvalidSchool"), Is.False);
	}

	// TODO MR:- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO MR:- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static ApplicationSelectSchoolModel SetupApplicationSelectSchoolModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationService mockConversionApplicationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationSelectSchoolModel(mockConversionApplicationRetrievalService, mockReferenceDataRetrievalService,
			mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
