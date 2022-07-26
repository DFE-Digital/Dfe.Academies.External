using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Base
{
	public sealed class ApiWrapper<T>
	{
		[JsonPropertyName("data")]
		public T Data { get; }
		
		[JsonConstructor]
		public ApiWrapper(T data) => (Data) = (data);
	}
}