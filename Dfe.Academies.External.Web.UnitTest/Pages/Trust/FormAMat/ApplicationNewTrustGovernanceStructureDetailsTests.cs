using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Dfe.Academies.External.Web.Pages.Trust.FormAMat;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.FormAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationNewTrustGovernanceStructureDetailsTests
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
		var mockFileUploadService = new Mock<IFileUploadService>();
		int applicationId = Fixture.Create<int>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(conversionApplication);
		mockFileUploadService
			.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(new List<string>());
		// act
		var pageModel = setupApplicationNewTrustGovernanceStructureDetails(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object,
			mockFileUploadService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ApplicationNewTrustGovernanceStructureDetails setupApplicationNewTrustGovernanceStructureDetails(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationCreationService mockConversionApplicationCreationService,
		IFileUploadService mockFileUploadService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationNewTrustGovernanceStructureDetails(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService,
			mockConversionApplicationCreationService,
			mockFileUploadService
			)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
