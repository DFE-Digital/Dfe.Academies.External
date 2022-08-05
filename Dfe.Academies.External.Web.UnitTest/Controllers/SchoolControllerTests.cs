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
internal sealed class SchoolControllerTests
{
    [Test]
    public async Task Seach___ResultsFound___ResultsReturned()
    {
        // arrange
        var mockLogger = new Mock<ILogger<SchoolController>>();
        var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object, mockConversionApplicationRetrievalService.Object);
        string schoolName = "wise";
        string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/schoolSearchResponse.json";
        string expectedJson = await File.ReadAllTextAsync(fullFilePath);
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

        // TODO MR:- need to throw the mockFactory into the DI pipeline so that  ResilientRequestProvider consumes it

        // act
        var result = await schoolController.Search(schoolName);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.Zero);
    }

    [Test]
    public async Task ReturnSchoolDetailsPartialViewPopulated___ValidSchool___ReturnsPartialView()
    {
	    // arrange
	    var mockLogger = new Mock<ILogger<SchoolController>>();
	    var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
	    var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object, mockConversionApplicationRetrievalService.Object);
	    string selectedSchool = "Wise owl primary school (587634)"; // selected value will be in the format 'Wise owl primary school (587634)'
	    string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getSchoolResponse.json";
	    string expectedJson = await File.ReadAllTextAsync(fullFilePath);
	    var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

	    // TODO MR:- need to throw the mockFactory into the DI pipeline so that  ResilientRequestProvider consumes it

        // act
        IActionResult result = await schoolController.ReturnSchoolDetailsPartialViewPopulated(selectedSchool);

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