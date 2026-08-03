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
internal sealed class SchoolConversionKeyDetailsModelTests
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
		var pageModel = SetupSchoolConversionKeyDetailsModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasKeyDetailsData_PopulatesSummaryViewModel()
	{
		// Arrange
		var mockRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var application = ApplicationFactory.Create(12345);
		mockRetrievalService.Setup(x => x.GetApplication(12345))
			.ReturnsAsync(application);

		var pageModel = SetupSchoolConversionKeyDetailsModel(
			mockRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = 12345;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			SchoolConversionContactHeadName = "John Smith",
			SchoolConversionContactHeadEmail = "john.smith@testschool.com",
			SchoolConversionContactChairName = "Jane Doe",
			SchoolConversionContactChairEmail = "jane.doe@testschool.com",
			SchoolConversionContactRole = "HeadTeacher",
			SchoolConversionApproverContactName = "Bob Wilson",
			SchoolConversionApproverContactEmail = "bob.wilson@testschool.com",
			SchoolConversionTargetDateSpecified = true,
			SchoolConversionTargetDate = new DateTime(2025, 9, 1),
			SchoolConversionTargetDateExplained = "Start of academic year",
			ApplicationJoinTrustReason = "Better educational opportunities",
			ConversionChangeNamePlanned = false
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(application.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(4)); // 4 main headings

		// Check contacts section
		var contactsSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Contact details"));
		Assert.That(contactsSection, Is.Not.Null);
		Assert.That(contactsSection.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));
		Assert.That(contactsSection.Sections.Count, Is.GreaterThan(5));

		// Check conversion date section
		var dateSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Date for conversion"));
		Assert.That(dateSection, Is.Not.Null);
		Assert.That(dateSection.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));

		// Check join trust reason section
		var trustReasonSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Reasons for joining"));
		Assert.That(trustReasonSection, Is.Not.Null);
		Assert.That(trustReasonSection.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));

		// Check name change section
		var nameChangeSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Changing the name"));
		Assert.That(nameChangeSection, Is.Not.Null);
		Assert.That(nameChangeSection.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));
	}

	[Test]
	public async Task PopulateUiModel_WhenSchoolHasMinimalKeyDetailsData_PopulatesNotStartedStatuses()
	{
		// Arrange
		var mockRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var application = ApplicationFactory.Create(12345);
		mockRetrievalService.Setup(x => x.GetApplication(12345))
			.ReturnsAsync(application);

		var pageModel = SetupSchoolConversionKeyDetailsModel(
			mockRetrievalService.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = 12345;

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			SchoolConversionContactHeadName = null,
			SchoolConversionContactHeadEmail = null,
			SchoolConversionContactChairName = null,
			SchoolConversionContactChairEmail = null,
			SchoolConversionContactRole = null,
			SchoolConversionApproverContactName = null,
			SchoolConversionApproverContactEmail = null,
			SchoolConversionTargetDateSpecified = null,
			SchoolConversionTargetDate = null,
			SchoolConversionTargetDateExplained = null,
			ApplicationJoinTrustReason = null,
			ConversionChangeNamePlanned = null
		};

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(application.ApplicationStatus));
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel, Has.Count.EqualTo(4));

		// Check that most sections show NotStarted status
		var contactsSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Contact details"));
		Assert.That(contactsSection?.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));

		var dateSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Date for conversion"));
		Assert.That(dateSection?.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));

		var trustReasonSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Reasons for joining"));
		Assert.That(trustReasonSection?.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));

		var nameChangeSection = pageModel.ViewModel.FirstOrDefault(v => v.Title.Contains("Changing the name"));
		Assert.That(nameChangeSection?.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static SchoolConversionKeyDetailsModel SetupSchoolConversionKeyDetailsModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new SchoolConversionKeyDetailsModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
