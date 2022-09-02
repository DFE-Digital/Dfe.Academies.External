using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class LandAndBuildingsModelTests
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
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedExplainedNotEntered___SchoolBuildLandWorksPlannedErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.SchoolBuildLandWorksPlannedError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedDateNotEntered___SchoolBuildLandWorksPlannedDateErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.SchoolBuildLandWorksPlannedDateError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandSharedFacilitiesExplainedNotEntered___SchoolBuildLandSharedFacilitiesExplainedErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.SchoolBuildLandSharedFacilitiesExplainedError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandGrantsBodiesNotEntered___SchoolBuildLandGrantsBodiesErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.SchoolBuildLandGrantsBodiesError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandPFISchemeTypeNotEntered___SchoolBuildLandPFISchemeTypeErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.SchoolBuildLandPFISchemeTypeError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedExplainedNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedDateNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandSharedFacilitiesExplainedNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandGrantsBodiesNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandPFISchemeTypeNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<LandAndBuildingsModel>>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync(mockForm.Object);

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.HasError);
	}

	// TODO MR:- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO MR:- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static LandAndBuildingsModel SetupLandAndBuildingsModel(
		ILogger<LandAndBuildingsModel> mockLogger,
		IConversionApplicationCreationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new LandAndBuildingsModel(mockLogger, mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
