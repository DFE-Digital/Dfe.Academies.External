using System.Globalization;
using System.Text.Json;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

		if (applicationDetails != null)
		{
			return applicationDetails.Schools.FirstOrDefault(s => s.URN == urn);
		}
		else
		{
			return null;
		}
	}
	
	public void TempDataSetBySchool<T>(int schoolUrn, T value)
	{
		TempData[$"{schoolUrn}-{typeof(T)}"] = JsonSerializer.Serialize(value);
	}
	
	public T TempDataLoadBySchool<T>(int schoolUrn)
	{
		var tempDataKey = $"{schoolUrn}-{typeof(T)}";
		var value = TempDataHelper.GetSerialisedValue<T>(tempDataKey, TempData);
		TempData[tempDataKey] = JsonSerializer.Serialize(value);
		return value;
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

	protected string SetFormAMatComponentUriFromName(string componentName)
	{
		return componentName.ToLower().Trim() switch
		{
			"name of the trust" => "/trust/formamat/applicationnewtrustname",
			"opening date" => "/trust/formamat/applicationnewtrustopeningdatesummary",
			"reasons for forming the trust" => "/trust/formamat/ApplicationNewTrustReasonsSummary",
			"plans for growth" => "/trust/formamat/applicationnewtrustplansforgrowth",
			"school improvement strategy" => "/trust/formamat/applicationnewtrustschoolimprovement",
			"governance structure" => "/trust/formamat/ApplicationNewTrustGovernanceSummary",
			"key people" => "/trust/formamat/applicationnewtrustkeypeople",
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

	/// <summary>
	/// method to run built in model validation ++ custom optional input validation, specific per page!
	/// </summary>
	/// <returns></returns>
	public abstract bool RunUiValidation();

	/// <summary>
	/// Call this func before save / PUT to API, to clear out optional data
	/// i.e. if user changes answer from no -> yes need to clear out optional string data capture
	/// </summary>
	public abstract Dictionary<string, dynamic> PopulateUpdateDictionary();

	/// <summary>
	/// check application contributors vs current user
	/// </summary>
	/// <param name="appId"></param>
	/// <returns></returns>
	public async Task<ActionResult> CheckApplicationPermission(int appId)
	{
		var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

		// check user access
		if (draftConversionApplication != null && !UserIsContributorToApplication(draftConversionApplication))
		{
			return Forbid();
		}

		return new OkResult();
	}
}
