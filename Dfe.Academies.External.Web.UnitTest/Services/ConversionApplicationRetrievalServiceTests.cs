using Dfe.Academies.External.Web.Services;
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
internal sealed class ConversionApplicationRetrievalServiceTests
{
    [Test]
    public void ConversionApplicationRetrievalService___GetPendingApplications___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 3; // TODO: 
        string userEmail = string.Empty; // TODO: filter by useremail
        var mockFactory = new Mock<IHttpClientFactory>();

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expected)
            });

        var httpClient = new HttpClient(mockMessageHandler.Object);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var expectedExistingApplicationsTestData = applicationRetrievalService.GetPendingApplications(userEmail);

        // assert
        Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
        Assert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
    }

    [Test]
    public void ConversionApplicationRetrievalService___GetCompletedApplications___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 1; // TODO: 
        string userEmail = string.Empty; // TODO: filter by useremail
        var mockFactory = new Mock<IHttpClientFactory>();

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expected)
            });

        var httpClient = new HttpClient(mockMessageHandler.Object);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var expectedExistingApplicationsTestData = applicationRetrievalService.GetCompletedApplications(userEmail);

        // assert
        Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
        Assert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetConversionApplicationAuditEntries___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 3; // TODO: 
        long applicationId = 99; // TODO: 
        var mockFactory = new Mock<IHttpClientFactory>();

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expected)
            });

        var httpClient = new HttpClient(mockMessageHandler.Object);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var auditEntries = await applicationRetrievalService.GetConversionApplicationAuditEntries(applicationId);

        // assert
        Assert.That(auditEntries, Is.Not.Null);
        Assert.AreEqual(expectedCount, auditEntries.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetConversionApplicationComponentStatuses___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 8; // TODO: 
        long applicationId = 99; // TODO: 
        var mockFactory = new Mock<IHttpClientFactory>();

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expected)
            });

        var httpClient = new HttpClient(mockMessageHandler.Object);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var applicationComponentStatuses = await applicationRetrievalService.GetConversionApplicationComponentStatuses(applicationId);

        // assert
        Assert.That(applicationComponentStatuses, Is.Not.Null);
        Assert.AreEqual(expectedCount, applicationComponentStatuses.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetConversionApplicationContributors___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 2; // TODO: 
        long applicationId = 99; // TODO: 
        var mockFactory = new Mock<IHttpClientFactory>();

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expected)
            });

        var httpClient = new HttpClient(mockMessageHandler.Object);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var applicationContributors = await applicationRetrievalService.GetConversionApplicationContributors(applicationId);

        // assert
        Assert.That(applicationContributors, Is.Not.Null);
        Assert.AreEqual(expectedCount, applicationContributors.Count, "Count is not correct");
    }
}