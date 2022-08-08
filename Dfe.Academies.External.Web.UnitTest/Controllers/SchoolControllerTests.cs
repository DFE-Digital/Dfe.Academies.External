using Dfe.Academies.External.Web.Controllers;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
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
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.UnitTest.Controllers;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolControllerTests
{
	private const string TestUrl = APIConstants.AcademiesAPITestUrl;

    [Test]
    public async Task Search___ResultsFound___ResultsReturned()
    {
        // arrange
        string schoolName = "wise";
        //int urn = 101934;
        string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/schoolSearchResponse.json";
        string expectedJson = await File.ReadAllTextAsync(fullFilePath);
        int expectedCount = 12;

        var mockSchoolControllerLogger = new Mock<ILogger<SchoolController>>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
        var mockReferenceDataRetrievalServiceLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
        var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockReferenceDataRetrievalServiceLogger.Object);
        
        var schoolController = new SchoolController(mockSchoolControllerLogger.Object, referenceDataRetrievalService, mockConversionApplicationRetrievalService.Object);

        // act
        var result = await schoolController.Search(schoolName);

        // assert
        var searchResults = result.ToList();
        Assert.That(searchResults, Is.Not.Null);
        Assert.That(searchResults.Count, Is.EqualTo(expectedCount));
    }

    [Test]
    public async Task ReturnSchoolDetailsPartialViewPopulated___ValidSchool___ReturnsPartialView()
    {
	    // arrange
	    string selectedSchool = "Wise owl primary school (587634)"; // selected value will be in the format 'Wise owl primary school (587634)'
	    string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getSchoolResponse.json";
	    string expectedJson = await File.ReadAllTextAsync(fullFilePath);
	    int urn = 101934;
        var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
	    var mockLogger = new Mock<ILogger<SchoolController>>();
	    var mockReferenceDataRetrievalServiceLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockReferenceDataRetrievalServiceLogger.Object);

        var schoolController = new SchoolController(mockLogger.Object, referenceDataRetrievalService, mockConversionApplicationRetrievalService.Object);

        // act
        PartialViewResult result = (PartialViewResult)await schoolController.ReturnSchoolDetailsPartialViewPopulated(selectedSchool);

	    // assert
	    Assert.That(result, Is.Not.Null);
        Assert.That(result.ViewName, Is.EqualTo("_SchoolDetails"));

        Assert.That(result.Model, Is.Not.Null);
        SchoolDetailsViewModel vm = (SchoolDetailsViewModel)result.Model!;
        Assert.That(vm.URN, Is.EqualTo(urn));
        Assert.That(vm.EstablishmentNumber, Is.EqualTo(null));
        Assert.That(vm.SchoolName, Is.EqualTo("The Cardinal Wiseman Catholic School"));
        Assert.That(vm.Street, Is.EqualTo("Greenford Road"));
        Assert.That(vm.Locality, Is.EqualTo(null));
        Assert.That(vm.Address3, Is.EqualTo(null));
        Assert.That(vm.Town, Is.EqualTo("Greenford"));
        Assert.That(vm.CountyDescription, Is.EqualTo(null));
        Assert.That(vm.FullUkPostcode, Is.EqualTo("UB6 9AW"));
        Assert.That(vm.UKPRN, Is.EqualTo(null));
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
	    httpClient.BaseAddress = new Uri(TestUrl);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

	    return mockFactory;
    }
}