using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationRetrievalServiceTests
{
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
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var expectedExistingApplicationsTestData = await applicationRetrievalService.GetPendingApplications(userEmail);

		// assert
		Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
		Assert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
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
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var expectedExistingApplicationsTestData = await applicationRetrievalService.GetCompletedApplications(userEmail);

		// assert
		Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
		Assert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
	}

	[Test]
	public async Task GetConversionApplicationAuditEntries___ApiReturns200___Success()
	{
		// arrange
		var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
		int expectedCount = 3; // TODO: 
		int applicationId = 99; // TODO: 
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var auditEntries = await applicationRetrievalService.GetConversionApplicationAuditEntries(applicationId);

		// assert
		Assert.That(auditEntries, Is.Not.Null);
		Assert.AreEqual(expectedCount, auditEntries.Count, "Count is not correct");
	}

	[Test]
	public async Task GetSchoolApplicationComponents___ApiReturns200___Success()
	{
		// arrange
		var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
		int expectedCount = 8; // TODO: 
		int applicationId = 99; // TODO: 
		int URN = 96; // TODO: 
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var applicationComponentStatuses = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

		// assert
		Assert.That(applicationComponentStatuses, Is.Not.Null);
		Assert.AreEqual(expectedCount, applicationComponentStatuses.Count, "Count is not correct");
	}

	[Test]
	public async Task GetConversionApplicationContributors___ApiReturns200___Success()
	{
		// arrange
		var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
		int expectedCount = 2; // TODO: 
		int applicationId = 1; // TODO: 
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var applicationContributors = await applicationRetrievalService.GetConversionApplicationContributors(applicationId);

		// assert
		Assert.That(applicationContributors, Is.Not.Null);
		Assert.AreEqual(expectedCount, applicationContributors.Count, "Count is not correct");
	}

	[Test]
	public async Task GetApplication___FormASat___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseFormASAT.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int applicationId = 2;
		int expectedCount = 1;
		int expectedURN = 123332;
		ApplicationStatus status = ApplicationStatus.InProgress;
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var application = await applicationRetrievalService.GetApplication(applicationId);

		// assert
		Assert.That(application, Is.Not.Null);
		Assert.That(application.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(application.ApplicationTitle, Is.EqualTo("Form new single academy trust A2B_2"));
		Assert.That(application.ApplicationStatus, Is.EqualTo(status));

		Assert.AreEqual(expectedCount, application.Schools.Count, "Count is not correct");
		Assert.That(application.Schools.FirstOrDefault()?.SchoolName, Is.EqualTo("string"));
		Assert.That(application.Schools.FirstOrDefault()?.URN, Is.EqualTo(expectedURN));
	}

	[Test]
	public async Task GetApplication___JoinAMat___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int applicationId = 1;
		int expectedCount = 1;
		int expectedURN = 123332;
		ApplicationStatus status = ApplicationStatus.InProgress;

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

		// act
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		var application = await applicationRetrievalService.GetApplication(applicationId);

		// assert
		Assert.That(application, Is.Not.Null);
		Assert.That(application.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(application.ApplicationTitle, Is.EqualTo("Join a multi-academy trust A2B_1"));
		Assert.That(application.ApplicationStatus, Is.EqualTo(status));

		Assert.AreEqual(expectedCount, application.Schools.Count, "Count is not correct");
		Assert.That(application.Schools.FirstOrDefault()?.SchoolName, Is.EqualTo("Frank Wise School"));
		Assert.That(application.Schools.FirstOrDefault()?.URN, Is.EqualTo(expectedURN));
	}

	private static Mock<IHttpClientFactory> SetupMockHttpClientFactory(HttpStatusCode expectedStatusCode, string expectedJson)
	{
		var mockFactory = new Mock<IHttpClientFactory>();

		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = expectedStatusCode,
				Content = new StringContent(expectedJson)
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(APIConstants.AcademiesAPITestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		return mockFactory;
	}
}
