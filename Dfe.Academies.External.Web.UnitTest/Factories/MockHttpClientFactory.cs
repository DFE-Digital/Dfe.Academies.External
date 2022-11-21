using Moq.Protected;
using Moq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal static class MockHttpClientFactory
{
	public static Mock<IHttpClientFactory> SetupMockHttpClientFactory(HttpStatusCode expectedStatusCode, string expectedJson)
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
		httpClient.BaseAddress = new Uri(APIConstants.AcademiesAPITestUrl);

		mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

		return mockFactory;
	}
}
