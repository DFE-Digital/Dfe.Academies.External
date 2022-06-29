using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
public class ResilientRequestProviderTests
{
    [Test]
    public async Task ResilientRequestProvider_Delete_Success()
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
        var resilientRequestProvider = new ResilientRequestProvider(httpClient);
        var response = await resilientRequestProvider.DeleteAsync("https://www.example.com/ConversionApplication/1/");

        // assert
        Assert.AreEqual(response, true);
    }

    // TODO:- Test resilientRequestProvider.GetAsync<>()

    // TODO:- resilientRequestProvider.PostAsync<>()

    // TODO:- resilientRequestProvider.PutAsync()
}