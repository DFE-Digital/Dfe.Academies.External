using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageEditModel : BasePageModel
{
	public readonly IConversionApplicationRetrievalService ConversionApplicationRetrievalService;
	private readonly IReferenceDataRetrievalService _referenceDataRetrievalService;

	protected BasePageEditModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
								IReferenceDataRetrievalService referenceDataRetrievalService)
	{
		ConversionApplicationRetrievalService = conversionApplicationRetrievalService;
		_referenceDataRetrievalService = referenceDataRetrievalService;
	}

	public async Task<ConversionApplication?> LoadAndSetApplicationDetails(int applicationId, ApplicationTypes applicationType)
	{
		var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(applicationId, applicationType);

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
			SchoolCacheValuesViewModel cachedValuesViewModel = new(int.Parse(schoolDetails.Urn), schoolDetails.Name);

			ViewDataHelper.StoreSerialisedValue(nameof(SchoolCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}

		return new SchoolApplyingToConvert(schoolDetails.Name, int.Parse(schoolDetails.Urn), applicationId, schoolDetails.UPRN);
	}

	protected string SetSchoolApplicationComponentUriFromName(string componentName)
	{
		switch (componentName.ToLower().Trim())
		{
			// V1:-
			case "about the conversion":
				return "/school/AboutTheConversion";
			case "further information":
				return "/school/FurtherInformation";
			case "finances":
				return "/school/Finances";
			case "future pupil numbers":
				return "/school/PupilNumbers";
			case "land and buildings":
				return "/school/LandAndBuildings";
			case "consultation":
				return "/school/ApplicationSchoolConsultation";
			case "pre-opening support grant":
				return "/school/ApplicationPreOpeningSupportGrant";
			case "declaration":
				return "/school/ApplicationDeclaration";
			//// V2:-
			////case "contact details":
			////	return "/school/ApplicationSchoolContactDetails";
			////case "performance and safeguarding":
			////	return "/school/ApplicationSchoolPerformanceAndSafeguarding";
			////case "partnerships and affiliations":
			////	return "/school/ApplicationSchoolPartnershipsAndAffliates";
			////case "religious education":
			////	return "/school/ApplicationSchoolReligiousEducation";
			////case "local authority":
			////	return "/school/ApplicationSchoolLocalAuthority";
			default:
				return string.Empty;
		}
	}
}