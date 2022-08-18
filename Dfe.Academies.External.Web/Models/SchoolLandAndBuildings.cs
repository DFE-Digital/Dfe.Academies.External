namespace Dfe.Academies.External.Web.Models;

public record SchoolLandAndBuildings(int ApplicationId,
	int URN,
	string? SchoolBuildLandOwnerExplained,
	bool? SchoolBuildLandWorksPlanned,  // should this be y/n enum ??
	string? SchoolBuildLandWorksPlannedExplained,
	DateTime? SchoolBuildLandWorksPlannedDate,
	bool? SchoolBuildLandSharedFacilities, // should this be y/n enum ??
	string? SchoolBuildLandSharedFacilitiesExplained,
	bool? SchoolBuildLandGrants, // should this be y/n enum ??
	string? SchoolBuildLandGrantsBodies,
	bool? SchoolBuildLandPFIScheme, // should this be y/n enum ??
	string? SchoolBuildLandPFISchemeType,
	bool? SchoolBuildLandPriorityBuildingProgramme, // should this be y/n enum ??
	bool? SchoolBuildLandFutureProgramme // should this be y/n enum ??
);