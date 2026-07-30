using System.Threading.Tasks;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class LandAndBuildingsSummaryModelTests
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
		var pageModel = SetupLandAndBuildingsSummaryModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasLandAndBuildingsData_PopulatesSummaryViewModel()
	{
		// Arrange
		var mockRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var application = ApplicationFactory.Create(12345);
		mockRetrievalService.Setup(x => x.GetApplication(12345))
			.ReturnsAsync(application);

		var pageModel = SetupLandAndBuildingsSummaryModel(
			mockRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = 12345;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			LandAndBuildings = new SchoolLandAndBuildings(
				"School owns the land", // OwnerExplained
				true, // WorksPlanned
				"Renovation work", // WorksPlannedExplained
				new DateTime(2025, 6, 1), // WorksPlannedDate
				false, // FacilitiesShared
				"No shared facilities", // FacilitiesSharedExplained
				true, // Grants
				"Department for Education", // GrantsAwardingBodies
				false, // PartOfPFIScheme
				"Not applicable", // PartOfPFISchemeType
				true, // PartOfPrioritySchoolsBuildingProgramme
				false // PartOfBuildingSchoolsForFutureProgramme
			)
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(application.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(1));

		var heading = pageModel.ViewModel[0];
		Assert.That(heading.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));
		Assert.That(heading.Sections, Has.Count.EqualTo(7)); // All sections should be present

		// Check some key sections
		Assert.That(heading.Sections[0].Answer, Is.EqualTo("School owns the land"));

		var worksPlannedSection = heading.Sections.FirstOrDefault(s => s.Name.Contains("building works"));
		Assert.That(worksPlannedSection?.Answer, Contains.Substring("Yes"));
		Assert.That(worksPlannedSection?.SubQuestionAndAnswers, Has.Count.EqualTo(2));
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasMinimalLandAndBuildingsData_PopulatesNotStartedStatus()
	{
		// Arrange
		var mockRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var application = ApplicationFactory.Create(12345);
		mockRetrievalService.Setup(x => x.GetApplication(12345))
			.ReturnsAsync(application);

		var pageModel = SetupLandAndBuildingsSummaryModel(
			mockRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = 12345;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			LandAndBuildings = new SchoolLandAndBuildings(
				null, // OwnerExplained
				null, // WorksPlanned - Key field is null - should be NotStarted
				null, // WorksPlannedExplained
				null, // WorksPlannedDate
				null, // FacilitiesShared
				null, // FacilitiesSharedExplained
				null, // Grants
				null, // GrantsAwardingBodies
				null, // PartOfPFIScheme
				null, // PartOfPFISchemeType
				null, // PartOfPrioritySchoolsBuildingProgramme
				null  // PartOfBuildingSchoolsForFutureProgramme
			)
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(application.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(1));

		var heading = pageModel.ViewModel[0];
		Assert.That(heading.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));
		Assert.That(heading.Sections, Has.Count.EqualTo(7));

		// Check that sections contain "You have not added" or similar default answers
		Assert.That(heading.Sections[0].Answer, Contains.Substring("You have not added"));
	}

	private static LandAndBuildingsSummaryModel SetupLandAndBuildingsSummaryModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new LandAndBuildingsSummaryModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
