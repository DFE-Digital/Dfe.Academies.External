namespace Dfe.Academies.External.Web.Models;

public record SchoolLandAndBuildings(string? OwnerExplained,
	bool? WorksPlanned,  // should this be y/n enum ??
	string? WorksPlannedExplained,
	DateTime? WorksPlannedDate,
	bool? FacilitiesShared, // should this be y/n enum ??
	string? FacilitiesSharedExplained,
	bool? Grants, // should this be y/n enum ??
	string? GrantsAwardingBodies,
	bool? PartOfPFIScheme, // should this be y/n enum ??
	string? PartOfPFISchemeType,
	bool? PartOfPrioritySchoolsBuildingProgramme, // should this be y/n enum ??
	bool? PartOfBuildingSchoolsForFutureProgramme // should this be y/n enum ??
);