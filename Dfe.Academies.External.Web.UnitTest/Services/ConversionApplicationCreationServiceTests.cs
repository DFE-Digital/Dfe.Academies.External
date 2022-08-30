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
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/newApplicationBodyNoContributor.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationNoRoles();

        // act
        var conversionApplicationCreationService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);
        var newApplication = await conversionApplicationCreationService.CreateNewApplication(conversionApplication);

        // assert
        Assert.That(newApplication, Is.Not.Null);
        Assert.AreEqual(newApplication.ApplicationType, conversionApplication.ApplicationType);
        Assert.AreEqual(newApplication.UserEmail, conversionApplication.UserEmail);
        Assert.AreEqual(newApplication.ApplicationTitle, conversionApplication.ApplicationTitle);
        Assert.AreNotEqual(newApplication.ApplicationId, 0);
    }

    [Test]
    public async Task CreateNewApplication___Contributor___ApiReturns200___Success()
    {
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/newApplicationBodyValid.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
	    var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
	    var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithOtherRole();

	    // act
	    var conversionApplicationCreationService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);
	    var newApplication = await conversionApplicationCreationService.CreateNewApplication(conversionApplication);

	    // assert
	    Assert.That(newApplication, Is.Not.Null);
	    Assert.AreEqual(newApplication.ApplicationType, conversionApplication.ApplicationType);
	    Assert.AreEqual(newApplication.UserEmail, conversionApplication.UserEmail);
	    Assert.AreEqual(newApplication.ApplicationTitle, conversionApplication.ApplicationTitle);
	    Assert.AreNotEqual(newApplication.ApplicationId, 0);
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

	/// <summary>
	/// call add school endpoint and mock HttpStatusCode.Created
	/// </summary>
	//[Test]
	public async Task AddSchoolToApplication___ApiReturns200___Ok()
    {
	    // arrange
	    var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
	    var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
	    int applicationId = Fixture.Create<int>();
        int urn = Fixture.Create<int>();
        string schoolName = Fixture.Create<string>();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

	    // assert
	    Assert.DoesNotThrowAsync(() => recordModelService.AddSchoolToApplication(applicationId, urn, schoolName));
    }

    /// <summary>
    /// call add school endpoint and mock HttpStatusCode.InternalServerError
    /// </summary>
    //[Test]
    public async Task AddSchoolToApplication___ApiReturns500___InternalServerError()
    {
	    // arrange
	    var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, expectedJson);
	    var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
	    int applicationId = Fixture.Create<int>();
	    int urn = Fixture.Create<int>();
	    string schoolName = Fixture.Create<string>();

	    // act
	    var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(() => recordModelService.AddSchoolToApplication(applicationId, urn, schoolName));

        // now we could test the exception itself
        //Assert.That(ex.Message == "Blah");
    }

    /// <summary>
    /// call endpoint and mock HttpStatusCode.Created
    /// </summary>
    //[Test]
    public async Task ApplicationAddJoinTrustReasons___ApiReturns200___Ok()
    {
        // arrange
        var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        string ApplicationAddJoinTrustReason = Fixture.Create<string>();
        int applicationId = Fixture.Create<int>();
		int urn = Fixture.Create<int>();

		// act
		var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        Assert.DoesNotThrowAsync(() => recordModelService.ApplicationAddJoinTrustReasons(applicationId, ApplicationAddJoinTrustReason, urn));
    }

    /// <summary>
    /// call endpoint and mock HttpStatusCode.InternalServerError
    /// </summary>
    //[Test]
    public async Task ApplicationAddJoinTrustReasons___ApiReturns500___InternalServerError()
    {
        // arrange
        var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        int applicationId = Fixture.Create<int>();
        int urn = Fixture.Create<int>();
		string ApplicationAddJoinTrustReason = Fixture.Create<string>();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(() => recordModelService.ApplicationAddJoinTrustReasons(applicationId, ApplicationAddJoinTrustReason, urn));

        // now we could test the exception itself
        //Assert.That(ex.Message == "Blah");
    }

	// TODO MR:- ApplicationChangeSchoolNameAndReason - ApiReturns200___Ok()

	// TODO MR:- ApplicationChangeSchoolNameAndReason - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolTargetConversionDate - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolTargetConversionDate - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolTargetConversionDate - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolTargetConversionDate - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolFuturePupilNumbers - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolFuturePupilNumbers - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolContacts - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolContacts - ApiReturns500___InternalServerError()

	// TODO MR:- ApplicationSchoolLandAndBuildings - ApiReturns200___Ok()

	// TODO MR:- ApplicationSchoolLandAndBuildings - ApiReturns500___InternalServerError()

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

	    mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

	    return mockFactory;
    }
}
