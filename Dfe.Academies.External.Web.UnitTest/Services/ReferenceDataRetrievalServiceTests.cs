using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ReferenceDataRetrievalServiceTests
{
    [Test]
    public async Task ReferenceDataRetrievalService___SearchSchools___Success()
    {
	    // arrange
	    var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    var mockFactory = new Mock<IHttpClientFactory>();
	    string schoolName = "wise"; // TODO MR:- this value hard coded in dummy data at present !!!!!!
		int urn = 587634; // TODO MR:- this value hard coded in dummy data at present !!!!!!

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

	    var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

	    SchoolSearch schoolSearch = new SchoolSearch(schoolName,urn.ToString());

	    // act
	    var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
	    var searchSchools = await referenceDataRetrievalService.SearchSchools(schoolSearch);

	    // assert
	    Assert.That(searchSchools, Is.Not.Null);
	    Assert.AreEqual(true, searchSchools.Any());
	    Assert.AreEqual(2, searchSchools.Count);
    }
}