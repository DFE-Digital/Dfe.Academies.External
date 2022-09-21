using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models
{
	public record SchoolFinancialYear(
		DateTime? FinancialYearEndDate = null,
		decimal? Revenue = null,
		RevenueType? RevenueStatus = null,
		string? RevenueStatusExplained = null,
		string? RevenueStatusFileLink = null,
		decimal? CapitalCarryForward = null,
		RevenueType? CapitalCarryForwardStatus = null,
		string? CapitalCarryForwardExplained = null,
		string? CapitalCarryForwardFileLink = null
		);
}
