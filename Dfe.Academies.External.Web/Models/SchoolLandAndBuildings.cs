namespace Dfe.Academies.External.Web.Models;

public record SchoolLandAndBuildings(int ApplicationId,
	int URN,
	string? SchoolBuildLandOwnerExplained,
	bool? SchoolBuildLandSharedFacilities, // should this be y/n enum ??
	bool? SchoolBuildLandWorksPlanned,  // should this be y/n enum ??
	string? SchoolBuildLandWorksPlannedExplained,
	DateTime? SchoolBuildLandWorksPlannedDate,
	bool? SchoolBuildLandGrants, // should this be y/n enum ??
	string? SchoolBuildLandGrantsBody,
	bool? SchoolBuildLandPriorityBuildingProgramme, // should this be y/n enum ??
	bool? SchoolBuildLandFutureProgramme, // should this be y/n enum ??
	bool? SchoolBuildLandPFIScheme, // should this be y/n enum ??
	string? SchoolBuildLandPFISchemeType
);