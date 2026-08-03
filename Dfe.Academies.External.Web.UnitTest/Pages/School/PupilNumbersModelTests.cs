using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
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
internal sealed class PupilNumbersModelTests
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
		var pageModel = SetupPupilNumbersModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static PupilNumbersModel SetupPupilNumbersModel(
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new PupilNumbersModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasPupilNumbersData_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupPupilNumbersModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			SchoolCapacityPublishedAdmissionsNumber = 150,
			ProjectedPupilNumbersYear1 = 160,
			ProjectedPupilNumbersYear2 = 170,
			ProjectedPupilNumbersYear3 = 180,
			SchoolCapacityAssumptions = "Projected growth based on local development plans"
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.SchoolCapacityPublishedAdmissionsNumber, Is.EqualTo(150));
		Assert.That(pageModel.ProjectedPupilNumbersYear1, Is.EqualTo(160));
		Assert.That(pageModel.ProjectedPupilNumbersYear2, Is.EqualTo(170));
		Assert.That(pageModel.ProjectedPupilNumbersYear3, Is.EqualTo(180));
		Assert.That(pageModel.SchoolCapacityAssumptions, Is.EqualTo("Projected growth based on local development plans"));
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoPupilNumbersData_SetsDefaults()
	{
		// Arrange
		var pageModel = SetupPupilNumbersModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null);

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.SchoolCapacityPublishedAdmissionsNumber, Is.Null);
		Assert.That(pageModel.ProjectedPupilNumbersYear1, Is.Null);
		Assert.That(pageModel.ProjectedPupilNumbersYear2, Is.Null);
		Assert.That(pageModel.ProjectedPupilNumbersYear3, Is.Null);
		Assert.That(pageModel.SchoolCapacityAssumptions, Is.Null);
	}
}
