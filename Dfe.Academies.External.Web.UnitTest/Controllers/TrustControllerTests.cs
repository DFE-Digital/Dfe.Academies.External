using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bogus.Bson;
using Dfe.Academies.Contracts.V4.Trusts;
using Dfe.Academies.External.Web.Controllers;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academisation.CorrelationIdMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Controllers;

[Parallelizable(ParallelScope.All)]
internal sealed class TrustControllerTests
{
	private const string TestUrl = APIConstants.AcademiesAPITestUrl;

	[Test]
	public async Task Search___ResultsFound___ResultsReturned()
	{
		// arrange
		string trustName = "wise";
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustSearchResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 10;

		var mockLogger = new Mock<ILogger<TrustController>>();
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockReferenceDataRetrievalServiceLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockReferenceDataRetrievalServiceLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var trustController = new TrustController(mockLogger.Object, referenceDataRetrievalService);

		// act
		var result = await trustController.Search(trustName);

		// assert
		var searchResults = result.ToList();
		Assert.That(searchResults, Is.Not.Null);
		Assert.That(searchResults.Count, Is.EqualTo(expectedCount));
	}

	[Test]
	public async Task ReturnTrustDetailsPartialViewPopulated___ValidTrust___ReturnsPartialView()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getTrustResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var selectedTrust = JsonConvert.DeserializeObject<TrustDto>(expectedJson);

		var mockLogger = new Mock<ILogger<TrustController>>();
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockReferenceDataRetrievalServiceLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockReferenceDataRetrievalServiceLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));
		var trustController = new TrustController(mockLogger.Object, referenceDataRetrievalService);

		// act
		PartialViewResult result = (PartialViewResult)await trustController.ReturnTrustDetailsPartialViewPopulated(selectedTrust);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.ViewName, Is.EqualTo("_TrustDetails"));

		Assert.That(result.Model, Is.Not.Null);
		TrustDetailsViewModel vm = (TrustDetailsViewModel)result.Model!;
		Assert.That(vm.Ukprn, Is.EqualTo(selectedTrust.Ukprn));
		Assert.That(vm.TrustName, Is.EqualTo("ALCESTER GRAMMAR SCHOOL"));
		Assert.That(vm.TrustReference, Is.EqualTo("TR02511"));
		Assert.That(vm.Street, Is.EqualTo("Birmingham Road"));
		Assert.That(vm.Locality, Is.EqualTo(null));
		Assert.That(vm.Address3, Is.EqualTo(null));
		Assert.That(vm.Town, Is.EqualTo("Alcester"));
		Assert.That(vm.CountyDescription, Is.EqualTo(null));
		Assert.That(vm.FullUkPostcode, Is.EqualTo("B49 5ED"));

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
