using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Base
{
	public sealed class ApiListWrapper<T>
	{
		[JsonPropertyName("data")] 
		public IList<T> Data { get; }
		
		[JsonPropertyName("paging")]
		public Pagination Paging { get; }

		[JsonConstructor]
		public ApiListWrapper(IList<T> data, Pagination paging) => (Data, Paging) = (data, paging);

		public class Pagination
		{
			[JsonPropertyName("page")]
			public int Page { get; }
			
			[JsonPropertyName("recordCount")]
			public int RecordCount { get; }
			
			[JsonPropertyName("nextPageUrl")]
			public string NextPageUrl { get; }
			
			[JsonConstructor]
			public Pagination(int page, int recordCount, string nextPageUrl) => 
				(Page, RecordCount, NextPageUrl) = (page, recordCount, nextPageUrl);
		}
	}
}