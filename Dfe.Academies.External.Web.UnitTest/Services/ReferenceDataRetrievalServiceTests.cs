using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ReferenceDataRetrievalServiceTests
{
	private const string TestUrl = APIConstants.AcademiesAPITestUrl;

	[Test]
    public async Task SearchSchools___ApiReturns200___Success()
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
	    httpClient.BaseAddress = new Uri(TestUrl);
		// default headers would be:-
		// httpClient.DefaultRequestHeaders.Add("ApiKey", tramsApiKey);
		// httpClient.DefaultRequestHeaders.Add("ContentType", MediaTypeNames.Application.Json);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

	    var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

	    SchoolSearch schoolSearch = new SchoolSearch(schoolName,urn.ToString(), "");

	    // act
	    var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
	    var searchSchools = await referenceDataRetrievalService.SearchSchools(schoolSearch);

	    // assert
	    Assert.That(searchSchools, Is.Not.Null);
	    Assert.AreEqual(true, searchSchools.Any());
	    Assert.AreEqual(2, searchSchools.Count);
    }

	//[Test]
	public async Task SearchSchools___ApiReturns500___Failure()
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
				StatusCode = HttpStatusCode.InternalServerError,
				Content = new StringContent(expected)
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
		SchoolSearch schoolSearch = new SchoolSearch(schoolName, urn.ToString(), "");

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.SearchSchools(schoolSearch));
	}

	[Test]
    public async Task GetSchool___ApiReturns200___Success()
    {
	    // arrange
	    var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    int urn = 101003; // TODO MR:- this value hard coded in dummy data at present !!!!!!
	    int schoolId = int.MaxValue;
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
	    Assert.That(school.Urn, Is.EqualTo(urn));
	    Assert.That(school.Name, Is.EqualTo("Chesterton primary school"));
    }

	//[Test]
	public async Task GetSchool___ApiReturns500___Failure()
	{
		// arrange
		var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
		int urn = 101003; // TODO MR:- this value hard coded in dummy data at present !!!!!!
		int schoolId = int.MaxValue; 
		var mockFactory = new Mock<IHttpClientFactory>();

		var mockMessageHandler = new Mock<HttpMessageHandler>();
		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.InternalServerError,
				Content = new StringContent(expected)
			});

		var httpClient = new HttpClient(mockMessageHandler.Object);
		httpClient.BaseAddress = new Uri(TestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		// act
		var applicationRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => applicationRetrievalService.GetSchool(urn));
	}

	//[Test]
	public async Task GetTrustsByPagination___ApiReturns200___Success()
	{
		// arrange
		var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
		var mockFactory = new Mock<IHttpClientFactory>();
		string name = "wise";
		int ukprn = 587634;

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

		TrustSearch schoolSearch = new TrustSearch(name, ukprn.ToString(), "");

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var trustsByPagination = await referenceDataRetrievalService.GetTrustsByPagination(schoolSearch);

		// assert
		Assert.That(trustsByPagination, Is.Not.Null);
		Assert.AreEqual(true, trustsByPagination.Data.Any());
		Assert.AreEqual(2, trustsByPagination.Data.Count);
	}

	// TODO MR:- GetTrustsByPagination() - failure

	//[Test]
	public async Task GetTrustByUkPrn___ApiReturns200___Success()
	{
		// arrange
		var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
		var mockFactory = new Mock<IHttpClientFactory>();
		int ukprn = 587634;

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
		var trustDetailsDto = await referenceDataRetrievalService.GetTrustByUkPrn(ukprn.ToString());

		// assert
		Assert.That(trustDetailsDto, Is.Not.Null);
		//Assert.AreEqual(ukprn, trustDetailsDto.Establishments.FirstOrDefault().ukprn); // TODO MR:- property is missing from object but definitely within JSON
	}

	// TODO MR:- GetTrustByUkPrn() - failure
}