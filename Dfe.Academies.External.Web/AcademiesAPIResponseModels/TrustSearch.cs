using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Base;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels;

/// <summary>
/// Container object to pass trust search criteria to API layer :-
/// {{api-host}}/V1/trusts?{BuildRequestUri(trustSearch)}
/// </summary>
public sealed class TrustSearch : PageSearch
{
	public string GroupName { get; }
	public string Ukprn { get; }
	public string CompaniesHouseNumber { get; }

	public TrustSearch(string groupName, string ukprn, string companiesHouseNumber) => 
		(GroupName, Ukprn, CompaniesHouseNumber) = (groupName, ukprn, companiesHouseNumber);
}
