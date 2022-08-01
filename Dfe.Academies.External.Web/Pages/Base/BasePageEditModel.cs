using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageEditModel : BasePageModel
{
	private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

	protected BasePageEditModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService)
	{
		_conversionApplicationRetrievalService = conversionApplicationRetrievalService;
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

	public async Task<SchoolApplyingToConvert?> LoadAndSetSchoolDetails(int schoolId)
	{
		var schoolDetails = await _conversionApplicationRetrievalService.GetSchool(schoolId);

		if (schoolDetails != null)
		{
			SchoolCacheValuesViewModel cachedValuesViewModel = new(schoolDetails.SchoolId, schoolDetails.SchoolName);

			ViewDataHelper.StoreSerialisedValue(nameof(SchoolCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}

		return schoolDetails;
	}

	protected string SetSchoolApplicationComponentUriFromName(string componentName)
	{
		switch (componentName.ToLower().Trim())
		{
			case "contact details":
				return "/ApplicationSchoolContactDetails";
			case "performance and safeguarding":
				return "/ApplicationSchoolPerformanceAndSafeguarding";
			case "pupil numbers":
				return "School/PupilNumbers";
			case "finances":
				return "/ApplicationSchoolFinances";
			case "partnerships and affiliations":
				return "/ApplicationSchoolPartnershipsAndAffliates";
			case "religious education":
				return "/ApplicationSchoolReligiousEducation";
			case "land and buildings":
				return "/ApplicationSchoolLandAndBuildings";
			case "local authority":
				return "/ApplicationSchoolLocalAuthority";
			default:
				return string.Empty;
		}
	}
}