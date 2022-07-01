using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class AcademisationCreationServiceTests
{
    [Test]
    public async Task AcademisationCreationService___CreateNewApplication___Success()
    {
        // arrange
        var expected = "Hello world"; // TODO MR:- will be json from Academies API
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

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var mockLogger = new Mock<ILogger<ConversionApplicationCreationService>>();

        var trustApplicationDto = ConversionApplicationTestDataFactory.BuildNewConversionApplication();

        // act
        var recordModelService = new ConversionApplicationCreationService(mockFactory.Object, mockLogger.Object);
        var trustApplicationModel = await recordModelService.CreateNewApplication(trustApplicationDto);

        // assert
        Assert.That(trustApplicationModel, Is.Not.Null);
        Assert.AreEqual(trustApplicationModel.ApplicationType, trustApplicationDto.ApplicationType);
        Assert.AreEqual(trustApplicationModel.UserEmail, trustApplicationDto.UserEmail);
        Assert.AreEqual(trustApplicationModel.Application, trustApplicationDto.Application);
        Assert.AreEqual(trustApplicationModel.TrustName, trustApplicationDto.TrustName);
        Assert.AreNotEqual(trustApplicationModel.Id, 0);
    }
}
