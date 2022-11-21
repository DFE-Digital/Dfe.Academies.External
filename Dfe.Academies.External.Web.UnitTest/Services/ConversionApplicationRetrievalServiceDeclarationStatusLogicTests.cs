using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.UnitTest.Services;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationRetrievalServiceDeclarationStatusLogicTests
{
	/// <summary>
	/// conversionApplication == null
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ConversionApplicationNullReturns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

		// act
		var declarationStatus = applicationRetrievalService.CalculateApplicationDeclarationStatus(null);

		// assert
		Assert.That(declarationStatus, Is.EqualTo(Status.NotStarted));
	}

	/// <summary>
	/// conversionApplication.ApplicationType == ApplicationTypes.JoinAMat - without school
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task CalculateDeclarationStatus___ApplicationTypeJoinAMatAndNoSchool___Returns___NotStarted()
	{
		// arrange
		string fullFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}ExampleJsonResponses/getApplicationResponse.json";
		string expectedJson = await File.ReadAllTextAsync(fullFilePath);
		var mockFactory = MockHttpClientFactory.SetupMockHttpClientFactory(HttpStatusCode.OK, expectedJson);

		var mockLogger = new Mock<ILogger<ConversionApplicationRetrievalService>>();
		var applicationRetrievalService = new ConversionApplicationRetrievalService(mockFactory.Object, mockLogger.Object);

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewJoinAMatConversionApplicationNoRoles();

		// act
		var trustStatus = applicationRetrievalService.CalculateTrustStatus(conversionApplication);

		// assert
		Assert.That(trustStatus, Is.EqualTo(Status.NotStarted));
	}

	// TODO:- conversionApplication.ApplicationType == ApplicationTypes.JoinAMat - with school

	// TODO:- add tests for CalculateApplicationDeclarationStatus() = returns Completed = selectedSchool?.DeclarationBodyAgree.HasValue 


	// TODO:- conversionApplication.ApplicationType == ApplicationTypes.FormAMat = BuildMinimalFormAMatConversionApplicationNoContributors()

	// BuildFormAMatConversionApplicationWithContributorWithSchool()
}
