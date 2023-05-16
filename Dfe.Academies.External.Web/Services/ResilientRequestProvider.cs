using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Azure;

namespace Dfe.Academies.External.Web.Services
{
	/// <summary>
	/// The resilient request provider.
	/// </summary>
	public sealed class ResilientRequestProvider : IAPIRequestProvider
	{
		/// <summary>
		/// The authorization method.
		/// </summary>
		private const string AuthorizationMethod = "Bearer";

		/// <summary>
		/// The client.
		/// </summary>
		private readonly HttpClient _client;
		private readonly ILogger logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResilientRequestProvider"/> class.
		/// </summary>
		public ResilientRequestProvider(HttpClient client, ILogger logger)
		{
			// consume singular HTTPClient, grabbed from DI config
			this._client = client;
			this.logger = logger;
		}

		/// <inheritdoc/>
		public async Task<TResult> GetAsync<TResult>(string uri, JsonSerializerOptions? options = null, string token = "")
		{
			//this.ClearRequestHeaders(this._client); // MR:- commented out as headers set up StartupExtension

			// don't always have token e.g. token / login 
			if (!string.IsNullOrEmpty(token))
			{
				this.AddBearerTokenAuthenticationHeader(this._client, token);
			}

			var result = await _client.GetFromJsonAsync<TResult>(uri, options);

			return result;
		}

		/// <inheritdoc/>
		public async Task<TResult> PostAsync<TResult, TData>(string uri, TData data, string token = "")
		{
			TResult result = default;
			var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			//this.ClearRequestHeaders(this._client); // MR:- commented out as headers set up StartupExtension

			// don't always have token e.g. token / login 
			if (!string.IsNullOrEmpty(token))
			{
				this.AddBearerTokenAuthenticationHeader(this._client, token);
			}

			var response = await this._client.PostAsync(uri, content);
			var responsecontent = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode) { 
				this.logger.LogError($"Request unsuccessfull, response status code : { response.StatusCode }| response content : { responsecontent }");
			}
;			response.EnsureSuccessStatusCode();

			// using stream reader as below
			result = await this.ConvertResponseContent<TResult>(response);

			return result;
		}

		/// <inheritdoc/>
		public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "")
		{
			TResult result = default;
			var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

			//this.ClearRequestHeaders(this._client); // MR:- commented out as headers set up StartupExtension

			// don't always have token e.g. token / login 
			if (!string.IsNullOrEmpty(token))
			{
				this.AddBearerTokenAuthenticationHeader(this._client, token);
			}

			var response = await this._client.PutAsync(uri, content);
			var responsecontent = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				this.logger.LogError($"Request unsuccessfull, response status code : {response.StatusCode}| response content : {responsecontent}");
			}
			response.EnsureSuccessStatusCode();

			// get PUT response JSON using stream reader as below
			result = await this.ConvertResponseContent<TResult>(response);

			return result;
		}

		/// <inheritdoc/>
		public async Task<bool> DeleteAsync<T>(string uri, T data, string token = "")
		{
			//this.ClearRequestHeaders(this._client); // MR:- commented out as headers set up StartupExtension

			// don't always have token e.g. token / login 
			if (!string.IsNullOrEmpty(token))
			{
				this.AddBearerTokenAuthenticationHeader(this._client, token);
			}

			var httpMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Delete,
				RequestUri = new Uri(uri),
				Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
			};
			var response = await this._client.SendAsync(httpMessage);
			response.EnsureSuccessStatusCode();

			return response.IsSuccessStatusCode;
		}

		/// <summary>
		/// The convert response content.
		/// </summary>
		/// <param name="response">
		/// The response.
		/// </param>
		/// <typeparam name="TResult">
		/// </typeparam>
		/// <returns>
		/// The <see cref="Task"/>.
		/// </returns>
		private async Task<TResult> ConvertResponseContent<TResult>(HttpResponseMessage response)
		{
			TResult result = default;

			var options = new JsonSerializerOptions
			{
				AllowTrailingCommas = true,
				PropertyNameCaseInsensitive = true,
				Converters = {
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				}
			};

			// Alternative JsonConvert below :- using a stream instead - faster & more efficient
			await using var stream = await response.Content.ReadAsStreamAsync();
			using var reader = new StreamReader(stream);
			string text = reader.ReadToEnd();

			if (!string.IsNullOrWhiteSpace(text))
			{
				result = await Task.Run(() => JsonSerializer.Deserialize<TResult>(text, options));
			}

			return result;
		}

		// MR:- commented out as headers set up StartupExtension
		///// <summary>
		///// The clear request headers.
		///// </summary>
		///// <param name="httpClient">
		///// The http client.
		///// </param>
		//private void ClearRequestHeaders(HttpClient httpClient)
		//{
		//    httpClient.DefaultRequestHeaders.Clear();
		//    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//}

		/// <summary>
		/// The add bearer token authentication header.
		/// </summary>
		/// <param name="httpClient">
		/// The http client.
		/// </param>
		/// <param name="token">
		/// The token.
		/// </param>
		private void AddBearerTokenAuthenticationHeader(HttpClient httpClient, string token)
		{
			if (httpClient == null)
				return;

			if (string.IsNullOrWhiteSpace(token))
				return;

			httpClient.DefaultRequestHeaders.Add("Authorization", $"{AuthorizationMethod} {token}");
		}
	}
}
