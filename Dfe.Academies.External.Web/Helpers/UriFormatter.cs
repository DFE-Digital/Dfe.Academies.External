namespace Dfe.Academies.External.Web.Helpers
{
	public static class UriFormatter
	{
		public static string SetSchoolApplicationComponentUriFromName(string componentName)
		{
			return componentName.ToLower().Trim() switch
			{
				// V1:-
				"about the conversion" => "/school/SchoolConversionKeyDetails",
				"further information" => "/school/FurtherInformationSummary",
				"finances" => "/school/FinancesReview",
				"future pupil numbers" => "/school/PupilNumbersSummary",
				"land and buildings" => "/school/LandAndBuildingsSummary",
				"consultation" => "/school/ApplicationSchoolConsultationSummary",
				"pre-opening support grant" => "/school/ApplicationPreOpeningSupportGrantSummary",
				"declaration" => "/school/DeclarationSummary",
				_ => string.Empty
			};
		}
		
		public static string SetFormAMatComponentUriFromName(string componentName)
		{
			return componentName.ToLower().Trim() switch
			{
				"name of the trust" => "/trust/formamat/ApplicationNewTrustName",
				"opening date" => "/trust/formamat/applicationnewtrustopeningdatesummary",
				"reasons for forming the trust" => "/trust/formamat/ApplicationNewTrustReasonsSummary",
				"plans for growth" => "/trust/formamat/ApplicationNewTrustGrowthSummary",
				"school improvement strategy" => "/trust/formamat/ApplicationNewTrustImprovementStrategySummary",
				"governance structure" => "/trust/formamat/applicationnewtrustgovernancesummary",
				"key people" => "/trust/formamat/applicationnewtrustkeypeoplesummary",
				_ => string.Empty
			};
		}
	}
}
