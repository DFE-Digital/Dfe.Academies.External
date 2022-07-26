using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ResilientRequestProvider"/> class.
        /// </summary>
        public ResilientRequestProvider(HttpClient client)
        {
            // consume singular HTTPClient, grabbed from DI config
            this._client = client;
        }
        
        /// <inheritdoc/>
        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            TResult result = default;

            // clear headers before putting on bearer / auth, otherwise buggo
            this.ClearRequestHeaders(this._client);

            // don't always have token e.g. token / login 
            if (!string.IsNullOrEmpty(token))
            {
                this.AddBearerTokenAuthenticationHeader(this._client, token);
            }

            var response = await this._client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            // using stream reader as below
            result = await this.ConvertResponseContent<TResult>(response);

            return result;
        }

        /// <inheritdoc/>
        public async Task<TResult> PostAsync<TResult, TData>(string uri, TData data, string token = "", string header = "")
        {
            TResult result = default;
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // clear headers before putting on bearer / auth, otherwise buggo
            this.ClearRequestHeaders(this._client);

            // api Key for academies API added by client factory
            if (!string.IsNullOrEmpty(header))
            {
                this.AddHeaderParameter(this._client, header);
            }

            // don't always have token e.g. token / login 
            if (!string.IsNullOrEmpty(token))
            {
                this.AddBearerTokenAuthenticationHeader(this._client, token);
            }

            var response = await this._client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            // using stream reader as below
            result = await this.ConvertResponseContent<TResult>(response);

            return result;
        }
        
        /// <inheritdoc/>
        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            TResult result = default;
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            // clear headers before putting on bearer / auth, otherwise buggo
            this.ClearRequestHeaders(this._client);

            if (!string.IsNullOrEmpty(header))
            {
                this.AddHeaderParameter(this._client, header);
            }

            // don't always have token e.g. token / login 
            if (!string.IsNullOrEmpty(token))
            {
                this.AddBearerTokenAuthenticationHeader(this._client, token);
            }

            var response = await this._client.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();

            // using stream reader as below
            result = await this.ConvertResponseContent<TResult>(response);

            return result;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(string uri, string token = "")
        {
            // clear headers before putting on bearer / auth, otherwise buggo
            this.ClearRequestHeaders(this._client);

            // don't always have token e.g. token / login 
            if (!string.IsNullOrEmpty(token))
            {
                this.AddBearerTokenAuthenticationHeader(this._client, token);
            }

            var response = await this._client.DeleteAsync(uri);
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
            // Alternative JsonConvert below :- using a stream instead - faster & more efficient
            await using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            TResult result = await Task.Run(() => JsonSerializer.Deserialize<TResult>(text));
            return result;
        }

        /// <summary>
        /// The add header parameter.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        /// <summary>
        /// The clear request headers.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        private void ClearRequestHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
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
