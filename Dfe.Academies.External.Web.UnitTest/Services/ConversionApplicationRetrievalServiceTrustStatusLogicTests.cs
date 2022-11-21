using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationRetrievalServiceTrustStatusLogicTests
{
	/// <summary>
	/// conversionApplication == null && conversionApplication.JoinTrustDetails == null
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateTrustStatus___ConversionApplicationNullReturns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(null);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.NotStarted));
	}

	/// <summary>
	/// conversionApplication != null && conversionApplication.JoinTrustDetails == null
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateTrustStatus___JoinTrustDetailsNullReturns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
		
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationNoRoles();

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.NotStarted));
	}

	// conversionApplication != null && conversionApplication.JoinTrustDetails != null (but empty)
	// = not applicable because new ExistingTrust() because of mandatory ctor params

	/// <summary>
	/// Status.InProgress - section 1 = done = conversionApplication.JoinTrustDetails.TrustName ONLY
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateTrustStatus___JoinTrustDetailsTrustNameOnlyReturns___InProgress()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithMinimalJoinTrustDetails();

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.InProgress));
	}

	/// <summary>
	/// Status.InProgress - section 2 = done = conversionApplication.JoinTrustDetails!.ChangesToTrust.HasValue && minimal 
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateTrustStatus___JoinTrustDetailsMinimalAndTrustChangesReturns___InProgress()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails();

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.InProgress));
	}

	/// <summary>
	/// Status.InProgress - section 3 = done conversionApplication.JoinTrustDetails!.ChangesToLaGovernance.HasValue && minimal
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateTrustStatus___JoinTrustDetailsMinimalAndChangesToLaGovernanceReturns___InProgress()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithMinimalAndChangesToLaGovernanceJoinTrustDetails();

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.InProgress));
	}

	// TODO:- Status.Completed - all 3 sections complete

	// TODO:- add tests for CalculateApplicationDeclarationStatus() = returns NotStarted

	// TODO:- add tests for CalculateApplicationDeclarationStatus() = returns InProgress

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
