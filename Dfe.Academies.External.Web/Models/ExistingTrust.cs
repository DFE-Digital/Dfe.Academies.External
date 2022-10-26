namespace Dfe.Academies.External.Web.Models;

public record ExistingTrust(
	int ApplicationId,
	string TrustName,
	int ukprn,
	bool? ChangesToTrust = null,
	string? ChangesToTrustExplained = null,
	bool? ChangesToLaGovernance = null,
	string? ChangesToLaGovernanceExplained = null
);
