using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageEditModel : BasePageModel
{
	private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;
	private readonly IReferenceDataRetrievalService _referenceDataRetrievalService;

	protected BasePageEditModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
								IReferenceDataRetrievalService referenceDataRetrievalService)
	{
		_conversionApplicationRetrievalService = conversionApplicationRetrievalService;
		_referenceDataRetrievalService = referenceDataRetrievalService;
	}

	public async Task<ConversionApplication?> LoadAndSetApplicationDetails(int applicationId, ApplicationTypes applicationType)
	{
		var applicationDetails = await _conversionApplicationRetrievalService.GetApplication(applicationId, applicationType);

		if (applicationDetails != null)
		{
			ApplicationCacheValuesViewModel cachedValuesViewModel = new(applicationDetails.Id, applicationDetails.ApplicationType, applicationDetails.ApplicationReference);

			ViewDataHelper.StoreSerialisedValue(nameof(ApplicationCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}

		return applicationDetails;
	}

	public async Task<SchoolApplyingToConvert?> LoadAndSetSchoolDetails(int applicationId, int schoolId)
	{
		var schoolDetails = await _referenceDataRetrievalService.GetSchool(schoolId);

		if (schoolDetails != null)
		{
			SchoolCacheValuesViewModel cachedValuesViewModel = new(schoolDetails.Urn, schoolDetails.Name);

			ViewDataHelper.StoreSerialisedValue(nameof(SchoolCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}

		return new SchoolApplyingToConvert(schoolDetails.Name, schoolDetails.Urn, applicationId, schoolDetails.Ukprn);
	}

	protected string SetSchoolApplicationComponentUriFromName(string componentName)
	{
		switch (componentName.ToLower().Trim())
		{
			case "contact details":
				return "/school/ApplicationSchoolContactDetails";
			case "performance and safeguarding":
				return "/school/ApplicationSchoolPerformanceAndSafeguarding";
			case "pupil numbers":
				return "/school/PupilNumbers";
			case "finances":
				return "/school/ApplicationSchoolFinances";
			case "partnerships and affiliations":
				return "/school/ApplicationSchoolPartnershipsAndAffliates";
			case "religious education":
				return "/school/ApplicationSchoolReligiousEducation";
			case "land and buildings":
				return "/school/ApplicationSchoolLandAndBuildings";
			case "local authority":
				return "/school/ApplicationSchoolLocalAuthority";
			default:
				return string.Empty;
		}
	}
}