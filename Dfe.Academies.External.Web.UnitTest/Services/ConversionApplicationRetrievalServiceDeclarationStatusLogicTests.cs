using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
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
internal sealed class ConversionApplicationRetrievalServiceDeclarationStatusLogicTests
{
	/// <summary>
	/// conversionApplication == null
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ConversionApplicationNullReturns___NotStarted()
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
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(null);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
	}

	/// <summary>
	/// conversionApplication.ApplicationType == ApplicationTypes.JoinAMat - without school
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ApplicationTypeJoinAMatAndNoSchool___Returns___NotStarted()
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
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(conversionApplication);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
	}

	/// <summary>
	/// conversionApplication.ApplicationType == ApplicationTypes.JoinAMat - with school
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ApplicationTypeJoinAMatAndSchool___Returns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(null);

		// act
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(conversionApplication);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
	}

	/// <summary>
	/// add tests for CalculateApplicationDeclarationStatus() = returns Completed = selectedSchool?.DeclarationBodyAgree.HasValue 
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ApplicationTypeJoinAMatAndSchoolAndDeclarationBodyAgree___Returns___Completed()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(null);
		var applicationSchool = conversionApplication.Schools.FirstOrDefault()!.DeclarationBodyAgree = true;
		
		// act
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(conversionApplication);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.Completed));
	}

	/// <summary>
	/// conversionApplication.ApplicationType == ApplicationTypes.FormAMat = BuildMinimalFormAMatConversionApplicationNoContributors()
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ApplicationTypeFormAMatAndNoSchool___Returns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildMinimalFormAMatConversionApplicationNoContributors();

		// act
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(conversionApplication);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
	}

	/// <summary>
	/// conversionApplication.ApplicationType == ApplicationTypes.FormAMat - with school
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ApplicationTypeFormAMatAndSchool___Returns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildFormAMatConversionApplicationWithContributorWithSchool();

		// act
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(conversionApplication);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
	}

	// TODO:- other tests when the know the FormAMat logic
}
