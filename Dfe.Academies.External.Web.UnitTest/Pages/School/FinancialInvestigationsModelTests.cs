using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class FinancialInvestigationsModelTests
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
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupFinancialInvestigationsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	// TODO MR:- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO MR:- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static FinancialInvestigationsModel SetupFinancialInvestigationsModel(
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new FinancialInvestigationsModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasFinancialInvestigationsData_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupFinancialInvestigationsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			FinanceOngoingInvestigations = true,
			FinancialInvestigationsExplain = "Under investigation for procurement irregularities",
			FinancialInvestigationsTrustAware = false
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.FinanceOngoingInvestigations, Is.EqualTo(SelectOption.Yes));
		Assert.That(pageModel.FinancialInvestigationsExplain, Is.EqualTo("Under investigation for procurement irregularities"));
		Assert.That(pageModel.FinancialInvestigationsTrustAware, Is.EqualTo(SelectOption.No));
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoFinancialInvestigationsData_SetsDefaults()
	{
		// Arrange
		var pageModel = SetupFinancialInvestigationsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			FinanceOngoingInvestigations = null,
			FinancialInvestigationsExplain = null,
			FinancialInvestigationsTrustAware = null
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.FinanceOngoingInvestigations, Is.Null);
		Assert.That(pageModel.FinancialInvestigationsExplain, Is.Null);
		Assert.That(pageModel.FinancialInvestigationsTrustAware, Is.Null);
	}
}
