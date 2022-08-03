using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
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
using System.Web;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ReferenceDataRetrievalServiceTests
{
	private const string TestUrl = APIConstants.AcademiesAPITestUrl;

	[Test]
    public async Task SearchSchools___ApiReturns200___Success()
    {
	    // arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/schoolSearchResponse.json";
		string expected = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 12;
		var mockFactory = new Mock<IHttpClientFactory>();
	    string schoolName = "wise";
		int urn = 587634;
		
		var mockMessageHandler = new Mock<HttpMessageHandler>();
	    mockMessageHandler.Protected()
		    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
		    .ReturnsAsync(new HttpResponseMessage
		    {
			    StatusCode = HttpStatusCode.OK,
			    Content = new StringContent(expected)
		    });

	    var httpClient = new HttpClient(mockMessageHandler.Object);
	    httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

	    var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

	    SchoolSearch schoolSearch = new (schoolName,urn.ToString(), string.Empty);

	    // act
	    var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
	    var searchSchools = await referenceDataRetrievalService.SearchSchools(schoolSearch);

	    // assert
	    Assert.That(searchSchools, Is.Not.Null);
	    Assert.AreEqual(true, searchSchools.Any());
	    Assert.AreEqual(expectedCount, searchSchools.Count);
    }

	[Test]
	public async Task SearchSchools___ApiReturns500___Failure()
	{
		// arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		string schoolName = "wise";
		int urn = 587634;

		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.InternalServerError
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
		SchoolSearch schoolSearch = new SchoolSearch(schoolName, urn.ToString(), string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.SearchSchools(schoolSearch));
	}

	[Test]
    public async Task GetSchool___ApiReturns200___Success()
    {
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getSchoolResponse.json";
		string expected = await File.ReadAllTextAsync(fullFilePath);
		int urn = 101934;
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
	    httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

	    var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

	    // act
	    var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
	    var school = await referenceDataRetrievalService.GetSchool(urn);

	    // assert
	    Assert.That(school, Is.Not.Null);
	    Assert.That(school.Urn, Is.EqualTo(urn.ToString()));
	    Assert.That(school.Name, Is.EqualTo("The Cardinal Wiseman Catholic School"));
	    Assert.That(school.Address.Street, Is.EqualTo("Greenford Road"));
	    Assert.That(school.Address.Locality, Is.EqualTo(null));
	    Assert.That(school.Address.AdditionalLine, Is.EqualTo(null));
	    Assert.That(school.Address.Town, Is.EqualTo("Greenford"));
	    Assert.That(school.Address.County, Is.EqualTo("Middlesex"));
	    Assert.That(school.Address.Postcode, Is.EqualTo("UB6 9AW"));
	}

	[Test]
	public async Task GetSchool___ApiReturns500___Failure()
	{
		// arrange
		int urn = 101003; 
		var mockFactory = new Mock<IHttpClientFactory>();

		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.InternalServerError
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.GetSchool(urn));
	}
	
	[Test]
	public void BuildSchoolSearchRequestUri___NameOnly___Success()
	{
		// arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		string name = "wise";
		SchoolSearch schoolSearch = new (name, string.Empty, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var builtUri = referenceDataRetrievalService.BuildSchoolSearchRequestUri(schoolSearch, "V1");

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("name%3dwise%26api-version%3dV1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("name=wise&api-version=V1", decodedUri);
	}

	[Test]
	public void BuildSchoolSearchRequestUri___UrnOnly___Success()
	{
		// arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		string urn = "101934"; // The Cardinal Wiseman Catholic School
		SchoolSearch schoolSearch = new (string.Empty, urn , string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var builtUri = referenceDataRetrievalService.BuildSchoolSearchRequestUri(schoolSearch, "V1");

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("Urn%3d101934%26api-version%3dV1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("Urn=101934&api-version=V1", decodedUri);
	}

	[Test]
	public void BuildSchoolSearchRequestUri___UKPRNOnly___Success()
	{
		// arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		string ukprn = "10015453"; // The Cardinal Wiseman Catholic School
		SchoolSearch schoolSearch = new (string.Empty, string.Empty, ukprn);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var builtUri = referenceDataRetrievalService.BuildSchoolSearchRequestUri(schoolSearch, "V1");

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("ukprn%3d10015453%26api-version%3dV1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("ukprn=10015453&api-version=V1", decodedUri);
	}
}