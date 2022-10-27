using System.Net.Http.Headers;
using System.Text;
using Dfe.Academies.External.Web.Helpers;

namespace Dfe.Academies.External.Web.Services;

public interface IFileUploadService
{
	Task<List<string>> GetFileNames(string entityName, string recordId, string recordName, string fieldName);
}

public class FileUploadService : IFileUploadService
{
	private readonly HttpClient _httpClient;
	private readonly IAadAuthorisationHelper _aadAuthorisationHelper;
		public FileUploadService(HttpClient httpClient, IAadAuthorisationHelper aadAuthorisationHelper)
		{
			_httpClient = httpClient;
			_aadAuthorisationHelper = aadAuthorisationHelper;
		}

		private async Task<string> GetFile(string entityName, string recordId, string recordName, string fieldName)
		{
			var url = $"?entityName={entityName}&recordName={recordName}&recordId={recordId}&fieldName={fieldName}";
			
			using var request = new HttpRequestMessage(HttpMethod.Get, url);

			
			var accessToken = await _aadAuthorisationHelper.GetAccessToken();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
				throw new Exception($"The request failed with a status of {response.ReasonPhrase}. \n\n" + await request.Content.ReadAsStringAsync());

			var receiveStream = await response.Content.ReadAsStreamAsync();
			using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
			return await readStream.ReadToEndAsync();
		}

		public async Task<List<string>> GetFileNames(string entityName, string recordId, string recordName, string fieldName)
		{
			var fileResponse = await GetFile(entityName, recordId, recordName, fieldName);
			

			return new List<string>(){fileResponse};
		}
}
