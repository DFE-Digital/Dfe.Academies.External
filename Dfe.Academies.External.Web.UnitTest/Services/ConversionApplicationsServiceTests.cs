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
internal sealed class ConversionApplicationsServiceTests
{
    [Test]
    public void ConversionApplicationsService___GetPendingApplications___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
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
        var recordModelService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var expectedExistingApplicationsTestData = recordModelService.GetPendingApplications(userEmail);

        // assert
        Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
        Assert.AreEqual(expectedExistingApplicationsTestData.Count, 3, "Count is not correct");

    }

    [Test]
    public void ConversionApplicationsService___GetCompletedApplications___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
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
        var recordModelService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var expectedExistingApplicationsTestData = recordModelService.GetCompletedApplications(userEmail);

        // assert
        Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
        Assert.AreEqual(expectedExistingApplicationsTestData.Count, 1, "Count is not correct");

    }
}