﻿using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Pages.Trust.JoinAMat;
using Microsoft.Extensions.Logging;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.JoinAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationSchoolJoinAMatTrustSummaryModelTests
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
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		int urn = 101934;
		int applicationId = int.MaxValue;
		var mockLogger = new Mock<ILogger<ApplicationSchoolJoinAMatTrustSummaryModel>>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationSchoolJoinAMatTrustSummaryModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object, mockFileUploadService.Object,
			mockLogger.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ApplicationSchoolJoinAMatTrustSummaryModel SetupApplicationSchoolJoinAMatTrustSummaryModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IFileUploadService fileUploadService,
		ILogger<ApplicationSchoolJoinAMatTrustSummaryModel> mockLogger,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationSchoolJoinAMatTrustSummaryModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, fileUploadService, mockLogger)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
