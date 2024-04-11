using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Dfe.Academies.External.Web.UnitTest.Services;

internal sealed class TempDataHelperTests
{
	[Test]
	public void TempDataHelperService___GetNonSerialisedValue___Success()
	{
		// arrange
		var expected = int.MaxValue.ToString();
		var storageKey = "TempDataHelperService___GetNonSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);

		// act
		TempDataHelper.StoreNonSerialisedValue(storageKey, pageModel.TempData, expected);

		// assert - grab value back to see if it's stored
		var storedValue = TempDataHelper.GetNonSerialisedValue(storageKey, pageModel.TempData);

		ClassicAssert.AreEqual(storedValue, expected);
	}

	[Test]
	public void TempDataHelperService___StoreNonSerialisedValue___Success()
	{
		// arrange
		var expected = int.MaxValue.ToString();
		var storageKey = "TempDataHelperService___StoreNonSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);

		// act
		TempDataHelper.StoreNonSerialisedValue(storageKey, pageModel.TempData, expected);

		// assert - grab value back to see if it's stored
		var storedValue = TempDataHelper.GetNonSerialisedValue(storageKey, pageModel.TempData);

		ClassicAssert.AreEqual(storedValue, expected);
	}

	[Test]
	public void TempDataHelperService___GetSerialisedValue___Success()
	{
		// arrange
		var storageKey = "TempDataHelperService___GetSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		TempDataHelper.StoreSerialisedValue(storageKey, pageModel.TempData, conversionApplication);

		// assert - grab value back to see if it's stored
		var storedValue = TempDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.TempData);

		ClassicAssert.AreEqual(conversionApplication.ApplicationId, storedValue.ApplicationId);
		ClassicAssert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
		ClassicAssert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
		ClassicAssert.AreEqual(conversionApplication.ApplicationTitle, storedValue.ApplicationTitle);
	}

	[Test]
	public void TempDataHelperService___StoreSerialisedValue___Success()
	{
		// arrange
		var storageKey = "TempDataHelperService___StoreSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		TempDataHelper.StoreSerialisedValue(storageKey, pageModel.TempData, conversionApplication);

		// assert - grab value back to see if it's stored
		var storedValue = TempDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.TempData);

		ClassicAssert.AreEqual(conversionApplication.ApplicationId, storedValue.ApplicationId);
		ClassicAssert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
		ClassicAssert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
		ClassicAssert.AreEqual(conversionApplication.ApplicationTitle, storedValue.ApplicationTitle);
	}

	private static WhatIsYourRoleModel SetupWhatIsYourRoleModel(
		IConversionApplicationService mockAcademisationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new WhatIsYourRoleModel(mockAcademisationCreationService, null)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
