﻿using Dfe.Academies.External.Web.Dtos;
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

internal sealed class ViewDataHelperTests
{
	[Test]
	public void ViewDataHelper___GetNonSerialisedValue___Success()
	{
		// arrange
		var expected = int.MaxValue.ToString();
		var storageKey = "ViewDataHelper___GetNonSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);

		// act
		ViewDataHelper.StoreNonSerialisedValue(storageKey, pageModel.ViewData, expected);

		// assert - grab value back to see if it's stored
		var storedValue = ViewDataHelper.GetNonSerialisedValue(storageKey, pageModel.ViewData);

		ClassicAssert.AreEqual(storedValue, expected);
	}

	[Test]
	public void ViewDataHelper___StoreNonSerialisedValue___Success()
	{
		// arrange
		var expected = int.MaxValue.ToString();
		var storageKey = "ViewDataHelper___StoreNonSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);

		// act
		ViewDataHelper.StoreNonSerialisedValue(storageKey, pageModel.ViewData, expected);

		// assert - grab value back to see if it's stored
		var storedValue = ViewDataHelper.GetNonSerialisedValue(storageKey, pageModel.ViewData);

		ClassicAssert.AreEqual(storedValue, expected);
	}

	[Test]
	public void ViewDataHelper___GetSerialisedValue___Success()
	{
		// arrange
		var storageKey = "ViewDataHelper___GetSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		ViewDataHelper.StoreSerialisedValue(storageKey, pageModel.ViewData, conversionApplication);

		// assert - grab value back to see if it's stored
		var storedValue = ViewDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.ViewData);

		ClassicAssert.AreEqual(conversionApplication.ApplicationId, storedValue.ApplicationId);
		ClassicAssert.AreEqual(conversionApplication.ApplicationType, storedValue.ApplicationType);
		ClassicAssert.AreEqual(conversionApplication.UserEmail, storedValue.UserEmail);
		ClassicAssert.AreEqual(conversionApplication.ApplicationTitle, storedValue.ApplicationTitle);
	}

	[Test]
	public void TempDataHelperService___StoreSerialisedValue___Success()
	{
		// arrange
		var storageKey = "ViewDataHelper___StoreSerialisedValue___Success";
		var mockAcademisationCreationService = new Mock<IConversionApplicationService>();
		var pageModel = SetupWhatIsYourRoleModel(mockAcademisationCreationService.Object);
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		ViewDataHelper.StoreSerialisedValue(storageKey, pageModel.ViewData, conversionApplication);

		// assert - grab value back to see if it's stored
		var storedValue = ViewDataHelper.GetSerialisedValue<ConversionApplication>(storageKey, pageModel.ViewData);

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
