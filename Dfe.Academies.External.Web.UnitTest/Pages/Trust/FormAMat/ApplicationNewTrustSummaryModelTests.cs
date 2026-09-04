using AutoFixture;
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
using System.Collections.Generic;
using System.Linq;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.FormAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationNewTrustSummaryModelTests
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
		int applicationId = Fixture.Create<int>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationNewTrustSummaryModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task PopulateUiModel_WhenConversionApplicationHasFormTrustDetails_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupApplicationNewTrustSummaryModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var formAMatComponents = new List<ApplicationComponentViewModel>
		{
			new("Trust Details", "/trust/trust-details", Status.Completed),
			new("Chair of Trustees", "/trust/chair-trustees", Status.InProgress)
		};

		pageModel.FormAMaTComponents = formAMatComponents;

		var conversionApplication = new ConversionApplication
		{
			ApplicationId = 12345,
			ApplicationType = ApplicationTypes.FormAMat,
			FormTrustDetails = new NewTrust
			{
				FormTrustProposedNameOfTrust = "Test Trust Name"
			}
		};

		// Act
		await pageModel.PopulateUiModel(conversionApplication);

		// Assert
		Assert.That(pageModel.TrustName, Is.EqualTo("Test Trust Name"));
		Assert.That(pageModel.ApplicationType, Is.EqualTo(ApplicationTypes.FormAMat));
		Assert.That(pageModel.FormAMatTrustComponents, Is.Not.Null);
		Assert.That(pageModel.FormAMatTrustComponents.ApplicationId, Is.EqualTo(12345));
		Assert.That(pageModel.FormAMatTrustComponents.TrustComponents, Has.Count.EqualTo(2));
		Assert.That(pageModel.FormAMatTrustComponents.TrustComponents.First().Name, Is.EqualTo("Trust Details"));
	}

	[Test]
	public async Task PopulateUiModel_WhenConversionApplicationIsNull_DoesNotPopulateModel()
	{
		// Arrange
		var pageModel = SetupApplicationNewTrustSummaryModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var originalTrustName = pageModel.TrustName;
		var originalApplicationType = pageModel.ApplicationType;

		// Act
		await pageModel.PopulateUiModel(null);

		// Assert
		Assert.That(pageModel.TrustName, Is.EqualTo(originalTrustName));
		Assert.That(pageModel.ApplicationType, Is.EqualTo(originalApplicationType));
		Assert.That(pageModel.FormAMatTrustComponents.TrustComponents, Is.Empty);
	}

	[Test]
	public async Task PopulateUiModel_WhenFormTrustDetailsIsNull_DoesNotPopulateModel()
	{
		// Arrange
		var pageModel = SetupApplicationNewTrustSummaryModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var conversionApplication = new ConversionApplication
		{
			ApplicationId = 12345,
			ApplicationType = ApplicationTypes.FormAMat,
			FormTrustDetails = null
		};

		var originalTrustName = pageModel.TrustName;
		var originalApplicationType = pageModel.ApplicationType;

		// Act
		await pageModel.PopulateUiModel(conversionApplication);

		// Assert
		Assert.That(pageModel.TrustName, Is.EqualTo(originalTrustName));
		Assert.That(pageModel.ApplicationType, Is.EqualTo(originalApplicationType));
		Assert.That(pageModel.FormAMatTrustComponents.TrustComponents, Is.Empty);
	}

	private static ApplicationNewTrustSummaryModel SetupApplicationNewTrustSummaryModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationNewTrustSummaryModel(mockConversionApplicationRetrievalService, 
			mockReferenceDataRetrievalService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
