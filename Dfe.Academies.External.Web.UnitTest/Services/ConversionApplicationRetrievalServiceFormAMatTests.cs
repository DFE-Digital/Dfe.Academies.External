using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.FeatureManagement;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using GovUK.Dfe.CoreLibs.Http.Interfaces;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using GovUK.Dfe.CoreLibs.SharePoint.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationRetrievalServiceFormAMatTests
{
	private const int ApplicationId = 42;
	private const string ApplicationReference = "A2B_42";
	private const string EntityId = "11111111-1111-1111-1111-111111111111";

	#region GetFormAMatTrustComponents Tests

	[Test]
	public async Task GetFormAMatTrustComponents_ValidApplication_Returns7Components()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson();
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		Assert.That(components, Is.Not.Null);
		ClassicAssert.AreEqual(7, components.Count);
		
		var componentNames = components.Select(c => c.Name).ToList();
		Assert.That(componentNames, Contains.Item("Name of the trust"));
		Assert.That(componentNames, Contains.Item("Opening date"));
		Assert.That(componentNames, Contains.Item("Reasons for forming the trust"));
		Assert.That(componentNames, Contains.Item("Plans for growth"));
		Assert.That(componentNames, Contains.Item("School improvement strategy"));
		Assert.That(componentNames, Contains.Item("Governance structure"));
		Assert.That(componentNames, Contains.Item("Key people"));
	}

	[Test]
	public async Task GetFormAMatTrustComponents_ApplicationNotFound_ReturnsEmptyList()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(applicationId: 999);
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		Assert.That(components, Is.Not.Null);
		Assert.That(components, Is.Empty);
	}

	[Test]
	public async Task GetFormAMatTrustComponents_ExceptionThrown_ReturnsEmptyList()
	{
		// Arrange
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, "Server Error");
		var service = CreateService(mockFactory, new Mock<ISharePointService>());

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		Assert.That(components, Is.Not.Null);
		Assert.That(components, Is.Empty);
	}

	#endregion

	#region GetAllApplications Tests

	[Test]
	public async Task GetAllApplications_ApiReturns200_ReturnsApplications()
	{
		// Arrange
		string fullFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExampleJsonResponses", "getAllApplicationsResponse.json");
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var service = CreateService(expectedJson, out _);

		// Act
		var result = await service.GetAllApplications();

		// Assert
		Assert.That(result, Is.Not.Null);
		ClassicAssert.AreEqual(2, result.Count);
		Assert.That(result[0].ApplicationReference, Is.EqualTo("A2B_1"));
		ClassicAssert.AreEqual(1, result[0].SchoolSharepointServiceModels.Count);
		Assert.That(result[0].SchoolSharepointServiceModels[0].Name, Is.EqualTo("Test School"));
		Assert.That(result[1].ApplicationReference, Is.EqualTo("A2B_2"));
		Assert.That(result[1].SchoolSharepointServiceModels, Is.Empty);
	}

	[Test]
	public async Task GetAllApplications_ApiReturnsError_ReturnsEmptyList()
	{
		// Arrange
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, "Server Error");
		var service = CreateService(mockFactory, new Mock<ISharePointService>());

		// Act
		var result = await service.GetAllApplications();

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	[Test]
	public async Task GetAllApplications_InvalidJsonResponse_ReturnsEmptyList()
	{
		// Arrange
		var invalidJson = "{ invalid json response }";
		var service = CreateService(invalidJson, out _);

		// Act
		var result = await service.GetAllApplications();

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	#endregion

	#region CalculateOpeningDateSectionStatus Tests

	[Test]
	public void CalculateOpeningDateSectionStatus_AllFieldsEmpty_ReturnsNotStarted()
	{
		// Arrange
		var service = CreateService("{}", out _);
		var trustDetails = new NewTrust();

		// Act
		var status = service.CalculateOpeningDateSectionStatus(trustDetails);

		// Assert
		Assert.That(status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public void CalculateOpeningDateSectionStatus_OnlyOpeningDateProvided_ReturnsInProgress()
	{
		// Arrange
		var service = CreateService("{}", out _);
		var trustDetails = new NewTrust
		{
			FormTrustOpeningDate = DateTime.Now.Date
		};

		// Act
		var status = service.CalculateOpeningDateSectionStatus(trustDetails);

		// Assert
		Assert.That(status, Is.EqualTo(Status.InProgress));
	}

	[Test]
	public void CalculateOpeningDateSectionStatus_TwoFieldsProvided_ReturnsInProgress()
	{
		// Arrange
		var service = CreateService("{}", out _);
		var trustDetails = new NewTrust
		{
			FormTrustOpeningDate = DateTime.Now.Date,
			TrustApproverName = "John Doe"
		};

		// Act
		var status = service.CalculateOpeningDateSectionStatus(trustDetails);

		// Assert
		Assert.That(status, Is.EqualTo(Status.InProgress));
	}

	[Test]
	public void CalculateOpeningDateSectionStatus_AllFieldsProvided_ReturnsCompleted()
	{
		// Arrange
		var service = CreateService("{}", out _);
		var trustDetails = new NewTrust
		{
			FormTrustOpeningDate = DateTime.Now.Date,
			TrustApproverName = "John Doe",
			TrustApproverEmail = "john.doe@education.gov.uk"
		};

		// Act
		var status = service.CalculateOpeningDateSectionStatus(trustDetails);

		// Assert
		Assert.That(status, Is.EqualTo(Status.Completed));
	}

	[Test]
	public void CalculateOpeningDateSectionStatus_OnlyApproverFieldsProvided_ReturnsInProgress()
	{
		// Arrange
		var service = CreateService("{}", out _);
		var trustDetails = new NewTrust
		{
			TrustApproverName = "John Doe",
			TrustApproverEmail = "john.doe@education.gov.uk"
		};

		// Act
		var status = service.CalculateOpeningDateSectionStatus(trustDetails);

		// Assert
		Assert.That(status, Is.EqualTo(Status.InProgress));
	}

	#endregion

	#region Status Calculation Tests via GetFormAMatTrustComponents

	[Test]
	public async Task CalculateReasonsForFormingTrustSectionStatus_EmptyReason_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustReasonForming: null);
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var reasonsComponent = GetComponentByName(components, "Reasons for forming the trust");
		Assert.That(reasonsComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public async Task CalculateReasonsForFormingTrustSectionStatus_WithReason_ReturnsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustReasonForming: "To improve educational outcomes");
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var reasonsComponent = GetComponentByName(components, "Reasons for forming the trust");
		Assert.That(reasonsComponent.Status, Is.EqualTo(Status.Completed));
	}

	[Test]
	public async Task CalculatePlansForGrowthSectionStatus_EmptyPlans_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustPlanForGrowth: null, formTrustPlansForNoGrowth: null);
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var plansComponent = GetComponentByName(components, "Plans for growth");
		Assert.That(plansComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public async Task CalculatePlansForGrowthSectionStatus_WithGrowthPlan_ReturnsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustPlanForGrowth: "Plan to grow by 3 schools");
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var plansComponent = GetComponentByName(components, "Plans for growth");
		Assert.That(plansComponent.Status, Is.EqualTo(Status.Completed));
	}

	[Test]
	public async Task CalculatePlansForGrowthSectionStatus_WithNoGrowthPlan_ReturnsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustPlansForNoGrowth: "No plans for growth");
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var plansComponent = GetComponentByName(components, "Plans for growth");
		Assert.That(plansComponent.Status, Is.EqualTo(Status.Completed));
	}

	[Test]
	public async Task CalculateSchoolImprovementStrategyStatus_EmptyStrategy_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustImprovementStrategy: null);
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var strategyComponent = GetComponentByName(components, "School improvement strategy");
		Assert.That(strategyComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public async Task CalculateSchoolImprovementStrategyStatus_WithStrategy_ReturnsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(formTrustImprovementStrategy: "Peer review and support model");
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var strategyComponent = GetComponentByName(components, "School improvement strategy");
		Assert.That(strategyComponent.Status, Is.EqualTo(Status.Completed));
	}

	[Test]
	public async Task CalculateKeyPeopleSectionStatus_NoKeyPeople_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(includeKeyPerson: false);
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var keyPeopleComponent = GetComponentByName(components, "Key people");
		Assert.That(keyPeopleComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public async Task CalculateKeyPeopleSectionStatus_WithKeyPeople_ReturnsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(includeKeyPerson: true);
		var service = CreateService(applicationJson, out _);

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var keyPeopleComponent = GetComponentByName(components, "Key people");
		Assert.That(keyPeopleComponent.Status, Is.EqualTo(Status.Completed));
	}

	[Test]
	public async Task CalculateGovernanceStructureSectionStatus_NoGovernanceFiles_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson();
		var service = CreateService(applicationJson, out var mockSharePoint);
		mockSharePoint
			.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>());

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var governanceComponent = GetComponentByName(components, "Governance structure");
		Assert.That(governanceComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public async Task CalculateGovernanceStructureSectionStatus_WithGovernanceFile_ReturnsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson();
		var service = CreateService(applicationJson, out var mockSharePoint);
		var expectedFolder = FileUploadConstants.FormatSharepointApplicationDirectory(ApplicationReference, EntityId);
		mockSharePoint
			.Setup(x => x.ListFilesAsync(expectedFolder))
			.ReturnsAsync(new List<SharePointFileInfo>
			{
				new() { Name = $"{FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName}_governance.pdf" }
			});

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var governanceComponent = GetComponentByName(components, "Governance structure");
		Assert.That(governanceComponent.Status, Is.EqualTo(Status.Completed));
		mockSharePoint.Verify(x => x.ListFilesAsync(expectedFolder), Times.Once);
	}

	[Test]
	public async Task CalculateGovernanceStructureSectionStatus_SharePointException_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson();
		var service = CreateService(applicationJson, out var mockSharePoint);
		mockSharePoint
			.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ThrowsAsync(new Exception("SharePoint error"));

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var governanceComponent = GetComponentByName(components, "Governance structure");
		Assert.That(governanceComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	[Test]
	public async Task CalculateGovernanceStructureSectionStatus_NonMatchingFiles_ReturnsNotStarted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson();
		var service = CreateService(applicationJson, out var mockSharePoint);
		mockSharePoint
			.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>
			{
				new() { Name = "other_file.pdf" },
				new() { Name = "another_document.docx" }
			});

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		var governanceComponent = GetComponentByName(components, "Governance structure");
		Assert.That(governanceComponent.Status, Is.EqualTo(Status.NotStarted));
	}

	#endregion

	#region Integration Tests

	[Test]
	public async Task GetFormAMatTrustComponents_CompleteApplication_AllComponentsCompleted()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(
			formTrustReasonForming: "To improve outcomes",
			formTrustPlanForGrowth: "Grow by 2 schools",
			formTrustImprovementStrategy: "Peer review model",
			includeKeyPerson: true,
			includeOpeningDate: true,
			includeApprover: true);
		
		var service = CreateService(applicationJson, out var mockSharePoint);
		var expectedFolder = FileUploadConstants.FormatSharepointApplicationDirectory(ApplicationReference, EntityId);
		mockSharePoint
			.Setup(x => x.ListFilesAsync(expectedFolder))
			.ReturnsAsync(new List<SharePointFileInfo>
			{
				new() { Name = $"{FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName}_file.pdf" }
			});

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		Assert.That(components.Count, Is.EqualTo(7));
		foreach (var component in components)
		{
			if (component.Name == "Name of the trust") // This one always returns Completed if trust name exists
				Assert.That(component.Status, Is.EqualTo(Status.Completed));
			else
				Assert.That(component.Status, Is.EqualTo(Status.Completed), $"Component '{component.Name}' should be completed");
		}
	}

	[Test]
	public async Task GetFormAMatTrustComponents_PartialApplication_MixedStatuses()
	{
		// Arrange
		var applicationJson = CreateFormAMatApplicationJson(
			formTrustReasonForming: "To improve outcomes", // Completed
			formTrustPlanForGrowth: null, // Not started
			formTrustImprovementStrategy: null, // Not started
			includeKeyPerson: true, // Completed
			includeOpeningDate: false, // Not started
			includeApprover: false);
		
		var service = CreateService(applicationJson, out var mockSharePoint);
		mockSharePoint
			.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>()); // No governance files

		// Act
		var components = await service.GetFormAMatTrustComponents(ApplicationId);

		// Assert
		Assert.That(GetComponentByName(components, "Reasons for forming the trust").Status, Is.EqualTo(Status.Completed));
		Assert.That(GetComponentByName(components, "Plans for growth").Status, Is.EqualTo(Status.NotStarted));
		Assert.That(GetComponentByName(components, "School improvement strategy").Status, Is.EqualTo(Status.NotStarted));
		Assert.That(GetComponentByName(components, "Key people").Status, Is.EqualTo(Status.Completed));
		Assert.That(GetComponentByName(components, "Opening date").Status, Is.EqualTo(Status.NotStarted));
		Assert.That(GetComponentByName(components, "Governance structure").Status, Is.EqualTo(Status.NotStarted));
	}

	#endregion

	#region Helper Methods

	private static ApplicationComponentViewModel GetComponentByName(List<ApplicationComponentViewModel> components, string name)
	{
		return components.Single(c => c.Name == name);
	}

	private static ConversionApplicationRetrievalService CreateService(string expectedJson, out Mock<ISharePointService> mockSharePoint)
	{
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		mockSharePoint = new Mock<ISharePointService>();
		mockSharePoint
			.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<SharePointFileInfo>());
		return CreateService(mockFactory, mockSharePoint);
	}

	private static ConversionApplicationRetrievalService CreateService(Mock<IHttpClientFactory> mockFactory, Mock<ISharePointService> mockSharePoint)
	{
		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		return new ConversionApplicationRetrievalService(
			mockFactory.Object,
			mockLogger.Object,
			mockSharePoint.Object,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()),
			mockConversionGrantExpiryFeature.Object);
	}

	private static string CreateFormAMatApplicationJson(
		int applicationId = ApplicationId,
		string? formTrustReasonForming = null,
		string? formTrustPlanForGrowth = null,
		string? formTrustPlansForNoGrowth = null,
		string? formTrustImprovementStrategy = null,
		bool includeKeyPerson = false,
		bool includeOpeningDate = false,
		bool includeApprover = false)
	{
		var keyPeopleJson = includeKeyPerson
			? @"[
				{
					""name"": ""John Doe"",
					""dateOfBirth"": ""1980-01-01T00:00:00"",
					""biography"": ""Experienced headteacher"",
					""roles"": [
						{
							""role"": ""ceo"",
							""timeInRole"": ""3 years""
						}
					]
				}
			]"
			: "[]";

		string JsonValueOrNull(string? value) => value == null ? "null" : $"\"{value}\"";
		string DateValueOrNull(bool include) => include ? $"\"{DateTime.Now.Date:yyyy-MM-dd}T00:00:00\"" : "null";

		return $@"{{
			""applicationId"": {applicationId},
			""applicationType"": ""formAMat"",
			""applicationStatus"": ""inProgress"",
			""applicationReference"": ""{ApplicationReference}"",
			""entityId"": ""{EntityId}"",
			""contributors"": [],
			""schools"": [],
			""formTrustDetails"": {{
				""applicationId"": {applicationId},
				""applicationReference"": ""{ApplicationReference}"",
				""formTrustProposedNameOfTrust"": ""New Trust Name"",
				""formTrustOpeningDate"": {DateValueOrNull(includeOpeningDate)},
				""trustApproverName"": {JsonValueOrNull(includeApprover ? "John Approver" : null)},
				""trustApproverEmail"": {JsonValueOrNull(includeApprover ? "john.approver@education.gov.uk" : null)},
				""formTrustReasonForming"": {JsonValueOrNull(formTrustReasonForming)},
				""formTrustPlanForGrowth"": {JsonValueOrNull(formTrustPlanForGrowth)},
				""formTrustPlansForNoGrowth"": {JsonValueOrNull(formTrustPlansForNoGrowth)},
				""formTrustImprovementStrategy"": {JsonValueOrNull(formTrustImprovementStrategy)},
				""keyPeople"": {keyPeopleJson}
			}}
		}}";
	}

	#endregion
}
