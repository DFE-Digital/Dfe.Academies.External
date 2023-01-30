using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Dtos;

public record ExistingTrust(
	int ApplicationId,
	string TrustName,
	int ukprn,
	TrustChange? ChangesToTrust = null,
	string? ChangesToTrustExplained = null,
	bool? ChangesToLaGovernance = null,
	string? ChangesToLaGovernanceExplained = null
);
