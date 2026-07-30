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
internal sealed class FinancesReviewModelTests
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
		var pageModel = SetupFinancesReviewModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static FinancesReviewModel SetupFinancesReviewModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new FinancesReviewModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasFinancialData_PopulatesFinancesSummaryViewModel()
	{
		// Arrange
		var applicationId = 12345;
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var applicationDetails = ApplicationFactory.Create(applicationId);
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(applicationDetails);

		var pageModel = SetupFinancesReviewModel(
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = applicationId;

		var endDate = new DateTime(2025, 7, 31);
		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			PreviousFinancialYear = new SchoolFinancialYear(
				FinancialYearEndDate: endDate,
				Revenue: 450000.75m,
				RevenueStatus: RevenueType.Surplus,
				CapitalCarryForward: 25000.50m
			),
			CurrentFinancialYear = new SchoolFinancialYear(
				FinancialYearEndDate: endDate,
				Revenue: 480000.00m,
				RevenueStatus: RevenueType.Surplus,
				CapitalCarryForward: 30000.00m
			),
			NextFinancialYear = new SchoolFinancialYear(
				FinancialYearEndDate: endDate,
				Revenue: 500000.00m,
				RevenueStatus: RevenueType.Surplus,
				CapitalCarryForward: 35000.00m
			),
			HasLoans = true,
			HasLeases = false,
			FinanceOngoingInvestigations = false
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(applicationDetails.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(6)); // PFY, CFY, NFY, Loans, Leases, Financial Investigations
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasMinimalFinancialData_PopulatesNotStartedStatuses()
	{
		// Arrange
		var applicationId = 67890;
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var applicationDetails = ApplicationFactory.Create(applicationId);
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(applicationDetails);

		var pageModel = SetupFinancesReviewModel(
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = applicationId;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			PreviousFinancialYear = new SchoolFinancialYear(),
			CurrentFinancialYear = new SchoolFinancialYear(),
			NextFinancialYear = new SchoolFinancialYear(),
			HasLoans = null,
			HasLeases = null,
			FinanceOngoingInvestigations = null
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(applicationDetails.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(6));
		
		// All sections should have NotStarted status when data is minimal
		foreach (var heading in pageModel.ViewModel)
		{
			Assert.That(heading.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));
		}
	}
}
