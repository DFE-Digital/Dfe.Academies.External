using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.FeatureManagement;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academisation.CorrelationIdMiddleware;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationRetrievalServiceTests
{
	// below hard coded because has to be same as Id in getApplicationResponse.json
	private const int GetApplicationId = 25;
	private const int GetSchoolUrn = 113537;

	[Test]
	public async Task GetPendingApplications___ApiReturns200___Success()
	{
		// arrange
		var expectedJson = @"[
    {
      ""applicationId"": 1,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""InProgress"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""email@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    },
	{
      ""applicationId"": 2,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""InProgress"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""email@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    },
	{
      ""applicationId"": 3,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""InProgress"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""email@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    },
	{
      ""applicationId"": 3,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""Submitted"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""email@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    }
  ]";
		int expectedCount = 3;
		string userEmail = "email@education.gov.uk";
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		var expectedExistingApplicationsTestData = await applicationRetrievalService.GetPendingApplications(userEmail);

		// assert
		Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
		ClassicAssert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
	}

	[Test]
	public async Task GetCompletedApplications___ApiReturns200___Success()
	{
		// arrange
		var expectedJson = @"[
    {
      ""applicationId"": 1,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""InProgress"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""robert@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    },
  {
      ""applicationId"": 2,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""InProgress"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""robert@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    },
  {
      ""applicationId"": 3,
      ""applicationType"": ""FormAMat"",
      ""applicationStatus"": ""Submitted"",
      ""contributors"": [
        {
          ""contributorId"": 1,
          ""firstName"": ""Robert"",
          ""lastName"": ""McHugh"",
          ""emailAddress"": ""robert@education.gov.uk"",
          ""role"": ""Other"",
          ""otherRoleName"": null
        }
      ],
      ""schools"": []
    }
  ]";
		int expectedCount = 1;
		string userEmail = "robert@education.gov.uk";
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		var expectedExistingApplicationsTestData = await applicationRetrievalService.GetCompletedApplications(userEmail);

		// assert
		Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
		ClassicAssert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
	}

	[Test]
	public async Task GetConversionApplicationAuditEntries___ApiReturns200___Success()
	{
		// arrange
		var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO:- will be json from Academies API
		int expectedCount = 3; // TODO: 
		int applicationId = 99; // TODO: 
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		var auditEntries = await applicationRetrievalService.GetConversionApplicationAuditEntries(applicationId);

		// assert
		Assert.That(auditEntries, Is.Not.Null);
		ClassicAssert.AreEqual(expectedCount, auditEntries.Count, "Count is not correct");
	}

	[Test]
	public async Task GetSchoolApplicationComponents___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 8;
		int applicationId = 25;
		int URN = 113537;
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		var applicationComponentStatuses = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

		// assert
		Assert.That(applicationComponentStatuses, Is.Not.Null);
		ClassicAssert.AreEqual(expectedCount, applicationComponentStatuses.Count, "Count is not correct");
	}

	[Test]
	public async Task GetConversionApplicationContributors___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 3; 
		int applicationId = 25; 
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		var applicationContributors = await applicationRetrievalService.GetConversionApplicationContributors(applicationId);

		// assert
		Assert.That(applicationContributors, Is.Not.Null);
		ClassicAssert.AreEqual(expectedCount, applicationContributors.Count, "Count is not correct");
	}

	[Test]
	public async Task GetApplication___JoinAMat___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 1;
		ApplicationStatus status = ApplicationStatus.InProgress;

		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		var application = await applicationRetrievalService.GetApplication(GetApplicationId);

		// assert
		Assert.That(application, Is.Not.Null);
		Assert.That(application.ApplicationId, Is.EqualTo(GetApplicationId));
		Assert.That(application.ApplicationTitle, Is.EqualTo("Join a multi-academy trust A2B_1"));
		Assert.That(application.ApplicationStatus, Is.EqualTo(status));

		ClassicAssert.AreEqual(expectedCount, application.Schools.Count, "Count is not correct");
		Assert.That(application.Schools.FirstOrDefault()?.SchoolName, Is.EqualTo("Plymstock School"));
		Assert.That(application.Schools.FirstOrDefault()?.URN, Is.EqualTo(GetSchoolUrn));
	}
}
