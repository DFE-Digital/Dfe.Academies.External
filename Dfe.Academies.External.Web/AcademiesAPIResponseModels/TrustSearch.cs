using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Base;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels;

/// <summary>
/// Container object to pass trust search criteria to API layer :-
/// {{api-host}}/trusts?api-version=V1&groupName=grammar
/// </summary>
public sealed class TrustSearch : PageSearch
{
	public string GroupName { get; }

	/// <summary>
	/// Unique identifier for a trust. urn is null on trust search
	/// </summary>
	public string Ukprn { get; }

	public string CompaniesHouseNumber { get; }

	public TrustSearch(string groupName, string ukprn, string companiesHouseNumber) => 
		(GroupName, Ukprn, CompaniesHouseNumber) = (groupName, ukprn, companiesHouseNumber);
}
