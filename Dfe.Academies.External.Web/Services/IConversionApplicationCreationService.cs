using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IConversionApplicationCreationService
{
    Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

    Task UpdateDraftApplication(ConversionApplication application);

    Task AddSchoolToApplication(int applicationId, int schoolUrn, string name);

    Task AddTrustToApplication(int applicationId, int trustUkPrn, string name);

    Task ApplicationAddJoinTrustReasons(int applicationId, string applicationJoinTrustReason, int schoolUrn);

    Task ApplicationChangeSchoolNameAndReason(ConversionApplication application, SelectOption changeName,
	    string changeSchoolNameReason, int schoolUrn);

    Task ApplicationSchoolTargetConversionDate(int applicationId, int schoolUrn, DateTime targetDate, string targetDateExplained);

	Task ApplicationSchoolFuturePupilNumbers(int applicationId, int schoolUrn, int projectedPupilNumbersYear1, int projectedPupilNumbersYear2,
		int projectedPupilNumbersYear3, string schoolCapacityAssumptions, int schoolCapacityPublishedAdmissionsNumber);

	Task ApplicationSchoolContacts(ApplicationSchoolContacts schoolContacts);

	Task ApplicationSchoolLandAndBuildings(SchoolLandAndBuildings schoolLandAndBuildings);

	Task ApplicationPreOpeningSupportGrantUpdate(PayFundsTo schoolSupportGrantFundsPaidTo);
}
