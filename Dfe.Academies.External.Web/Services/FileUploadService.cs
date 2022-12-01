﻿using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Dfe.Academies.External.Web.Helpers;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dfe.Academies.External.Web.Services;

public interface IFileUploadService
{
	Task<List<string>> GetFiles(string entityName, string recordId, string recordName, string fieldName);
	Task<string> UploadFile(string entity, string recordId, string recordName, string fieldName, IFormFile file);
	Task DeleteFile(string entityName, string recordId, string recordName, string fieldName, string fileName);
}

public class FileUploadService : IFileUploadService
{
	private readonly HttpClient _httpClient;
	private readonly IAadAuthorisationHelper _aadAuthorisationHelper;

	public FileUploadService() { }

	public FileUploadService(HttpClient httpClient, IAadAuthorisationHelper aadAuthorisationHelper)
		{
			_httpClient = httpClient;
			_aadAuthorisationHelper = aadAuthorisationHelper;
		}

		public async Task<List<string>> GetFiles(string entityName, string recordId, string recordName, string fieldName)
		{
			var url = $"?entityName={entityName}&recordName={recordName}&recordId={recordId}&fieldName={fieldName}";
			
			using var request = new HttpRequestMessage(HttpMethod.Get, url);

			var content = await DoHttpRequest(request);

			return ParseJResponse(content);
		}
		
		public async Task<string> UploadFile(string entity, string recordId, string recordName, string fieldName, IFormFile file)
		{
			using var memoryStream = new MemoryStream();
			file.CopyTo(memoryStream);

			var fileName = Path.GetFileName(file.FileName);
			var newFile = new JObject
			{
				{"entity", entity},
				{"fileName", string.IsNullOrEmpty(fileName) ? fileName : Regex.Replace(fileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)},
				{"fieldName", fieldName},
				{"recordName", recordName},
				{"recordId", recordId},
				{"fileContentBase64", Convert.ToBase64String(memoryStream.ToArray())}
			};

			using var request = new HttpRequestMessage(HttpMethod.Post, "")
			{
				Content = new StringContent(GetJsonAsString(newFile), Encoding.UTF8, "application/json")
			};
			var response = await DoHttpRequest(request);
			return response;
		}
		
		public async Task DeleteFile(string entityName, string recordId, string recordName, string fieldName, string fileName)
		{
			var url = $@"?entityName={entityName}&recordName={recordName}&recordId={recordId}&fieldName={fieldName}&fileName={fileName}";

			using var request = new HttpRequestMessage(HttpMethod.Delete, url);
			await DoHttpRequest(request);
		}

		private async Task<string> DoHttpRequest(HttpRequestMessage request)
		{
			var accessToken = await _aadAuthorisationHelper.GetAccessToken();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
				throw new Exception($"The file upload service failed with a status of {response.ReasonPhrase}");

			var receiveStream = await response.Content.ReadAsStreamAsync();
			using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
			return await readStream.ReadToEndAsync();
		}
		
		private List<string> ParseJResponse(string content)
		{
			var jobject = JObject.Parse(content);
			var jfiles = (JArray)jobject?.GetValue("Files", StringComparison.OrdinalIgnoreCase)!;
			return jfiles.Select(x => (string)x).ToList();
		}

		internal static string GetJsonAsString(JObject jObject)
		{
			var sb = new StringBuilder();
			sb.Append("\"");
			using (var sw = new StringWriter(sb))
			using (var writer = new JsonTextWriter(sw))
			{
				writer.QuoteChar = '\'';

				var ser = new JsonSerializer();
				ser.Serialize(writer, jObject);
			}

			sb.Append("\"");

			return sb.ToString();
		}
}
