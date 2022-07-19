using System.Linq;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationRetrievalServiceTests
{
    [Test]
    public void ConversionApplicationRetrievalService___GetPendingApplications___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 3; // TODO: 
        string userEmail = string.Empty; // TODO: filter by useremail
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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var expectedExistingApplicationsTestData = applicationRetrievalService.GetPendingApplications(userEmail);

        // assert
        Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
        Assert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
    }

    [Test]
    public void ConversionApplicationRetrievalService___GetCompletedApplications___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 1; // TODO: 
        string userEmail = string.Empty; // TODO: filter by useremail
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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var expectedExistingApplicationsTestData = applicationRetrievalService.GetCompletedApplications(userEmail);

        // assert
        Assert.That(expectedExistingApplicationsTestData, Is.Not.Null);
        Assert.AreEqual(expectedCount, expectedExistingApplicationsTestData.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetConversionApplicationAuditEntries___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 3; // TODO: 
        int applicationId = 99; // TODO: 
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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var auditEntries = await applicationRetrievalService.GetConversionApplicationAuditEntries(applicationId);

        // assert
        Assert.That(auditEntries, Is.Not.Null);
        Assert.AreEqual(expectedCount, auditEntries.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetSchoolApplicationComponents___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 8; // TODO: 
        int applicationId = 99; // TODO: 
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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var applicationComponentStatuses = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId);

        // assert
        Assert.That(applicationComponentStatuses, Is.Not.Null);
        Assert.AreEqual(expectedCount, applicationComponentStatuses.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetConversionApplicationContributors___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int expectedCount = 2; // TODO: 
        int applicationId = 99; // TODO: 
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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var applicationContributors = await applicationRetrievalService.GetConversionApplicationContributors(applicationId);

        // assert
        Assert.That(applicationContributors, Is.Not.Null);
        Assert.AreEqual(expectedCount, applicationContributors.Count, "Count is not correct");
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetSchool___Success()
    {
	    // arrange
	    var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    int schoolId = 1; // TODO MR:- this value hard coded in dummy data at present !!!!!!
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

	    var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

	    // act
	    var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
	    var school = await applicationRetrievalService.GetSchool(schoolId);

	    // assert
	    Assert.That(school, Is.Not.Null);
	    Assert.That(school.SchoolId, Is.EqualTo(schoolId));
        Assert.That(school.SchoolName, Is.EqualTo("Chesterton primary school"));
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetApplication___FormNewMat___Success()
    {
	    // arrange
	    var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
	    int applicationId = 1; // TODO MR:- this value hard coded in dummy data at present !!!!!!
	    int expectedCount = 3; // TODO MR:- this value hard coded in dummy data at present !!!!!!
        var mockFactory = new Mock<IHttpClientFactory>();
        ApplicationTypes applicationType = ApplicationTypes.FormNewMat;

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

	    var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

	    // act
	    var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
	    var application = await applicationRetrievalService.GetApplication(applicationId, applicationType);

	    // assert
	    Assert.That(application, Is.Not.Null);
	    Assert.That(application.Id, Is.EqualTo(applicationId));
        Assert.That(application.Application, Is.EqualTo("Form a new multi-academy trust A2B_2549"));
        Assert.AreEqual(expectedCount, application.SchoolOrSchoolsApplyingToConvert.Count, "Count is not correct");
        Assert.That(application.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolName, Is.EqualTo("Chesterton primary school"));
        Assert.That(application.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolId, Is.EqualTo(96));
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetApplication___JoinAMat___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int applicationId = 1; // TODO MR:- this value hard coded in dummy data at present !!!!!!
        int expectedCount = 1; // TODO MR:- this value hard coded in dummy data at present !!!!!!
        var mockFactory = new Mock<IHttpClientFactory>();
        ApplicationTypes applicationType = ApplicationTypes.JoinMat;

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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var application = await applicationRetrievalService.GetApplication(applicationId, applicationType);

        // assert
        Assert.That(application, Is.Not.Null);
        Assert.That(application.Id, Is.EqualTo(applicationId));
        Assert.That(application.Application, Is.EqualTo("Join a multi-academy trust A2B_2549"));
        Assert.AreEqual(expectedCount, application.SchoolOrSchoolsApplyingToConvert.Count, "Count is not correct");
        Assert.That(application.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolName, Is.EqualTo("Chesterton primary school"));
        Assert.That(application.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolId, Is.EqualTo(96));
    }

    [Test]
    public async Task ConversionApplicationRetrievalService___GetApplication___FormNewSingleAcademyTrust___Success()
    {
        // arrange
        var expected = @"{ ""foo"": ""bar"" }"; // TODO MR:- will be json from Academies API
        int applicationId = 1; // TODO MR:- this value hard coded in dummy data at present !!!!!!
        int expectedCount = 1; // TODO MR:- this value hard coded in dummy data at present !!!!!!
        var mockFactory = new Mock<IHttpClientFactory>();
        ApplicationTypes applicationType = ApplicationTypes.FormNewSingleAcademyTrust;

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

        var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();

        // act
        var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);
        var application = await applicationRetrievalService.GetApplication(applicationId, applicationType);

        // assert
        Assert.That(application, Is.Not.Null);
        Assert.That(application.Id, Is.EqualTo(applicationId));
        Assert.That(application.Application, Is.EqualTo("Form a new single academy trust A2B_2549"));
        Assert.AreEqual(expectedCount, application.SchoolOrSchoolsApplyingToConvert.Count, "Count is not correct");
        Assert.That(application.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolName, Is.EqualTo("Chesterton primary school"));
        Assert.That(application.SchoolOrSchoolsApplyingToConvert.FirstOrDefault()?.SchoolId, Is.EqualTo(96));
    }
}