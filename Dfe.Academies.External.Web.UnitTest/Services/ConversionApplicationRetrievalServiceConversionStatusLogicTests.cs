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

			var conversionApplication = ConversionApplicationTestDataFactory.BuildJoinAMatConversionApplicationWithContributorWithSchool();

			// act
			var applicationStatus = applicationRetrievalService.CalculateApplicationStatus(conversionApplication);

			// assert
			Assert.That(applicationStatus, Is.EqualTo(Status.NotStarted));
		}

		// school.SchoolApplicationComponents.any = true
		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = not started && conversion status = not started



		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = not started && conversion status = in prog

		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = not started && conversion status = completed

		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = in prog && conversion status = not started

		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = in prog && conversion status = in prog

		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = in prog && conversion status = completed

		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = completed && conversion status = not started

		// TODO:- ApplicationType == ApplicationTypes.JoinAMat && trust status = completed && conversion status = in prog

		// TODO:-  ApplicationType == ApplicationTypes.JoinAMat &&trust status = completed && conversion status = completed

		// TODO:- conversionApplication.ApplicationType == ApplicationTypes.FormAMat etc....

		// TODO:- other tests when the know the FormAMat logic
	}
}
