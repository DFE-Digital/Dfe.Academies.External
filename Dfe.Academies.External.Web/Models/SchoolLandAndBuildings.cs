namespace Dfe.Academies.External.Web.Models;

public class SchoolLandAndBuildings
{
    public int Id { get; set; }

    public int SchoolId { get; set; }

    public string SchoolBuildLandOwnerExplained { get; set; }
    public bool? SchoolBuildLandSharedFacilities { get; set; }
    public string SchoolBuildLandSharedFacilitiesExplained { get; set; }
    public bool? SchoolBuildLandWorksPlanned { get; set; }
    public string SchoolBuildLandWorksPlannedExplained { get; set; }
    public DateTime? SchoolBuildLandWorksPlannedDate { get; set; }
    public bool? SchoolBuildLandGrants { get; set; }
    public string SchoolBuildLandGrantsBody { get; set; }
    public bool? SchoolBuildLandPriorityBuildingProgramme { get; set; }
    public bool? SchoolBuildLandFutureProgramme { get; set; }
    public bool? SchoolBuildLandPFIScheme { get; set; }
    public string SchoolBuildLandPFISchemeType { get; set; }
}