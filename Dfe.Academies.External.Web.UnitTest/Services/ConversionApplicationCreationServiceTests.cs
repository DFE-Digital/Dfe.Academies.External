using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.FeatureManagement;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academisation.CorrelationIdMiddleware;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationCreationServiceTests
{
	private static readonly Fixture Fixture = new();
	// below hard coded because has to be same as Id in getApplicationResponse.json
	private const int GetApplicationId = 25;
	private const int GetSchoolUrn = 113537;

	[Test]
	public async Task CreateNewApplication___NoContributor___ApiReturns400___BadRequest()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/createApplicationResponseInValid.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationNoRoles();

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.BadRequest, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();

		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// act / assert
		var ex = Assert.ThrowsAsync<ArgumentException>(() => conversionApplicationCreationService.CreateNewApplication(conversionApplication));

		// now we could test the exception itself
		Assert.That(ex.Message == "Mandatory Contributor Missing");
	}

	[Test]
	public async Task CreateNewApplication___Contributor___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/createApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithOtherRole();

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();

		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// act
		var newApplication = await conversionApplicationCreationService.CreateNewApplication(conversionApplication);

		// assert
		Assert.That(newApplication, Is.Not.Null);
		ClassicAssert.AreEqual(newApplication.ApplicationType, conversionApplication.ApplicationType);
		ClassicAssert.AreEqual(newApplication.ApplicationStatus, conversionApplication.ApplicationStatus);

		ClassicAssert.AreNotEqual(newApplication.ApplicationId, 0);
	}

	/// <summary>
	/// call add school endpoint and mock HttpStatusCode.Created
	/// </summary>
	[Test]
	public async Task AddSchoolToApplication___ApiReturns200___Ok()
	{
		// arrange
		int urn = Fixture.Create<int>();
		string schoolName = Fixture.Create<string>();
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService, 
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		Assert.DoesNotThrowAsync(() => conversionApplicationCreationService.AddSchoolToApplication(GetApplicationId,
			urn,
			schoolName));
	}

	/// <summary>
	/// call add school endpoint and mock HttpStatusCode.InternalServerError
	/// </summary>
	[Test]
	public async Task AddSchoolToApplication___ApiReturns500___InternalServerError()
	{
		// arrange
		int urn = Fixture.Create<int>();
		string schoolName = Fixture.Create<string>();

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService, 
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		Assert.ThrowsAsync<HttpRequestException>(() => conversionApplicationCreationService.AddSchoolToApplication(GetApplicationId,
			urn,
			schoolName));
	}

	[Test]
	public async Task AddSchoolToApplication___InvalidApplicationId___ArgumentException()
	{
		// arrange
		int applicationId = Fixture.Create<int>();
		int urn = Fixture.Create<int>();
		string schoolName = Fixture.Create<string>();
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		var ex = Assert.ThrowsAsync<ArgumentException>(() => conversionApplicationCreationService.AddSchoolToApplication(applicationId,
			urn,
			schoolName));

		// now we could test the exception itself
		Assert.That(ex.Message == "Application not found");
	}

	[Test]
	public async Task PutApplicationDetails__NoApplication__ThrowsArgumentException()
	{
		//Arrange
		var schoolUrn = 123456;

		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();

		var sut = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		//Act
		var result = Assert.ThrowsAsync<ArgumentException>(async () => await sut.PutSchoolApplicationDetails(GetApplicationId, schoolUrn, dictionary));

		//Assert
		ClassicAssert.AreEqual("Application not found", result.Message);

	}
	[Test]
	public async Task PutApplicationDetails__NoMatchingSchool__ThrowsArgumentException()
	{
		//Arrange
		var applicationId = 1;
		var schoolUrn = 123456;

		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();

		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(Fixture.Build<ConversionApplication>().With(x => x.ApplicationId, applicationId).With(x => x.Schools, new List<SchoolApplyingToConvert> { new("test", 1, "") }).Create());

		//Act
		var sut = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService.Object, 
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		var result = Assert.ThrowsAsync<ArgumentException>(async () => await sut.PutSchoolApplicationDetails(applicationId, schoolUrn, dictionary));
		ClassicAssert.AreEqual("School not found", result.Message);
	}

	[Test]
	public async Task PutApplicationDetails__ApiReturns200__Ok()
	{
		//Arrange
		var schoolUrn = 123456;

		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();

		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(GetApplicationId))
			.ReturnsAsync(Fixture.Build<ConversionApplication>().With(x => x.ApplicationId, GetApplicationId).With(x => x.Schools, new List<SchoolApplyingToConvert> { new("test", schoolUrn, "") }).Create());

		var sut = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		//Act
		Assert.DoesNotThrowAsync(async () => await sut.PutSchoolApplicationDetails(GetApplicationId, schoolUrn, dictionary));
	}

	/// <summary>
	/// call ApplicationSchoolNextFinancialYear service func and mock HttpStatusCode.Created
	/// </summary>
	[Test]
	public async Task ApplicationSchoolNextFinancialYear___ApiReturns200___Ok()
	{
		// arrange
		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		Assert.DoesNotThrowAsync(() => conversionApplicationCreationService.PutSchoolApplicationDetails(
			GetApplicationId,
			GetSchoolUrn,
			dictionary
		));
	}

	[Test]
	public async Task ApplicationSchoolNextFinancialYear__ApiReturns500___InternalServerError()
	{
		// arrange
		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		
		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		Assert.ThrowsAsync<HttpRequestException>(() => conversionApplicationCreationService.PutSchoolApplicationDetails(
			GetApplicationId,
			GetSchoolUrn,
			dictionary
		));
	}
	/// <summary>
	/// call ApplicationSchoolPreviousFinancialYear service func and mock HttpStatusCode.Created
	/// </summary>
	[Test]
	public async Task ApplicationSchoolPreviousFinancialYear___ApiReturns200___Ok()
	{
		// arrange
		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		
		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		Assert.DoesNotThrowAsync(() => conversionApplicationCreationService.PutSchoolApplicationDetails(
			GetApplicationId,
			GetSchoolUrn,
			dictionary
		));
	}

	[Test]
	public async Task ApplicationSchoolPreviousFinancialYear__ApiReturns500___InternalServerError()
	{
		// arrange
		var fixture = Fixture.Create<SchoolApplyingToConvert>();
		var properties = fixture.GetType().GetProperties();
		var dictionary = properties.ToDictionary<PropertyInfo?, string, dynamic>(prop => prop.Name, prop => prop.GetValue(fixture));

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object, mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
		
		// act
		var conversionApplicationCreationService = new ConversionApplicationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService,
			Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		Assert.ThrowsAsync<HttpRequestException>(() => conversionApplicationCreationService.PutSchoolApplicationDetails(
			GetApplicationId,
			GetSchoolUrn,
			dictionary
		));
	}

	//public async Task UpdateDraftApplication___OtherRole___Success()
	//{
	//    // arrange
	//    var expectedJson = @"{ ""foo"": ""bar"" }"; 
	//    var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
	//    var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
	//    var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithOtherRole();

	//    // act
	//    var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

	//    // assert
	//    Assert.DoesNotThrowAsync(() => recordModelService.UpdateDraftApplication(trustApplicationDto));
	//}

	//public async Task UpdateDraftApplication___ChairRole___Success()
	//{
	//    // arrange
	//    var expectedJson = @"{ ""foo"": ""bar"" }"; 
	//    var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
	//    var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
	//    var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

	//    // act
	//    var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

	//    // assert
	//    Assert.DoesNotThrowAsync(() => recordModelService.UpdateDraftApplication(trustApplicationDto));
	//}

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
