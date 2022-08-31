using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationCreationServiceTests
{
	private static readonly Fixture Fixture = new();

	[Test]
    public async Task CreateNewApplication___NoContributor___ApiReturns400___BadRequest()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/createApplicationResponseInValid.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationNoRoles();

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.BadRequest, expectedJson);
        var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		
		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object, 
			mockConversionApplicationRetrievalService.Object);

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
		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithOtherRole();

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
	    var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();

		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService.Object);

		// act
		var newApplication = await conversionApplicationCreationService.CreateNewApplication(conversionApplication);

	    // assert
	    Assert.That(newApplication, Is.Not.Null);
	    Assert.AreEqual(newApplication.ApplicationType, conversionApplication.ApplicationType);
	    Assert.AreEqual(newApplication.ApplicationStatus, conversionApplication.ApplicationStatus);

	    Assert.AreNotEqual(newApplication.ApplicationId, 0);
    }

	/// <summary>
	/// call add school endpoint and mock HttpStatusCode.Created
	/// </summary>
	[Test]
	public async Task AddSchoolToApplication___ApiReturns200___Ok()
    {
	    // arrange
	    int applicationId = 1; // hard coded because has to be same as example JSON
        int urn = Fixture.Create<int>();
        string schoolName = Fixture.Create<string>();
        string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
        string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		
		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
        var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService);

	    // assert
	    Assert.DoesNotThrowAsync(() => conversionApplicationCreationService.AddSchoolToApplication(applicationId, urn, schoolName));
    }

    /// <summary>
    /// call add school endpoint and mock HttpStatusCode.InternalServerError
    /// </summary>
    [Test]
    public async Task AddSchoolToApplication___ApiReturns500___InternalServerError()
    {
		// arrange
		int applicationId = 1; // hard coded because has to be same as example JSON
		int urn = Fixture.Create<int>();
	    string schoolName = Fixture.Create<string>();

	    string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
	    string expectedJson = await File.ReadAllTextAsync(fullFilePath);

	    var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, string.Empty);
	    var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
	    var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
	    var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

	    // act
	    var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
		    mockLoggerCreationService.Object,
		    mockConversionApplicationRetrievalService);

		// assert
		Assert.ThrowsAsync<HttpRequestException>(() => conversionApplicationCreationService.AddSchoolToApplication(applicationId, urn, schoolName));
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
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
	    var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
	    var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

	    // act
	    var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
		    mockLoggerCreationService.Object,
		    mockConversionApplicationRetrievalService);

		// assert
		var ex = Assert.ThrowsAsync<ArgumentException>(() => conversionApplicationCreationService.AddSchoolToApplication(applicationId, urn, schoolName));

		// now we could test the exception itself
		Assert.That(ex.Message == "Application not found");
	}

	/// <summary>
	/// call endpoint and mock HttpStatusCode.Created
	/// </summary>
	//[Test]
	public async Task ApplicationAddJoinTrustReasons___ApiReturns200___Ok()
    {
		// arrange
		int applicationId = 2; // hard coded because has to be same as example JSON
		int urn = Fixture.Create<int>();
		string applicationAddJoinTrustReason = Fixture.Create<string>();

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService);

		// assert
		Assert.DoesNotThrowAsync(() => conversionApplicationCreationService.ApplicationAddJoinTrustReasons(applicationId, 
																								applicationAddJoinTrustReason, urn));
    }

    /// <summary>
    /// call endpoint and mock HttpStatusCode.InternalServerError
    /// </summary>
    //[Test]
    public async Task ApplicationAddJoinTrustReasons___ApiReturns500___InternalServerError()
    {
		// arrange
		int applicationId = 2; // hard coded because has to be same as example JSON
		int urn = Fixture.Create<int>();
		string applicationAddJoinTrustReason = Fixture.Create<string>();

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => conversionApplicationCreationService.ApplicationAddJoinTrustReasons(applicationId, 
																									applicationAddJoinTrustReason, urn));

        // now we could test the exception itself
        //Assert.That(ex.Message == "Blah");
    }

	// TODO MR:- AddTrustToApplication - ApiReturns200___Ok()

	// TODO MR:- AddTrustToApplication - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationChangeSchoolNameAndReason - ApiReturns200___Ok()

	// TODO MR:- ApplicationChangeSchoolNameAndReason - ApiReturns500___InternalServerError()

	[Test]
	public async Task ApplicationSchoolTargetConversionDate___ApiReturns200___Ok()
	{
		// arrange
		int applicationId = 2; // hard coded because has to be same as example JSON
		int urn = Fixture.Create<int>();
		DateTime targetDate = Fixture.Create<DateTime>();
		string targetDateExplained = Fixture.Create<string>();

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService);

		// assert
		Assert.DoesNotThrowAsync(() => conversionApplicationCreationService.ApplicationSchoolTargetConversionDate(
			applicationId, 
			urn,
			targetDate,
			targetDateExplained
			));
	}

	[Test]
	public async Task ApplicationSchoolTargetConversionDate___ApiReturns500___InternalServerError()
	{
		// arrange
		int applicationId = 2; // hard coded because has to be same as example JSON
		int urn = Fixture.Create<int>();
		DateTime targetDate = Fixture.Create<DateTime>();
		string targetDateExplained = Fixture.Create<string>();

		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockCreationHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, string.Empty);
		var mockRetrievalHttpClientFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLoggerCreationService = new Mock<ILogger<ConversionApplicationCreationService>>();
		var mockLoggerRetrievalService = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var mockConversionApplicationRetrievalService = new ConversionApplicationRetrievalService(mockRetrievalHttpClientFactory.Object, mockLoggerRetrievalService.Object);

		// act
		var conversionApplicationCreationService = new ConversionApplicationCreationService(mockCreationHttpClientFactory.Object,
			mockLoggerCreationService.Object,
			mockConversionApplicationRetrievalService);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => conversionApplicationCreationService.ApplicationSchoolTargetConversionDate(
			applicationId,
			urn,
			targetDate,
			targetDateExplained
			));

		// now we could test the exception itself
		//Assert.That(ex.Message == "Blah");
	}



	// TODO MR:- ApplicationSchoolFuturePupilNumbers - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolFuturePupilNumbers - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolContacts - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolContacts - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolLandAndBuildings - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolLandAndBuildings - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationPreOpeningSupportGrantUpdate - ApiReturns200___Ok()

	// TODO MR:- ApplicationPreOpeningSupportGrantUpdate - ApiReturns500___InternalServerError()

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
