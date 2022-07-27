using Dfe.Academies.External.Web.AcademiesAPIResponseModels;

namespace Dfe.Academies.External.Shared.Tests.Factory;

public static class SchoolFactory
{
	public static SchoolSearch BuildSchoolSearch(string name = "", string urn = "", string ukprn = "")
	{
		return new SchoolSearch(name, urn, ukprn);
	}
}