namespace Dfe.Academies.External.Web.Dtos;

public class SchoolPerformance
{
	public bool? InspectedButReportNotPublished { get; set; }
	public string? InspectedButReportNotPublishedExplain { get; set; }
	public bool? OngoingSafeguardingInvestigations { get; set; }
	public string? OngoingSafeguardingDetails { get; set; }
}