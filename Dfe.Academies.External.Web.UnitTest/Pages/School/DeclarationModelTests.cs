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
internal sealed class DeclarationModelTests
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
		var pageModel = SetupDeclarationModel(mockConversionApplicationCreationService.Object,
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

	private static DeclarationModel SetupDeclarationModel(
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new DeclarationModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasDeclarationData_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupDeclarationModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			DeclarationIAmTheChairOrHeadteacher = true,
			DeclarationBodyAgree = true
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.SchoolDeclarationTeacherChair, Is.True);
		Assert.That(pageModel.SchoolDeclarationBodyAgree, Is.True);
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoDeclarationData_LeavesModelUnchanged()
	{
		// Arrange
		var pageModel = SetupDeclarationModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			DeclarationIAmTheChairOrHeadteacher = null,
			DeclarationBodyAgree = null
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert - Properties should remain at their default values since we didn't populate them
		Assert.That(pageModel.SchoolDeclarationTeacherChair, Is.False); // default bool value
		Assert.That(pageModel.SchoolDeclarationBodyAgree, Is.False); // default bool value
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasPartialDeclarationData_PopulatesOnlyAvailableData()
	{
		// Arrange
		var pageModel = SetupDeclarationModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			DeclarationIAmTheChairOrHeadteacher = false,
			DeclarationBodyAgree = null
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.SchoolDeclarationTeacherChair, Is.False);
		Assert.That(pageModel.SchoolDeclarationBodyAgree, Is.False); // default value since null
	}
}

