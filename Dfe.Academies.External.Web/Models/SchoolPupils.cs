namespace Dfe.Academies.External.Web.Models;

public class SchoolPupils
{
    public int Id { get; set; }
    public int SchoolId { get; set; }

    public int? ProjectedPupilNumbersYear1 { get; set; }
    public int? ProjectedPupilNumbersYear2 { get; set; }
    public int? ProjectedPupilNumbersYear3 { get; set; }
    public string SchoolCapacityAssumptions { get; set; }
    public int? SchoolCapacityPublishedAdmissionsNumber { get; set; }

    public int? YearOneProjectedCapacity { get; set; }
    public int? YearOneProjectedPupilNumbers { get; set; }
    public int? YearTwoProjectedCapacity { get; set; }
    public int? YearTwoProjectedPupilNumbers { get; set; }
    public int? YearThreeProjectedCapacity { get; set; }
    public int? YearThreeProjectedPupilNumbers { get; set; }
    public string SchoolPupilForecastsAdditionalInformation { get; set; }
}