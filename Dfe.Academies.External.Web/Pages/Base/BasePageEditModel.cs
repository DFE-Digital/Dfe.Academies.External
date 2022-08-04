using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageEditModel : BasePageModel
{
	private readonly IReferenceDataRetrievalService _referenceDataRetrievalService;
	public readonly IConversionApplicationRetrievalService ConversionApplicationRetrievalService;
	public const string SchoolOverviewPath = "SchoolOverview";

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

	public async Task<SchoolApplyingToConvert?> LoadAndSetSchoolDetails(int applicationId, int urn)
	{
		var schoolDetails = await _referenceDataRetrievalService.GetSchool(urn);

		if (schoolDetails != null)
		{
			SchoolCacheValuesViewModel cachedValuesViewModel = new(urn, schoolDetails.EstablishmentName);

			ViewDataHelper.StoreSerialisedValue(nameof(SchoolCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}

		return new SchoolApplyingToConvert(schoolDetails.EstablishmentName, urn, applicationId, schoolDetails.UPRN);
	}

	public void LoadAndStoreCachedConversionApplication()
	{
		//// on load - grab draft application from temp
		var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		//// MR:- Need to drop into this pages cache here ready for post / server callback !
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
	}

	protected string SetSchoolApplicationComponentUriFromName(string componentName)
	{
		return componentName.ToLower().Trim() switch
		{
			// V1:-
			"about the conversion" => "/school/AboutTheConversion",
			"further information" => "/school/FurtherInformation",
			"finances" => "/school/Finances",
			"future pupil numbers" => "/school/PupilNumbers",
			"land and buildings" => "/school/LandAndBuildings",
			"consultation" => "/school/ApplicationSchoolConsultation",
			"pre-opening support grant" => "/school/ApplicationPreOpeningSupportGrant",
			"declaration" => "/school/ApplicationDeclaration",
			_ => string.Empty
		};
	}
}