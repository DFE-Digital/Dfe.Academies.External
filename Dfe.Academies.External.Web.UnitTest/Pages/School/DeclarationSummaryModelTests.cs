using System;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class DeclarationSummaryModelTests
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
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupDeclarationSummaryModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static DeclarationSummaryModel SetupDeclarationSummaryModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new DeclarationSummaryModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasDeclarationData_PopulatesSummaryViewModel()
	{
		// Arrange
		var applicationId = 12345;
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var applicationDetails = ApplicationFactory.Create(applicationId);
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(applicationDetails);

		var pageModel = SetupDeclarationSummaryModel(
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = applicationId;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			DeclarationIAmTheChairOrHeadteacher = true,
			DeclarationBodyAgree = true
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(applicationDetails.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(1));
		
		var heading = pageModel.ViewModel[0];
		Assert.That(heading.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));
		Assert.That(heading.Sections, Has.Count.EqualTo(1));
		Assert.That(heading.Sections[0].Answer, Is.EqualTo("Yes"));
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasNoDeclarationData_PopulatesNotStartedStatus()
	{
		// Arrange
		var applicationId = 67890;
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var applicationDetails = ApplicationFactory.Create(applicationId);
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(applicationDetails);

		var pageModel = SetupDeclarationSummaryModel(
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = applicationId;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			DeclarationIAmTheChairOrHeadteacher = null,
			DeclarationBodyAgree = null
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(applicationDetails.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(1));
		
		var heading = pageModel.ViewModel[0];
		Assert.That(heading.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));
		Assert.That(heading.Sections, Has.Count.EqualTo(1));
	}
}
