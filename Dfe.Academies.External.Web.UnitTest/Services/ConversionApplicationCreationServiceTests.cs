using AutoFixture;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationCreationServiceTests
{
	private static readonly Fixture Fixture = new();

    //[Test]
    public async Task CreateNewApplication___Success()
    {
        // arrange
        var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationNoRoles();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);
        var trustApplicationModel = await recordModelService.CreateNewApplication(trustApplicationDto);

        // assert
        Assert.That(trustApplicationModel, Is.Not.Null);
        Assert.AreEqual(trustApplicationModel.ApplicationType, trustApplicationDto.ApplicationType);
        Assert.AreEqual(trustApplicationModel.UserEmail, trustApplicationDto.UserEmail);
        Assert.AreEqual(trustApplicationModel.Application, trustApplicationDto.Application);
        Assert.AreNotEqual(trustApplicationModel.Id, 0);
    }

    //[Test]
    public async Task UpdateDraftApplication___OtherRole___Success()
    {
        // arrange
        var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithOtherRole();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        Assert.DoesNotThrowAsync(() => recordModelService.UpdateDraftApplication(trustApplicationDto));
    }

    //[Test]
    public async Task UpdateDraftApplication___ChairRole___Success()
    {
        // arrange
        var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        Assert.DoesNotThrowAsync(() => recordModelService.UpdateDraftApplication(trustApplicationDto));
    }

    /// <summary>
    /// call add school endpoint and mock HttpStatusCode.Created
    /// </summary>
    //[Test]
    public async Task AddSchoolToApplication___ApiReturns201___Created()
    {
	    // arrange
	    var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
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
    public async Task ApplicationAddJoinTrustReasons___ApiReturns201___Created()
    {
        // arrange
        var expectedJson = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.Created, expectedJson);
        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();
        string ApplicationAddJoinTrustReason = Fixture.Create<string>();
        var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationNoRoles();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        Assert.DoesNotThrowAsync(() => recordModelService.ApplicationAddJoinTrustReasons(trustApplicationDto, ApplicationAddJoinTrustReason));
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
        string ApplicationAddJoinTrustReason = Fixture.Create<string>();
        var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplicationNoRoles();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);

        // assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(() => recordModelService.ApplicationAddJoinTrustReasons(trustApplicationDto, ApplicationAddJoinTrustReason));

        // now we could test the exception itself
        //Assert.That(ex.Message == "Blah");
    }

    private Mock<IHttpClientFactory> SetupMockHttpClientFactory(HttpStatusCode expectedStatusCode, string expectedJson)
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
