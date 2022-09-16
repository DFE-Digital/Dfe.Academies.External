namespace Dfe.Academies.External.Web.Models
{
	public record SchoolFinancialYear(
		DateTime? FinancialYearEndDate = null,
		decimal? Revenue = null,
		string? RevenueStatus = null,
		string? RevenueStatusExplained = null,
		string? RevenueStatusFileLink = null,
		decimal? CapitalCarryForward = null,
		string? CapitalCarryForwardStatus = null,
		string? CapitalCarryForwardExplained = null,
		string? CapitalCarryForwardFileLink = null
		);
}
