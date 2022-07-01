using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Dfe.Academies.External.Web.Services;

/// <summary>
/// Helper service to put stuff in and out of TempData[]
/// to handle serialization / de-serialization / type conversion /
/// whether key found in TempData[]
/// </summary>
public class TempDataHelperService: ITempDataHelperService
{
    public T? GetSerialisedValue<T>(string key, ITempDataDictionary tempData)
    {
        if (tempData.ContainsKey(key))
        {
            return JsonSerializer.Deserialize<T>(tempData[key].ToString() ?? string.Empty) ?? default(T);
        }
        else
        {
            return default(T);
        }
    }

    public void StoreSerialisedValue(string key, ITempDataDictionary tempData, object data)
    {
        tempData[key] = JsonSerializer.Serialize(data);
    }

    public string? GetNonSerialisedValue(string key, ITempDataDictionary tempData)
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

    public void StoreNonSerialisedValue(string key, ITempDataDictionary tempData, object data)
    {
        tempData[key] = data;
    }
}