﻿using AutoFixture;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Moq;
using System.Threading.Tasks;
using NUnit.Framework;
using Dfe.Academies.External.Web.Pages.Trust.FormAMat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.FormAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationNewTrustGrowthPlansModelTests
{
	private static readonly Fixture Fixture = new();

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
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int applicationId = Fixture.Create<int>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationNewTrustGrowthPlansModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ApplicationNewTrustPlansForGrowthModel SetupApplicationNewTrustGrowthPlansModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationService mockConversionApplicationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationNewTrustPlansForGrowthModel(mockConversionApplicationRetrievalService, mockReferenceDataRetrievalService,
			mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
