using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academisation.CorrelationIdMiddleware;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

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
		string expectedJSON = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 12;
		string schoolName = "wise";
		int urn = 101934;

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJSON);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		SchoolSearch schoolSearch = new(schoolName, urn.ToString(), string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var searchSchools = await referenceDataRetrievalService.SearchSchools(schoolSearch);

		// assert
		Assert.That(searchSchools, Is.Not.Null);
		Assert.AreEqual(true, searchSchools.Any());
		Assert.AreEqual(expectedCount, searchSchools.Count);
		Assert.That(searchSchools?.FirstOrDefault()?.SchoolName, Is.EqualTo("The Cardinal Wiseman Catholic School"));
		Assert.That(searchSchools?.FirstOrDefault()?.URN, Is.EqualTo(101934));
		Assert.That(searchSchools?.FirstOrDefault()?.UKPRN, Is.EqualTo("10006563"));
	}

	[Test]
	public async Task SearchSchools___ApiReturns500___Failure()
	{
		// arrange
		string schoolName = "wise";
		int urn = 587634;
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		SchoolSearch schoolSearch = new(schoolName, urn.ToString(), string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.SearchSchools(schoolSearch));
	}

	[Test]
	public async Task GetSchool___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getSchoolResponse.json";
		string expectedJSON = await File.ReadAllTextAsync(fullFilePath);
		int urn = 101934;

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJSON);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var school = await referenceDataRetrievalService.GetSchool(urn);

		// assert
		Assert.That(school, Is.Not.Null);
		Assert.That(school.Urn, Is.EqualTo(urn.ToString()));
		Assert.That(school.EstablishmentNumber, Is.EqualTo("4603"));
		Assert.That(school.EstablishmentName, Is.EqualTo("The Cardinal Wiseman Catholic School"));
		Assert.That(school.Address.Street, Is.EqualTo("Greenford Road"));
		Assert.That(school.Address.Locality, Is.EqualTo(null));
		Assert.That(school.Address.AdditionalLine, Is.EqualTo(null));
		Assert.That(school.Address.Town, Is.EqualTo("Greenford"));
		Assert.That(school.Address.County, Is.EqualTo("Middlesex"));
		Assert.That(school.Address.Postcode, Is.EqualTo("UB6 9AW"));
		Assert.That(school.Ukprn, Is.EqualTo("10006563"));
		Assert.That(school.UPRN, Is.EqualTo("12141188"));
	}

	[Test]
	public async Task GetSchool___ApiReturns500___Failure()
	{
		// arrange
		int urn = 101003;
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.GetSchool(urn));
	}

	[Test]
	public void BuildSchoolSearchRequestUri___NameOnly___Success()
	{
		// arrange
		string name = "wise";
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		SchoolSearch schoolSearch = new(name, string.Empty, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var builtUri = referenceDataRetrievalService.BuildSchoolSearchRequestUri(schoolSearch);

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
		string urn = "101934"; // The Cardinal Wiseman Catholic School
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		SchoolSearch schoolSearch = new(string.Empty, urn, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var builtUri = referenceDataRetrievalService.BuildSchoolSearchRequestUri(schoolSearch);

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
		string ukprn = "10015453"; // The Cardinal Wiseman Catholic School
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		SchoolSearch schoolSearch = new(string.Empty, string.Empty, ukprn);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var builtUri = referenceDataRetrievalService.BuildSchoolSearchRequestUri(schoolSearch);

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("ukprn%3d10015453%26api-version%3dV1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("ukprn=10015453&api-version=V1", decodedUri);
	}

	[Test]
	public async Task GetTrusts___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustSearchResponse.json";
		string expectedJSON = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 10;
		string name = "grammar";
		string ukprn = "10058464";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJSON);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		TrustSearch trustSearch = new(name, ukprn, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var trusts = await referenceDataRetrievalService.GetTrusts(trustSearch);

		// assert
		Assert.That(trusts, Is.Not.Null);
		Assert.AreEqual(true, trusts.Data.Any());
		Assert.AreEqual(expectedCount, trusts.Data.Count());
		Assert.AreEqual(ukprn, trusts?.Data.FirstOrDefault()?.Ukprn);
		Assert.AreEqual("ALCESTER GRAMMAR SCHOOL", trusts?.Data.FirstOrDefault()?.Name);
		Assert.AreEqual("07485466", trusts?.Data.FirstOrDefault()?.CompaniesHouseNumber);
		Assert.AreEqual(null, trusts?.Data.FirstOrDefault()?.Address.Street);
		Assert.AreEqual(null, trusts?.Data.FirstOrDefault()?.Address.Locality);
		Assert.AreEqual("Birmingham Road", trusts?.Data.FirstOrDefault()?.Address.Additional);
		Assert.AreEqual("Alcester", trusts?.Data.FirstOrDefault()?.Address.Town);
		Assert.AreEqual(null, trusts?.Data.FirstOrDefault()?.Address.County);
		Assert.AreEqual("B49 5ED", trusts?.Data.FirstOrDefault()?.Address.Postcode);
	}

	[Test]
	public async Task GetTrusts___ApiReturns500___Failure()
	{
		// arrange
		string name = "grammar";
		string ukprn = "10058464";
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		TrustSearch trustSearch = new(name, ukprn, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.GetTrusts(trustSearch));
	}

	[Test]
	public async Task GetTrustByUkPrn___ApiReturns200___Success()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustResponse.json";
		string expectedJSON = await File.ReadAllTextAsync(fullFilePath);
		string ukprn = "10058464";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJSON);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var trusts = await referenceDataRetrievalService.GetTrustByUkPrn(ukprn);

		// assert
		Assert.That(trusts, Is.Not.Null);
		Assert.AreEqual(ukprn, trusts?.Ukprn);
		Assert.AreEqual("ALCESTER GRAMMAR SCHOOL", trusts?.Name);
		Assert.AreEqual("07485466", trusts?.CompaniesHouseNumber);
		Assert.AreEqual(null, trusts?.Address.Street);
		Assert.AreEqual(null, trusts?.Address.Locality);
		Assert.AreEqual("Birmingham Road", trusts?.Address.Additional);
		Assert.AreEqual("Alcester", trusts?.Address.Town);
		Assert.AreEqual(null, trusts?.Address.County);
		Assert.AreEqual("B49 5ED", trusts?.Address.Postcode);
	}

	[Test]
	public async Task GetTrustByUkPrn___ApiReturns500___Failure()
	{
		// arrange
		string ukprn = "10058464";
		string expectedJson = @"{ ""foo"": ""bar"" }";

		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.InternalServerError, expectedJson);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		// assert
		var ex = Assert.ThrowsAsync<HttpRequestException>(() => referenceDataRetrievalService.GetTrustByUkPrn(ukprn));
	}

	[Test]
	public void BuildTrustSearchRequestUri___NameOnly___Success()
	{
		// arrange
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		string name = "wise";
		TrustSearch trustSearch = new(name, string.Empty, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
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
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		string ukPrn = "10058464"; // ALCESTER GRAMMAR SCHOOL
		TrustSearch trustSearch = new(string.Empty, ukPrn, string.Empty);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
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
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, string.Empty);
		var mockLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();

		string companiesHouseNumber = "07485466"; // ALCESTER GRAMMAR SCHOOL
		TrustSearch trustSearch = new(string.Empty, string.Empty, companiesHouseNumber);

		// act
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var builtUri = referenceDataRetrievalService.BuildTrustSearchRequestUri(trustSearch);

		// assert
		Assert.That(builtUri, Is.Not.Null);
		Assert.AreEqual("companiesHouseNumber%3d07485466%26page%3d1", builtUri);
		var decodedUri = HttpUtility.UrlDecode(builtUri);
		Assert.AreEqual("companiesHouseNumber=07485466&page=1", decodedUri);
	}

	private static Mock<IHttpClientFactory> SetupMockHttpClientFactory(HttpStatusCode expectedStatusCode, string expectedJson)
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
