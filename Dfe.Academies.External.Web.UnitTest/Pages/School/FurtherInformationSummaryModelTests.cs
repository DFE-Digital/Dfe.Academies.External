using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using GovUK.Dfe.CoreLibs.SharePoint.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class FurtherInformationSummaryModelTests
{
	[Test]
	public void RunUiValidation_Always_ReturnsTrue()
	{
		// Arrange
		var pageModel = SetupFurtherInformationSummaryModel();

		// Act
		var result = pageModel.RunUiValidation();

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void PopulateUpdateDictionary_Always_ReturnsEmptyDictionary()
	{
		// Arrange
		var pageModel = SetupFurtherInformationSummaryModel();

		// Act
		var result = pageModel.PopulateUpdateDictionary();

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Count, Is.EqualTo(0));
	}

	[Test]
	public void PopulateValidationMessages_CallsPopulateViewDataErrorsWithModelStateErrors()
	{
		// Arrange
		var pageModel = SetupFurtherInformationSummaryModel();
		pageModel.ModelState.AddModelError("TestKey", "Test error");

		// Act
		pageModel.PopulateValidationMessages();

		// Assert
		Assert.That(pageModel.ViewData["Errors"], Is.Not.Null);
	}

	[Test]
	public async Task PopulateUiModel_WithFullSchoolData_PopulatesCompleteViewModel()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 123;
		var applicationReference = "APP-REF-001";
		
		var school = new SchoolApplyingToConvert("Test School", 12345, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Trust will provide excellent support",
			OfstedInspectionDetails = "Outstanding in all areas",
			Safeguarding = true,
			LocalAuthorityReorganisationDetails = "LA restructuring planned",
			LocalAuthorityClosurePlanDetails = "Closure plan in place",
			DioceseName = "Diocese of Example",
			PartOfFederation = false,
			FoundationTrustOrBodyName = "Foundation Trust Example",
			ExemptionEndDate = DateTimeOffset.Now.AddYears(1),
			MainFeederSchools = "Primary School A, Primary School B",
			ProtectedCharacteristics = SchoolEqualitiesProtectedCharacteristics.Unlikely,
			FurtherInformation = "Additional important information"
		};

		var application = new ConversionApplication
		{
			ApplicationId = applicationId,
			ApplicationReference = applicationReference,
			ApplicationStatus = ApplicationStatus.InProgress
		};

		var mockFiles = new List<SharePointFileInfo>
		{
			new() { Name = $"{FileUploadConstants.ResolutionConsentfilePrefixFieldName}_resolution.pdf" },
			new() { Name = "other_file.pdf" }
		};

		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(mockFiles);

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel.Count, Is.EqualTo(1));
		
		var viewModel = pageModel.ViewModel[0];
		Assert.That(viewModel.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));
		Assert.That(viewModel.Sections.Count, Is.EqualTo(13));

		// Verify specific sections
		var trustBenefitSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.SchoolTrustBenefit);
		Assert.That(trustBenefitSection, Is.Not.Null);
		Assert.That(trustBenefitSection.Answer, Is.EqualTo("Trust will provide excellent support"));

		var ofstedSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.OfstedInspection);
		Assert.That(ofstedSection, Is.Not.Null);
		Assert.That(ofstedSection.Answer, Is.EqualTo("Yes"));

		var safeguardingSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.SafeguardingInvestigations);
		Assert.That(safeguardingSection, Is.Not.Null);
		Assert.That(safeguardingSection.Answer, Is.EqualTo("Yes"));

		var dioceseSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.Diocese);
		Assert.That(dioceseSection, Is.Not.Null);
		Assert.That(dioceseSection.Answer, Is.EqualTo("Yes"));

		var federationSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.Federation);
		Assert.That(federationSection, Is.Not.Null);
		Assert.That(federationSection.Answer, Is.EqualTo("No"));

		var foundationSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.FoundationTrustOrBody);
		Assert.That(foundationSection, Is.Not.Null);
		Assert.That(foundationSection.Answer, Is.EqualTo("Yes"));

		var exemptionSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.ExemptionSACRE);
		Assert.That(exemptionSection, Is.Not.Null);
		Assert.That(exemptionSection.Answer, Is.EqualTo("Yes"));

		var feederSchoolsSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.MainFeederSchools);
		Assert.That(feederSchoolsSection, Is.Not.Null);
		Assert.That(feederSchoolsSection.Answer, Is.EqualTo("Primary School A, Primary School B"));

		var resolutionSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.Resolution);
		Assert.That(resolutionSection, Is.Not.Null);
		Assert.That(resolutionSection.Answer, Does.StartWith(FileUploadConstants.ResolutionConsentfilePrefixFieldName));

		var equalitiesSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.EqualitiesImpactAssessment);
		Assert.That(equalitiesSection, Is.Not.Null);
		Assert.That(equalitiesSection.Answer, Is.EqualTo("Yes"));

		var furtherInfoSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.FurtherInformation);
		Assert.That(furtherInfoSection, Is.Not.Null);
		Assert.That(furtherInfoSection.Answer, Is.EqualTo("Yes"));

		// Verify model properties are set correctly
		Assert.That(pageModel.EntityId, Is.EqualTo(entityId));
		Assert.That(pageModel.ApplicationReference, Is.EqualTo(applicationReference));
		Assert.That(pageModel.ApplicationStatus, Is.EqualTo(ApplicationStatus.InProgress));
	}

	[Test]
	public async Task PopulateUiModel_WithMinimalSchoolData_PopulatesNotStartedViewModel()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 123;
		var applicationReference = "APP-REF-002";
		
		var school = new SchoolApplyingToConvert("Minimal School", 54321, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = null, // Section not started
			OfstedInspectionDetails = null,
			Safeguarding = null,
			LocalAuthorityReorganisationDetails = null,
			LocalAuthorityClosurePlanDetails = null,
			DioceseName = null,
			PartOfFederation = null,
			FoundationTrustOrBodyName = null,
			ExemptionEndDate = null,
			MainFeederSchools = null,
			ProtectedCharacteristics = null,
			FurtherInformation = null!
		};

		var application = new ConversionApplication
		{
			ApplicationId = applicationId,
			ApplicationReference = applicationReference,
			ApplicationStatus = ApplicationStatus.InProgress
		};

		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>());

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel.Count, Is.EqualTo(1));
		
		var viewModel = pageModel.ViewModel[0];
		Assert.That(viewModel.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));

		// Verify all sections show "not started" responses
		var trustBenefitSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.SchoolTrustBenefit);
		Assert.That(trustBenefitSection, Is.Not.Null);
		Assert.That(trustBenefitSection.Answer, Is.EqualTo(QuestionAndAnswerConstants.NoInfoAnswer));

		var ofstedSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.OfstedInspection);
		Assert.That(ofstedSection, Is.Not.Null);
		Assert.That(ofstedSection.Answer, Is.EqualTo(QuestionAndAnswerConstants.NoInfoAnswer));

		var safeguardingSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.SafeguardingInvestigations);
		Assert.That(safeguardingSection, Is.Not.Null);
		Assert.That(safeguardingSection.Answer, Is.EqualTo(QuestionAndAnswerConstants.NoInfoAnswer));
	}

	[Test]
	public async Task PopulateUiModel_WithPartiallyStartedSection_ShowsMixedResponses()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 456;
		var school = new SchoolApplyingToConvert("Partial School", 67890, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Some benefit details", // Section started
			OfstedInspectionDetails = "", // Empty but section started
			Safeguarding = false,
			LocalAuthorityReorganisationDetails = "",
			LocalAuthorityClosurePlanDetails = "",
			DioceseName = "",
			PartOfFederation = true,
			FoundationTrustOrBodyName = "",
			ExemptionEndDate = null,
			MainFeederSchools = "",
			ProtectedCharacteristics = null,
			FurtherInformation = ""
		};

		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.ApplicationId = applicationId;
		
		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>());

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		var viewModel = pageModel.ViewModel[0];
		Assert.That(viewModel.Status, Is.EqualTo(SchoolConversionComponentStatus.Complete));

		// Trust benefit should show the actual value since it has content
		var trustBenefitSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.SchoolTrustBenefit);
		Assert.That(trustBenefitSection.Answer, Is.EqualTo("Some benefit details"));

		// Ofsted should show "No" since section is started but details are empty
		var ofstedSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.OfstedInspection);
		Assert.That(ofstedSection.Answer, Is.EqualTo("No"));

		// Safeguarding should show "No" since it's explicitly false
		var safeguardingSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.SafeguardingInvestigations);
		Assert.That(safeguardingSection.Answer, Is.EqualTo("No"));

		// Federation should show "Yes" since it's true
		var federationSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.Federation);
		Assert.That(federationSection.Answer, Is.EqualTo("Yes"));

		// Exemption should show "No" since no end date is set
		var exemptionSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.ExemptionSACRE);
		Assert.That(exemptionSection.Answer, Is.EqualTo("No"));
	}

	[Test]
	public async Task PopulateUiModel_WhenSharePointThrowsException_LogsErrorAndContinuesWithoutFiles()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 789;
		var school = new SchoolApplyingToConvert("Test School", 11111, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Some details"
		};

		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.ApplicationId = applicationId;
		
		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ThrowsAsync(new Exception("SharePoint error"));

		var loggerMock = new Mock<ILogger<FurtherInformationSummaryModel>>();
		
		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object,
			loggerMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel.Count, Is.EqualTo(1));
		
		var viewModel = pageModel.ViewModel[0];
		
		// Should still have the basic sections, just without file information
		Assert.That(viewModel.Sections.Count, Is.EqualTo(10)); // Should be 10 instead of 13 since file sections are in try/catch
		
		// Verify that logging occurred
		loggerMock.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("No school file(s) directory exists yet for application")),
				It.IsAny<Exception?>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.Once);
	}

	[Test]
	public async Task PopulateUiModel_WhenMultipleResolutionFilesExist_ShowsFirstFile()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 999;
		var school = new SchoolApplyingToConvert("Multi-File School", 22222, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Started"
		};

		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.ApplicationId = applicationId;

		var mockFiles = new List<SharePointFileInfo>
		{
			new() { Name = $"{FileUploadConstants.ResolutionConsentfilePrefixFieldName}_resolution1.pdf" },
			new() { Name = $"{FileUploadConstants.ResolutionConsentfilePrefixFieldName}_resolution2.pdf" },
			new() { Name = "unrelated_file.doc" }
		};
		
		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(mockFiles);

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		var viewModel = pageModel.ViewModel[0];
		var resolutionSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.Resolution);
		Assert.That(resolutionSection, Is.Not.Null);
		Assert.That(resolutionSection.Answer, Does.StartWith(FileUploadConstants.ResolutionConsentfilePrefixFieldName));
		Assert.That(resolutionSection.Answer, Does.Contain("resolution1.pdf"));
	}

	[Test]
	public async Task PopulateUiModel_WhenNoResolutionFilesExist_ShowsNoInfoAnswer()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 111;
		var school = new SchoolApplyingToConvert("No Files School", 33333, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Started"
		};

		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.ApplicationId = applicationId;

		var mockFiles = new List<SharePointFileInfo>
		{
			new() { Name = "other_prefix_file.pdf" },
			new() { Name = "random_document.doc" }
		};
		
		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(mockFiles);

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		var viewModel = pageModel.ViewModel[0];
		var resolutionSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.Resolution);
		Assert.That(resolutionSection, Is.Not.Null);
		Assert.That(resolutionSection.Answer, Is.EqualTo(QuestionAndAnswerConstants.NoInfoAnswer));
	}

	[Test]
	public async Task PopulateUiModel_WithNullProtectedCharacteristics_ShowsNoForEqualitiesAssessment()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 222;
		var school = new SchoolApplyingToConvert("Null Characteristics School", 44444, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Started", // Section is started
			ProtectedCharacteristics = null // But this is null
		};

		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.ApplicationId = applicationId;
		
		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>());

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		var viewModel = pageModel.ViewModel[0];
		var equalitiesSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.EqualitiesImpactAssessment);
		Assert.That(equalitiesSection, Is.Not.Null);
		Assert.That(equalitiesSection.Answer, Is.EqualTo("No"));
	}

	[Test]
	public async Task PopulateUiModel_WithEmptyMainFeederSchools_ShowsNoInfoAnswer()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var applicationId = 333;
		var school = new SchoolApplyingToConvert("Empty Feeders School", 55555, null)
		{
			EntityId = entityId,
			TrustBenefitDetails = "Started",
			MainFeederSchools = "" // Empty string
		};

		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.ApplicationId = applicationId;
		
		var retrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalServiceMock.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(application);

		var sharePointServiceMock = new Mock<ISharePointService>();
		sharePointServiceMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>());

		var pageModel = SetupFurtherInformationSummaryModel(
			sharePointServiceMock.Object,
			retrievalServiceMock.Object);
		pageModel.ApplicationId = applicationId;

		// Act
		await pageModel.PopulateUiModel(school);

		// Assert
		var viewModel = pageModel.ViewModel[0];
		var feederSchoolsSection = viewModel.Sections.Find(s => s.Name == FurtherInformationSectionViewModel.MainFeederSchools);
		Assert.That(feederSchoolsSection, Is.Not.Null);
		Assert.That(feederSchoolsSection.Answer, Is.EqualTo(QuestionAndAnswerConstants.NoInfoAnswer));
	}

	private static FurtherInformationSummaryModel SetupFurtherInformationSummaryModel(
		ISharePointService? sharePointService = null,
		IConversionApplicationRetrievalService? conversionApplicationRetrievalService = null,
		ILogger<FurtherInformationSummaryModel>? logger = null)
	{
		(var pageContext, var tempData, var actionContext) = PageContextFactory.PageContextBuilder(false);

		var model = new FurtherInformationSummaryModel(
			conversionApplicationRetrievalService ?? Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			sharePointService ?? Mock.Of<ISharePointService>(),
			logger ?? Mock.Of<ILogger<FurtherInformationSummaryModel>>()
		)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};

		return model;
	}
}
