using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Dfe.Academies.External.Web.Services;

/// <summary>
/// Helper service to put stuff in and out of TempData[]
/// to handle serialization / de-serialization / type conversion /
/// whether key found in TempData[]
/// In the internal mechanism, temp data is basically using session variables
/// </summary>
public static class TempDataHelper
{
    public const string DraftConversionApplicationKey = "draftConversionApplication";
    public const string SelectedSchoolKey = "selectedSchoolKey";

    public static T? GetSerialisedValue<T>(string key, ITempDataDictionary tempData)
    {
        if (tempData.ContainsKey(key))
        {
            return JsonSerializer.Deserialize<T>(tempData[key].ToString() ?? string.Empty) ?? default(T);
        }
        else
        {
            return default;
        }
    }

    public static void StoreSerialisedValue(string key, ITempDataDictionary tempData, object data)
    {
        tempData[key] = JsonSerializer.Serialize(data);
        tempData.Keep(key);
    }

    public static string? GetNonSerialisedValue(string key, ITempDataDictionary tempData)
    {
        if (tempData.ContainsKey(key))
        {
            return tempData[key]?.ToString() ?? string.Empty;
        }
        else
        {
            return string.Empty;
        }
    }

    public static void StoreNonSerialisedValue(string key, ITempDataDictionary tempData, object data)
    {
        tempData[key] = data;
        tempData.Keep(key);
    }
}