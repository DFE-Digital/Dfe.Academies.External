using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Dfe.Academies.External.Web.Services;

/// <summary>
/// The view data objects which stored the data only exists during the current request.
/// Once, the view is generated in the browser and it sends the data back to the server from the client, the ViewData object is automatically destroyed and cleared. 
/// </summary>
public static class ViewDataHelper
{
	public static T? GetSerialisedValue<T>(string key, ViewDataDictionary viewData)
	{
		if (viewData.ContainsKey(key))
		{
			return JsonSerializer.Deserialize<T>(viewData[key]?.ToString() ?? string.Empty) ?? default(T);
		}
		else
		{
			return default;
		}
	}

	public static void StoreSerialisedValue(string key, ViewDataDictionary viewData, object data)
	{
		viewData[key] = JsonSerializer.Serialize(data);
	}

	public static string? GetNonSerialisedValue(string key, ViewDataDictionary viewData)
	{
		if (viewData.ContainsKey(key))
		{
			return viewData[key]?.ToString() ?? string.Empty;
		}
		else
		{
			return string.Empty;
		}
	}

	public static void StoreNonSerialisedValue(string key, ViewDataDictionary viewData, object data)
	{
		viewData[key] = data;
	}
}