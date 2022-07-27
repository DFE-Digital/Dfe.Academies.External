using Dfe.Academies.External.Shared.Tests.Factory;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Academies.External.Integration.Tests.Services;

internal sealed class ReferenceDataRetrievalServiceIntegrationTests: BaseIntegrationTest
{
	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		_factory.Dispose();
	}

	[Test]
	public async Task SearchSchools___Success()
	{
		// arrange
		var referenceDataRetrievalService = _factory.Services.GetRequiredService<IReferenceDataRetrievalService>();
		
		const string name = "wise";
		const string urn = "101934";
		////const string ukprn = "10006563"; // MR:- real world example

		// act
		var schools = await referenceDataRetrievalService.SearchSchools(SchoolFactory.BuildSchoolSearch(name: name, urn: urn));

		// assert
		Assert.That(schools, Is.Not.Null);
		//Assert.That(Is.GreaterThanOrEqualTo(1), schools.Count());
	}

	[Test]
	public async Task GetSchool___Success()
	{
		// arrange
		var referenceDataRetrievalService = _factory.Services.GetRequiredService<IReferenceDataRetrievalService>();
		const int urn = 101934;

		// act
		var school = await referenceDataRetrievalService.GetSchool(urn);

		// assert
		Assert.That(school, Is.Not.Null);
		//Assert.That(Is.GreaterThanOrEqualTo(1), school.Count());
	}

	[Test]
	public async Task GetTrusts___Success()
	{
		// arrange
		var referenceDataRetrievalService = _factory.Services.GetRequiredService<IReferenceDataRetrievalService>();

		const string name = "grammar";
		
		// act
		var trusts = await referenceDataRetrievalService.GetTrusts(TrustFactory.BuildTrustSearch(groupName: name));

		// assert
		Assert.That(trusts, Is.Not.Null);
		//Assert.That(Is.GreaterThanOrEqualTo(1), trusts.Count());
	}

	[Test]
	public async Task GetTrustByUkPrn___Success()
	{
		// arrange
		var referenceDataRetrievalService = _factory.Services.GetRequiredService<IReferenceDataRetrievalService>();
		const string ukprn = "10058464";

		// act
		var trustDetails = await referenceDataRetrievalService.GetTrustByUkPrn(ukprn);

		// assert
		Assert.That(trustDetails, Is.Not.Null);
		//Assert.That(Is.GreaterThanOrEqualTo(1), trustDetails.Count());
	}
}