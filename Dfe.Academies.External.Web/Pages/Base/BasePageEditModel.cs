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

	public async Task LoadAndSetApplicationDetails(int applicationId)
	{
		var applicationDetails = await _conversionApplicationRetrievalService.GetApplication(applicationId);

		if (applicationDetails != null)
		{
			ApplicationCacheValuesViewModel cachedValuesViewModel = new(applicationDetails.Id, applicationDetails.ApplicationType);

			ViewDataHelper.StoreSerialisedValue(nameof(ApplicationCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}
	}

	public async Task LoadAndSetSchoolDetails(int schoolId)
	{
		var schoolDetails = await _conversionApplicationRetrievalService.GetSchool(schoolId);

		if (schoolDetails != null)
		{
			SchoolCacheValuesViewModel cachedValuesViewModel = new(schoolDetails.SchoolId, schoolDetails.SchoolName);

			ViewDataHelper.StoreSerialisedValue(nameof(SchoolCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}
	}
}