using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;

namespace Dfe.Academies.External.Web.UnitTest.Services
{
	//// Submit button should NOT be available unless ConversionStatus == Completed &&&&&&& TrustConversionStatus = Completed !!
	internal sealed class ConversionApplicationRetrievalServiceConversionStatusLogicTests
	{
		/// <summary>
		/// conversionApplication == null
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ConversionApplicationNullReturns___NotStarted()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(null);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.NotStarted));
		}

		/// <summary>
		/// conversionApplication.ApplicationType == ApplicationTypes.JoinAMat - without school
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatAndNoSchool___Returns___NotStarted()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationNoRoles();

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.NotStarted));
		}

		/// <summary>
		/// conversionApplication.ApplicationType == ApplicationTypes.JoinAMat - with school but nothing filled in !
		/// && school.SchoolApplicationComponents.any = false
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatAndSchool___Returns___NotStarted()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(null);

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.NotStarted));
		}
		
		/// <summary>
		/// school.SchoolApplicationComponents.any = true
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = not started && conversion status = not started
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatAndSchoolAndComponents___Returns___NotStarted()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.NotStarted));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = not started && conversion status = in prog
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatAndSchoolAndComponents___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// set one SchoolApplicationComponents = InProgress. means schoolConversionStatus = InProgress
			var firstComponent = school!.SchoolApplicationComponents.FirstOrDefault();
			firstComponent!.Status = Status.InProgress;
			
			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = not started && conversion status = completed
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatAndSchoolAndComponents___Returns___InProgress2()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// set EVERY SchoolApplicationComponents = Completed. means schoolConversionStatus = Completed
			foreach (var component in school!.SchoolApplicationComponents)
			{
				component!.Status = Status.Completed;
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = InProgress && conversion status = not started
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatTrustStatusInProgress___ConversionStatusNotStarted___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = InProgress. TrustName = set
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = InProgress && conversion status = InProgress
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatTrustStatusInProgress___ConversionStatusInProgress___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = InProgress. TrustName = set
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// set one SchoolApplicationComponents = InProgress. means schoolConversionStatus = InProgress
			var firstComponent = school!.SchoolApplicationComponents.FirstOrDefault();
			firstComponent!.Status = Status.InProgress;

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = InProgress && conversion status = Completed
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatTrustStatusInProgress___ConversionStatusCompleted___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = InProgress. TrustName = set
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// set EVERY SchoolApplicationComponents = Completed. means schoolConversionStatus = Completed
			foreach (var component in school!.SchoolApplicationComponents)
			{
				component.Status = Status.Completed;
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = Completed && conversion status = not started
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatTrustStatusCompleted___ConversionStatusNotStarted___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = Completed. TrustName = set, 
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = Completed && conversion status = InProgress
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatTrustStatusCompleted___ConversionStatusInProgress___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = Completed. TrustName = set, 
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// set one SchoolApplicationComponents = InProgress. means schoolConversionStatus = InProgress
			var firstComponent = school!.SchoolApplicationComponents.FirstOrDefault();
			firstComponent!.Status = Status.InProgress;

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.InProgress));
		}

		/// <summary>
		/// ApplicationType == ApplicationTypes.JoinAMat && trust status = Completed && conversion status = Completed
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeJoinAMatTrustStatusCompleted___ConversionStatusCompleted___Returns___InProgress()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponseBasicJoinAMat.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = Completed. TrustName = set, 
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// set EVERY SchoolApplicationComponents = Completed. means schoolConversionStatus = Completed
			foreach (var component in school!.SchoolApplicationComponents)
			{
				component.Status = Status.Completed;
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.Completed));
		}

		/// <summary>
		/// conversionApplication.ApplicationType == ApplicationTypes.FormAMat - with school
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task CalculateApplicationStatus___ApplicationTypeFormAMatAndSchool___Returns___NotStarted()
		{
			// arrange
			string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
			string expectedJson = await File.ReadAllTextAsync(fullFilePath);
			var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);
			var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildFormAMatConversionApplicationWithContributorWithSchool();
			var school = conversionApplication.Schools.FirstOrDefault();
			school!.SchoolApplicationComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// act
			var declarationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
		}

		// TODO:- other tests when the know the FormAMat logic
	}
}
