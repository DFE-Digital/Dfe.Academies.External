using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academisation.CorrelationIdMiddleware;
using Dfe.Academies.External.Web.FeatureManagement;

namespace Dfe.Academies.External.Web.UnitTest.Services
{
	//// Submit button should NOT be available unless ConversionStatus == Completed &&&&&&& TrustConversionStatus = Completed !!
	internal sealed class ConversionApplicationRetrievalServiceConversionStatusLogicTests
	{
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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);
			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationNoRoles();
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;
			
			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(null);

			var schoolViewModels = new List<SchoolComponentsViewModel>();
			
			foreach (var school in conversionApplication.Schools)
			{
				var fixture = new Fixture();
				var schoolComponents = new List<ApplicationComponentViewModel>(fixture
					.Build<ApplicationComponentViewModel>().With(x => x.Status, Status.NotStarted).CreateMany());
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);

			// act
			var schoolViewModel = new SchoolComponentsViewModel(applicationId, URN, school.SchoolName,
				applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents);

			// assert
			Assert.That(schoolViewModel.Status, Is.EqualTo(Status.NotStarted));
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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = new List<ApplicationComponentViewModel>(new Fixture()
					.Build<ApplicationComponentViewModel>().With(x => x.Status, Status.InProgress).CreateMany());
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = new List<ApplicationComponentViewModel>(new Fixture()
					.Build<ApplicationComponentViewModel>().With(x => x.Status, Status.InProgress).CreateMany());
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = InProgress. TrustName = set
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = new List<ApplicationComponentViewModel>(new Fixture()
					.Build<ApplicationComponentViewModel>().With(x => x.Status, Status.InProgress).CreateMany());
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = InProgress. TrustName = set
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(conversionApplication.ApplicationId, URN);
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					Status.Completed, schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = InProgress. TrustName = set
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(conversionApplication.ApplicationId, URN);
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					Status.Completed, schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = Completed. TrustName = set, 
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(conversionApplication.ApplicationId, URN);
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = Completed. TrustName = set, 
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(applicationId);
			var schoolViewModels = new List<SchoolComponentsViewModel>();
			foreach (var school in conversionApplication.Schools)
			{
				var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(conversionApplication.ApplicationId, URN);
				schoolViewModels.Add(new SchoolComponentsViewModel(applicationId, URN,
					school.SchoolName,
					Status.InProgress, schoolComponents));
			}

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication, schoolViewModels);

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
			var mockFileUploadService = new Mock<IFileUploadService>();
			var mockConversionGrantExpiryFeature = new Mock<IConversionGrantExpiryFeature>();
			var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object,mockFileUploadService.Object, Mock.Of<ICorrelationContext>(x => x.CorrelationId == Guid.NewGuid()), mockConversionGrantExpiryFeature.Object);

			int applicationId = 25; // hard coded as per example JSON
			int URN = 113537;

			// set trust status = Completed. TrustName = set, 
			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(applicationId);
			var school = conversionApplication.Schools.FirstOrDefault();
			var schoolComponents = await applicationRetrievalService.GetSchoolApplicationComponents(applicationId, URN);
			foreach (ApplicationComponentViewModel schoolComponent in schoolComponents)
			{
				schoolComponent.Status = Status.Completed;
			}
			// act
			var schoolViewModel = new SchoolComponentsViewModel(applicationId, URN, school.SchoolName,
				applicationRetrievalService.CalculateSchoolStatus(schoolComponents), schoolComponents);
			// assert
			Assert.That(schoolViewModel.Status, Is.EqualTo(Status.Completed));
		}
	}
}
