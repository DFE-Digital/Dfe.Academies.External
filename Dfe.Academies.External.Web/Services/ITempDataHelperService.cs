using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Dfe.Academies.External.Web.Services;

/// <summary>
/// Helper service to put stuff in and out of TempData[]
/// to handle serialization / de-serialization / type conversion /
/// whether key found in TempData[]
/// </summary>
public interface ITempDataHelperService
{
    T? GetSerialisedValue<T>(string key, ITempDataDictionary tempData);

    void StoreSerialisedValue(string key, ITempDataDictionary tempData, object data);

    string? GetNonSerialisedValue(string key, ITempDataDictionary tempData);

    void StoreNonSerialisedValue(string key, ITempDataDictionary tempData, object data);
}