using System.Globalization;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.Extensions.Primitives;

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

	public async Task<ConversionApplication?> LoadAndSetApplicationDetails(int applicationId)
	{
		var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(applicationId);

		if (applicationDetails != null)
		{
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, applicationDetails);
		}

		return applicationDetails;
	}

	public async Task<SchoolApplyingToConvert?> LoadAndSetSchoolDetails(int applicationId, int urn)
	{
		var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(applicationId);
		return applicationDetails.Schools.FirstOrDefault(s => s.URN == urn);
	}

	public void LoadAndStoreCachedConversionApplication()
	{
		//// on load - grab draft application from temp
		var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		//// MR:- Need to drop into this pages cache here ready for post / server callback !
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
	}

	public Dictionary<string, string> RetrieveDateTimeComponentsFromDatePicker(IFormCollection form, string formControlName)
	{
		form.TryGetValue($"{formControlName}-day", out StringValues day);
		form.TryGetValue($"{formControlName}-month", out StringValues month);
		form.TryGetValue($"{formControlName}-year", out StringValues year);

		Dictionary<string, string> dateComponents = new()
			{
				{ "day", day },
				{ "month", month },
				{ "year", year }
			};

		return dateComponents;
	}

	protected string SetSchoolApplicationComponentUriFromName(string componentName)
	{
		return componentName.ToLower().Trim() switch
		{
			// V1:-
			"about the conversion" => "/school/SchoolConversionKeyDetails",
			"further information" => "/school/FurtherInformation",
			"finances" => "/school/FinancesReview",
			"future pupil numbers" => "/school/PupilNumbersSummary",
			"land and buildings" => "/school/LandAndBuildingsSummary",
			"consultation" => "/school/ApplicationSchoolConsultationSummary",
			"pre-opening support grant" => "/school/ApplicationPreOpeningSupportGrantSummary",
			"declaration" => "/school/ApplicationDeclaration",
			_ => string.Empty
		};
	}

	private void CacheSelectedSchool(EstablishmentResponse? schoolDetails)
	{
		if (schoolDetails != null)
		{
			SchoolCacheValuesViewModel cachedValuesViewModel = new(Convert.ToInt32(schoolDetails.Urn), schoolDetails.EstablishmentName);

			ViewDataHelper.StoreSerialisedValue(nameof(SchoolCacheValuesViewModel), ViewData, cachedValuesViewModel);
		}
	}

	private SchoolApplyingToConvert? ConvertApiResponseToModel(EstablishmentResponse? schoolDetails)
	{
		if (schoolDetails != null)
		{
			return new SchoolApplyingToConvert(schoolDetails.EstablishmentName, Convert.ToInt32(schoolDetails.Urn), schoolDetails.UPRN);
		}
		else
		{
			return null;
		}
	}

	protected DateTime BuildDateTime(string day, string month, string year)
	{
		if (!string.IsNullOrWhiteSpace(day) && !string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(year))
		{
			string dateString = $"{day.PadLeft(2, '0')}/{month.PadLeft(2, '0')}/{year.PadLeft(4, '0')}";
			string format = "dd/MM/yyyy";

			DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,
				DateTimeStyles.None, out DateTime newDate);

			return newDate;
		}
		else
		{
			return DateTime.MinValue;
		}
	}
}
