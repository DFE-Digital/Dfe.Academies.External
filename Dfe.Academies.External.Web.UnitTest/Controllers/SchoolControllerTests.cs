using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dfe.Academies.Contracts.V4.Establishments;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Controllers;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academisation.CorrelationIdMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Controllers;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolControllerTests
{
	private const string TestUrl = APIConstants.AcademiesAPITestUrl;

	[Test]
	public async Task Search___ResultsFound___ResultsReturned()
	{
		// arrange
		string schoolName = "wise";
		//int urn = 101934;
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/schoolSearchResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int expectedCount = 12;

		var mockSchoolControllerLogger = new Mock<ILogger<SchoolController>>();
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockReferenceDataRetrievalServiceLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockReferenceDataRetrievalServiceLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		var schoolController = new SchoolController(mockSchoolControllerLogger.Object, referenceDataRetrievalService);

		// act
		var result = await schoolController.Search(schoolName);

		// assert
		var searchResults = result.ToList();
		Assert.That(searchResults, Is.Not.Null);
		Assert.That(searchResults.Count, Is.EqualTo(expectedCount));
	}

	[Test]
	public async Task Search___NullSearchQuery___ReturnsEmpty()
	{
		// arrange
		string? searchQuery = null;
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		// act
		var result = await schoolController.Search(searchQuery!);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
		mockReferenceDataRetrievalService.Verify(x => x.SearchSchools(It.IsAny<SchoolSearch>()), Times.Never);
	}

	[Test]
	public async Task Search___EmptySearchQuery___ReturnsEmpty()
	{
		// arrange
		string searchQuery = string.Empty;
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
		mockReferenceDataRetrievalService.Verify(x => x.SearchSchools(It.IsAny<SchoolSearch>()), Times.Never);
	}

	[Test]
	public async Task Search___SearchQueryTooShort___ReturnsEmpty()
	{
		// arrange
		string searchQuery = "ab"; // Less than SearchQueryMinLength (3)
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
		mockReferenceDataRetrievalService.Verify(x => x.SearchSchools(It.IsAny<SchoolSearch>()), Times.Never);
	}

	[Test]
	public async Task Search___SixDigitNumericQuery___SearchesByUrn()
	{
		// arrange
		string searchQuery = "123456";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		var mockResults = new List<EstablishmentDto>
		{
			new EstablishmentDto { Urn = "123456", Name = "Test School" }
		};
		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.Is<SchoolSearch>(s => s.Urn == searchQuery && s.Name == string.Empty)))
			.ReturnsAsync(mockResults);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Count(), Is.EqualTo(1));
		Assert.That(result.First(), Is.EqualTo("Test School (123456)"));
		mockReferenceDataRetrievalService.Verify(x => x.SearchSchools(It.Is<SchoolSearch>(s => s.Urn == searchQuery && s.Name == string.Empty)), Times.Once);
	}

	[Test]
	public async Task Search___TextQuery___SearchesByName()
	{
		// arrange
		string searchQuery = "Test School";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		var mockResults = new List<EstablishmentDto>
		{
			new EstablishmentDto { Urn = "123456", Name = "Test School" }
		};
		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.Is<SchoolSearch>(s => s.Name == searchQuery && s.Urn == string.Empty)))
			.ReturnsAsync(mockResults);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Count(), Is.EqualTo(1));
		Assert.That(result.First(), Is.EqualTo("Test School (123456)"));
		mockReferenceDataRetrievalService.Verify(x => x.SearchSchools(It.Is<SchoolSearch>(s => s.Name == searchQuery && s.Urn == string.Empty)), Times.Once);
	}

	[Test]
	public async Task Search___NoResultsFound___ReturnsEmpty()
	{
		// arrange
		string searchQuery = "Nonexistent School";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.IsAny<SchoolSearch>()))
			.ReturnsAsync(new List<EstablishmentDto>());

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	[Test]
	public async Task Search___ResultsOrderedByRelevance___ExactMatchFirst()
	{
		// arrange
		string searchQuery = "Test School";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		var mockResults = new List<EstablishmentDto>
		{
			new EstablishmentDto { Urn = "111111", Name = "Another Test School" }, // Contains query
			new EstablishmentDto { Urn = "222222", Name = "Test School" }, // Exact match - should be first
			new EstablishmentDto { Urn = "333333", Name = "Test School Name" } // Starts with query
		};
		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.IsAny<SchoolSearch>()))
			.ReturnsAsync(mockResults);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		var resultsList = result.ToList();
		Assert.That(resultsList, Is.Not.Null);
		Assert.That(resultsList.Count, Is.EqualTo(3));
		// Exact match should be first
		Assert.That(resultsList[0], Is.EqualTo("Test School (222222)"));
		// Starts with should be second
		Assert.That(resultsList[1], Is.EqualTo("Test School Name (333333)"));
		// Contains should be third
		Assert.That(resultsList[2], Is.EqualTo("Another Test School (111111)"));
	}

	[Test]
	public async Task Search___ResultsOrderedByRelevance___UrnExactMatchSecond()
	{
		// arrange
		string searchQuery = "123456";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		var mockResults = new List<EstablishmentDto>
		{
			new EstablishmentDto { Urn = "123456", Name = "Different Name" }, // URN exact match - should be first
			new EstablishmentDto { Urn = "123457", Name = "School 123456" }, // Name contains URN
			new EstablishmentDto { Urn = "123458", Name = "Another School" } // No match
		};
		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.IsAny<SchoolSearch>()))
			.ReturnsAsync(mockResults);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		var resultsList = result.ToList();
		Assert.That(resultsList, Is.Not.Null);
		Assert.That(resultsList.Count, Is.EqualTo(3));
		// URN exact match should be first
		Assert.That(resultsList[0], Is.EqualTo("Different Name (123456)"));
	}

	[Test]
	public async Task Search___ExceptionThrown___ReturnsEmpty()
	{
		// arrange
		string searchQuery = "Test";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.IsAny<SchoolSearch>()))
			.ThrowsAsync(new Exception("Test exception"));

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	[Test]
	public async Task Search___ResultsWithNullNameOrUrn___HandlesGracefully()
	{
		// arrange
		string searchQuery = "Test";
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		var mockResults = new List<EstablishmentDto>
		{
			new EstablishmentDto { Urn = "123456", Name = null! },
			new EstablishmentDto { Urn = null!, Name = "Test School" },
			new EstablishmentDto { Urn = "789012", Name = "Another School" }
		};
		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.IsAny<SchoolSearch>()))
			.ReturnsAsync(mockResults);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		var resultsList = result.ToList();
		Assert.That(resultsList, Is.Not.Null);
		Assert.That(resultsList.Count, Is.EqualTo(3));
		// Should handle null values gracefully
		Assert.That(resultsList.Any(r => r.Contains("123456")), Is.True);
		Assert.That(resultsList.Any(r => r.Contains("Test School")), Is.True);
		Assert.That(resultsList.Any(r => r.Contains("Another School")), Is.True);
	}

	[Test]
	public async Task Search___NumericQueryNotSixDigits___SearchesByName()
	{
		// arrange
		string searchQuery = "12345"; // 5 digits, not 6
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object);

		var mockResults = new List<EstablishmentDto>
		{
			new EstablishmentDto { Urn = "123456", Name = "School 12345" }
		};
		mockReferenceDataRetrievalService
			.Setup(x => x.SearchSchools(It.Is<SchoolSearch>(s => s.Name == searchQuery && s.Urn == string.Empty)))
			.ReturnsAsync(mockResults);

		// act
		var result = await schoolController.Search(searchQuery);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Count(), Is.EqualTo(1));
		mockReferenceDataRetrievalService.Verify(x => x.SearchSchools(It.Is<SchoolSearch>(s => s.Name == searchQuery && s.Urn == string.Empty)), Times.Once);
	}

	[Test]
	public async Task ReturnSchoolDetailsPartialViewPopulated___ValidSchool___ReturnsPartialView()
	{
		// arrange
		string selectedSchool = "Wise owl primary school (587634)"; // selected value will be in the format 'Wise owl primary school (587634)'
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getSchoolResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		int urn = 101934;
		var mockFactory = SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
		var mockLogger = new Mock<ILogger<SchoolController>>();
		var mockReferenceDataRetrievalServiceLogger = new Mock<ILogger<ReferenceDataRetrievalService>>();
		var referenceDataRetrievalService = new ReferenceDataRetrievalService(mockFactory.Object, mockReferenceDataRetrievalServiceLogger.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()));

		var schoolController = new SchoolController(mockLogger.Object, referenceDataRetrievalService);

		// act
		PartialViewResult result = (PartialViewResult)await schoolController.ReturnSchoolDetailsPartialViewPopulated(selectedSchool);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.ViewName, Is.EqualTo("_SchoolDetails"));

		Assert.That(result.Model, Is.Not.Null);
		SchoolDetailsViewModel vm = (SchoolDetailsViewModel)result.Model!;
		Assert.That(vm.URN, Is.EqualTo(urn));
		Assert.That(vm.EstablishmentNumber, Is.EqualTo(null));
		Assert.That(vm.SchoolName, Is.EqualTo("The Cardinal Wiseman Catholic School"));
		Assert.That(vm.Street, Is.EqualTo("Greenford Road"));
		Assert.That(vm.Locality, Is.EqualTo(null));
		Assert.That(vm.Address3, Is.EqualTo(null));
		Assert.That(vm.Town, Is.EqualTo("Greenford"));
		Assert.That(vm.CountyDescription, Is.EqualTo(null));
		Assert.That(vm.FullUkPostcode, Is.EqualTo("UB6 9AW"));
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
