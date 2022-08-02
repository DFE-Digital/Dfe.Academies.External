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
	public async Task GetTrusts___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustSearchResponse.json";
		string expected = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 10;
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

		TrustSearch trustSearch = new (name, ukprn.ToString(), string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var trusts = await referenceDataRetrievalService.GetTrusts(trustSearch);

		// assert
		Assert.That(trusts, Is.Not.Null);
		Assert.AreEqual(true, trusts.Any());
		Assert.AreEqual(expectedCount, trusts.Count);
	}
	
	[Test]
	public async Task GetTrusts___ApiReturns500___Failure()
	{
		// arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		string name = "wise";
		int ukprn = 587634;
		
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
		TrustSearch trustSearch = new (name, ukprn.ToString(), string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.GetTrusts(trustSearch));
	}

	[Test]
	public async Task GetTrustByUkPrn___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustResponse.json";
		string expected = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = new Mock<IHttpClientFactory>();
		string ukprn = "10058464";

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
		var trusts = await referenceDataRetrievalService.GetTrustByUkPrn(ukprn);

		// assert
		Assert.That(trusts, Is.Not.Null);
		Assert.AreEqual(ukprn, trusts?.FirstOrDefault()?.UkPrn);
		Assert.AreEqual(null, trusts?.FirstOrDefault()?.Urn);
		Assert.AreEqual("ALCESTER GRAMMAR SCHOOL", trusts?.FirstOrDefault()?.Name);
		Assert.AreEqual("07485466", trusts?.FirstOrDefault()?.CompaniesHouseNumber);
		Assert.AreEqual("Standalone", trusts?.FirstOrDefault()?.TypeDescription);
	}

	[Test]
	public async Task GetTrustByUkPrn___ApiReturns500___Failure()
	{
		// arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		string ukprn = "10058464";

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
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.GetTrustByUkPrn(ukprn));
	}

	[Test]
	public void BuildTrustSearchRequestUri___NameOnly___Success()
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
		TrustSearch trustSearch = new (name, string.Empty, string.Empty);
		
		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var builtUri = referenceDataRetrievalService.BuildTrustSearchRequestUri(trustSearch);

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("groupName%3dwise%26page%3d1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("groupName=wise&page=1", decodedUri);
	}

	[Test]
	public void BuildTrustSearchRequestUri___UkPrnOnly___Success()
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

		string ukPrn = "10058464"; // ALCESTER GRAMMAR SCHOOL
		TrustSearch trustSearch = new (string.Empty, ukPrn, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var builtUri = referenceDataRetrievalService.BuildTrustSearchRequestUri(trustSearch);

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("ukprn%3d10058464%26page%3d1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("ukprn=10058464&page=1", decodedUri);
	}

	[Test]
	public void BuildTrustSearchRequestUri___CompaniesHouseNumberOnly___Success()
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

		string companiesHouseNumber = "07485466"; // ALCESTER GRAMMAR SCHOOL
		TrustSearch trustSearch = new (string.Empty, string.Empty, companiesHouseNumber);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object);
		var builtUri = referenceDataRetrievalService.BuildTrustSearchRequestUri(trustSearch);

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("companiesHouseNumber%3d07485466%26page%3d1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("companiesHouseNumber=07485466&page=1", decodedUri);
	}
}