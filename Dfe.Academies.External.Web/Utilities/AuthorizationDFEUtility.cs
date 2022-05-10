using System.Net.Http.Headers;
using System.Text.Json;

namespace Dfe.Academies.External.Web.Utilities
{
	public static class AuthorizationDFEUtility
	{
		public static async Task<bool> UserHasService(string? userId, string serviceId, string organizationId, string dfeSignApiUrl, string dfeApiKey)
		{
			// HACK: need to speak to DfE Sign-in team to find out how this should work
			return true;

			if (userId == null) return false;
			
			using (var client = new HttpClient())
			{
				var url = $"{dfeSignApiUrl}/services/{serviceId}/organisations/{organizationId}/users/{userId}";
				var message = new HttpRequestMessage(HttpMethod.Get, url);
				message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", dfeApiKey);
				var response = await client.SendAsync(message);

				if (response.IsSuccessStatusCode)
				{
					var responseAsString = await response.Content.ReadAsStringAsync();

					var body = JsonDocument.Parse(responseAsString);

					// ToDo: need to replace the line below with the correct System.Text.Json implementation
					return false;
					//return (body["serviceId"] != null && ((JValue)body["serviceId"]).Value.ToString() == serviceId);
				}

				return false;
			}
		}
	}
}