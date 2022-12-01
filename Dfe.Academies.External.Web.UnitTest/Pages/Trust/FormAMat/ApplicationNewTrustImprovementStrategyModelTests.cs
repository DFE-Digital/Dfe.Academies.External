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

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.FormAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationNewTrustImprovementStrategyModelTests
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
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		int applicationId = Fixture.Create<int>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationNewTrustImprovementStrategyModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ApplicationNewTrustImprovementStrategyModel SetupApplicationNewTrustImprovementStrategyModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationCreationService mockConversionApplicationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationNewTrustImprovementStrategyModel(mockConversionApplicationRetrievalService, mockReferenceDataRetrievalService,
			mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
