using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IConversionApplicationCreationService
{
    Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

    Task AddSchoolToApplication(int applicationId, int schoolUrn, string name);

    Task AddTrustToApplication(int applicationId, int trustUkPrn, string name);

    Task ApplicationAddJoinTrustReasons(int applicationId, string applicationJoinTrustReason, int schoolUrn);

    Task ApplicationChangeSchoolNameAndReason(int applicationId, SelectOption changeName,
	    string newSchoolName, int schoolUrn);

    Task ApplicationSchoolTargetConversionDate(int applicationId, int schoolUrn, SelectOption targetDateDifferent, DateTime targetDate, string targetDateExplained);

	Task ApplicationSchoolFuturePupilNumbers(int applicationId, int schoolUrn, int projectedPupilNumbersYear1, int projectedPupilNumbersYear2,
		int projectedPupilNumbersYear3, string schoolCapacityAssumptions, int schoolCapacityPublishedAdmissionsNumber);

	Task ApplicationSchoolContacts(ApplicationSchoolContacts schoolContacts, int applicationId);

	Task ApplicationSchoolLandAndBuildings(SchoolLandAndBuildings schoolLandAndBuildings, int applicationId, int schoolUrn);

	Task ApplicationPreOpeningSupportGrantUpdate(PayFundsTo schoolSupportGrantFundsPaidTo, int applicationId);

	//Task UpdateDraftApplication(ConversionApplication application);
}
