using AutoFixture;
using Dfe.Academies.External.Web.Pages.Trust.FormAMat;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.FormAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationNewTrustGovernanceSummaryModelTests
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
		var mockFileUploadService = new Mock<IFileUploadService>();
		int applicationId = Fixture.Create<int>();
		var mockLogger = new Mock<ILogger<ApplicationNewTrustGovernanceSummaryModel>>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationNewTrustGovernanceSummaryModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object, mockFileUploadService.Object,
			mockLogger.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ApplicationNewTrustGovernanceSummaryModel SetupApplicationNewTrustGovernanceSummaryModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IFileUploadService mockFileUploadService,
		ILogger<ApplicationNewTrustGovernanceSummaryModel> mockLogger,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationNewTrustGovernanceSummaryModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockFileUploadService, mockLogger)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
