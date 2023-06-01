using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ResilientRequestProviderTests
{
	[Test]
	public async Task ResilientRequestProvider___Delete___Success()
	{
		// arrange
		var expected = @"{ ""foo"": ""bar"" }"; // expected JSON from API
		var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
		{
			var response = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(expected)
			};
			return Task.FromResult(response);
		});

		var factoryMock = new Mock<IHttpClientFactory>();
		factoryMock.Setup(m => m.CreateClient(It.IsAny<string>()))
			.Returns(() => new HttpClient(clientHandlerStub));

		var httpClient = new HttpClient(clientHandlerStub);

		// act
		var resilientRequestProvider = new ResilientRequestProvider(httpClient, new Mock<ILogger>().Object);
		var response = await resilientRequestProvider.DeleteAsync<string>("https://www.example.com/ConversionApplication/1/", expected);

		// assert
		Assert.AreEqual(response, true);
	}

	// TODO:- Test resilientRequestProvider.GetAsync<>()

	// TODO:- resilientRequestProvider.PostAsync<>()

	// TODO:- resilientRequestProvider.PutAsync()
}
