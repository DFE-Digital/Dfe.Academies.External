using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academisation.CorrelationIdMiddleware;
using Dfe.Academies.External.Web.FeatureManagement;

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
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

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
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationNoRoles();

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
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalJoinTrustDetails();

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
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(null);

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
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndChangesToLaGovernanceJoinTrustDetails();

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.InProgress));
	}

	/// <summary>
	/// Status.Completed - all 3 sections complete
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateTrustStatus___JoinTrustDetailsReturns___Completed()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(null);

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.Completed));
	}
}
