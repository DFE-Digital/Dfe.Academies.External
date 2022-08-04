using Dfe.Academies.External.Web.Controllers;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Controllers;

[Parallelizable(ParallelScope.All)]
internal sealed class TrustControllerTests
{
    [Test]
    public async Task TrustController___Search___ReturnsResult()
    {
        // arrange
        var mockLogger = new Mock<ILogger<TrustController>>();
        var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
        var trustController = new TrustController(mockLogger.Object, mockReferenceDataRetrievalService.Object);
        string trustName = "wise";

        string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustSearchResponse.json";
        string expectedJson = await File.ReadAllTextAsync(fullFilePath);
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

        // act
        var result = await trustController.Search(trustName);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.Zero);
    }

    [Test]
    public async Task TrustController___ReturnSchoolDetailsPartialViewPopulated___ReturnsPartialView()
    {
	    // arrange
	    var mockLogger = new Mock<ILogger<TrustController>>();
	    var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
        var trustController = new TrustController(mockLogger.Object, mockReferenceDataRetrievalService.Object);
	    string selectedTrust = "WISE OWL TRUST (10059766)"; // selected value will be in the format 'WISE OWL TRUST (10059766)'
	    string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustResponse.json";
	    string expectedJson = await File.ReadAllTextAsync(fullFilePath);
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

        // act
        IActionResult result = await trustController.ReturnTrustDetailsPartialViewPopulated(selectedTrust);

	    // assert
	    Assert.That(result, Is.Not.Null);
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