using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;
using System;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolLandAndBuildingsTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		int applicationId = Fixture.Create<int>();
		int urn = Fixture.Create<int>();
		string? schoolBuildLandOwnerExplained = Fixture.Create<string>();
		bool? schoolBuildLandSharedFacilities = false;
		bool? schoolBuildLandWorksPlanned = false;
		string? schoolBuildLandWorksPlannedExplained = Fixture.Create<string>();
		DateTime? schoolBuildLandWorksPlannedDate = Fixture.Create<DateTime>();
		bool? schoolBuildLandGrants = false;
		string? schoolBuildLandGrantsBody = Fixture.Create<string>();
		bool? schoolBuildLandPriorityBuildingProgramme = false;
		bool? schoolBuildLandFutureProgramme = false;
		bool? schoolBuildLandPfiScheme = false;
		string? schoolBuildLandPfiSchemeType = Fixture.Create<string>();

		var applicationComponent = new SchoolLandAndBuildings(applicationId, urn,
			schoolBuildLandOwnerExplained,
			schoolBuildLandSharedFacilities,
			schoolBuildLandWorksPlanned,
			schoolBuildLandWorksPlannedExplained,
			schoolBuildLandWorksPlannedDate,
			schoolBuildLandGrants,
			schoolBuildLandGrantsBody,
			schoolBuildLandPriorityBuildingProgramme,
			schoolBuildLandFutureProgramme,
			schoolBuildLandPfiScheme,
			schoolBuildLandPfiSchemeType);

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(applicationComponent.URN, Is.EqualTo(urn));
		Assert.That(applicationComponent.SchoolBuildLandOwnerExplained, Is.EqualTo(schoolBuildLandOwnerExplained));
		Assert.That(applicationComponent.SchoolBuildLandSharedFacilities, Is.EqualTo(schoolBuildLandSharedFacilities));
		Assert.That(applicationComponent.SchoolBuildLandWorksPlanned, Is.EqualTo(schoolBuildLandWorksPlanned));
		Assert.That(applicationComponent.SchoolBuildLandWorksPlannedExplained, Is.EqualTo(schoolBuildLandWorksPlannedExplained));
		Assert.That(applicationComponent.SchoolBuildLandWorksPlannedDate, Is.EqualTo(schoolBuildLandWorksPlannedDate));
		Assert.That(applicationComponent.SchoolBuildLandGrants, Is.EqualTo(schoolBuildLandGrants));
		Assert.That(applicationComponent.SchoolBuildLandGrantsBody, Is.EqualTo(schoolBuildLandGrantsBody));
		Assert.That(applicationComponent.SchoolBuildLandPriorityBuildingProgramme, Is.EqualTo(schoolBuildLandPriorityBuildingProgramme));
		Assert.That(applicationComponent.SchoolBuildLandFutureProgramme, Is.EqualTo(schoolBuildLandFutureProgramme));
		Assert.That(applicationComponent.SchoolBuildLandPFIScheme, Is.EqualTo(schoolBuildLandPfiScheme));
		Assert.That(applicationComponent.SchoolBuildLandPFISchemeType, Is.EqualTo(schoolBuildLandPfiSchemeType));
	}
}