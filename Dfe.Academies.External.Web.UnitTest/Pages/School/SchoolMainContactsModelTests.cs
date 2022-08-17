using Dfe.Academies.External.Web.Pages;
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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolMainContactsModelTests
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
		var mockLogger = new Mock<ILogger<SchoolMainContactsModel>>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupSchoolMainContactsModel(mockLogger.Object,
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
	public async Task ModelState___MainContactOtherNameNotEntered___OtherNameErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<SchoolMainContactsModel>>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("MainContactOtherNameNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.OtherNameError);
	}
	
	[Test]
	public async Task ModelState___MainContactOtherEmailNotEntered___OtherEmailErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<SchoolMainContactsModel>>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("MainContactOtherEmailNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.OtherEmailError);
	}

	[Test]
	public async Task ModelState___MainContactOtherTelephoneNotEntered___OtherTelephoneErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockLogger = new Mock<ILogger<SchoolMainContactsModel>>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockLogger.Object,
			mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.ModelState.AddModelError("MainContactOtherTelephoneNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.AreEqual(1, errors.Count);
		Assert.AreEqual(true, pageModel.OtherTelephoneError);
	}

	// TODO MR:- Test OtherContactError() prop

	// TODO MR:- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO MR:- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static SchoolMainContactsModel SetupSchoolMainContactsModel(
		ILogger<SchoolMainContactsModel> mockLogger,
		IConversionApplicationCreationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new SchoolMainContactsModel(mockLogger, mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}