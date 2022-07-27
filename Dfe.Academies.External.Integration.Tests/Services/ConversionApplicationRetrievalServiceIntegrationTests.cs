using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Academies.External.Integration.Tests.Services;

internal sealed class ConversionApplicationRetrievalServiceIntegrationTests : BaseIntegrationTest
{
	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		_factory.Dispose();
	}

	//[Test]
	public void GetCompletedApplications_Success()
	{
		// arrange
		var conversionApplicationRetrievalService = _factory.Services.GetRequiredService<IConversionApplicationRetrievalService>();
		const string userName = "test@education.gov.uk";
		const int expectedCount = 2;

		// act
		var conversionApplications = conversionApplicationRetrievalService.GetCompletedApplications(userName);

		// assert
		Assert.That(conversionApplications, Is.Not.Null);
		Assert.That(conversionApplications.Count, Is.EqualTo(expectedCount), "Count is not correct");
	}

	// TODO MR:- GetPendingApplications()

	// TODO MR:- GetSchoolApplicationComponents()

	// TODO MR:- GetApplication()
}