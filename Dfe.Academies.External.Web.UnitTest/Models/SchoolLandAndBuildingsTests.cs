using System;
using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolLandAndBuildingsTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string? schoolBuildLandOwnerExplained = Fixture.Create<string>();
		bool? schoolBuildLandWorksPlanned = false;
		string? schoolBuildLandWorksPlannedExplained = Fixture.Create<string>();
		DateTime? schoolBuildLandWorksPlannedDate = Fixture.Create<DateTime>();
		bool? schoolBuildLandSharedFacilities = false;
		string? schoolBuildLandSharedFacilitiesExplained = Fixture.Create<string>();
		bool? schoolBuildLandGrants = false;
		string? schoolBuildLandGrantsBodies = Fixture.Create<string>();
		bool? schoolBuildLandPfiScheme = false;
		string? schoolBuildLandPfiSchemeType = Fixture.Create<string>();
		bool? schoolBuildLandPriorityBuildingProgramme = false;
		bool? schoolBuildLandFutureProgramme = false;

		var applicationComponent = new SchoolLandAndBuildings(
			schoolBuildLandOwnerExplained,
			schoolBuildLandWorksPlanned,
			schoolBuildLandWorksPlannedExplained,
			schoolBuildLandWorksPlannedDate,
			schoolBuildLandSharedFacilities,
			schoolBuildLandSharedFacilitiesExplained,
			schoolBuildLandGrants,
			schoolBuildLandGrantsBodies,
			schoolBuildLandPfiScheme,
			schoolBuildLandPfiSchemeType,
			schoolBuildLandPriorityBuildingProgramme,
			schoolBuildLandFutureProgramme
		);

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.OwnerExplained, Is.EqualTo(schoolBuildLandOwnerExplained));
		Assert.That(applicationComponent.WorksPlanned, Is.EqualTo(schoolBuildLandWorksPlanned));
		Assert.That(applicationComponent.WorksPlannedExplained, Is.EqualTo(schoolBuildLandWorksPlannedExplained));
		Assert.That(applicationComponent.WorksPlannedDate, Is.EqualTo(schoolBuildLandWorksPlannedDate));
		Assert.That(applicationComponent.FacilitiesShared, Is.EqualTo(schoolBuildLandSharedFacilities));
		Assert.That(applicationComponent.FacilitiesSharedExplained, Is.EqualTo(schoolBuildLandSharedFacilitiesExplained));
		Assert.That(applicationComponent.Grants, Is.EqualTo(schoolBuildLandGrants));
		Assert.That(applicationComponent.GrantsAwardingBodies, Is.EqualTo(schoolBuildLandGrantsBodies));
		Assert.That(applicationComponent.PartOfPFIScheme, Is.EqualTo(schoolBuildLandPfiScheme));
		Assert.That(applicationComponent.PartOfPFISchemeType, Is.EqualTo(schoolBuildLandPfiSchemeType));
		Assert.That(applicationComponent.PartOfPrioritySchoolsBuildingProgramme, Is.EqualTo(schoolBuildLandPriorityBuildingProgramme));
		Assert.That(applicationComponent.PartOfBuildingSchoolsForFutureProgramme, Is.EqualTo(schoolBuildLandFutureProgramme));
	}
}
