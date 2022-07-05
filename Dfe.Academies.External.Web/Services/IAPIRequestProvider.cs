namespace Dfe.Academies.External.Web.Services
{
    /// <summary>
    /// The Request Provider interface. Generic API provider interface
    /// </summary>
    public interface IAPIRequestProvider
    {
        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        /// <summary>
        /// The post async.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <typeparam name="TData">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TResult> PostAsync<TResult, TData>(string uri, TData data, string token = "", string header = "");

        /// <summary>
        /// The put async.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> DeleteAsync(string uri, string token = "");
    }
}
