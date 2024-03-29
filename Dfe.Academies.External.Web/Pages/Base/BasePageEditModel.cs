﻿using System.Globalization;
using System.Text.Json;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BasePageEditModel : BasePageModel
{
	public readonly IReferenceDataRetrievalService ReferenceDataRetrievalService;
	public readonly IConversionApplicationRetrievalService ConversionApplicationRetrievalService;
	public const string SchoolOverviewPath = "SchoolOverview";
	
	[BindProperty]
	public ApplicationTypes ApplicationType { get; set; }

	protected BasePageEditModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
								IReferenceDataRetrievalService referenceDataRetrievalService)
	{
		ConversionApplicationRetrievalService = conversionApplicationRetrievalService;
		ReferenceDataRetrievalService = referenceDataRetrievalService;
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
			ApplicationType = applicationDetails.ApplicationType;
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
