using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationCacheValuesViewModel
{
	public ApplicationCacheValuesViewModel(int applicationId, ApplicationTypes applicationType)
	{
		ApplicationId = applicationId;
		ApplicationType = applicationType;
	}

	public int ApplicationId { get; set; }

	/// <summary>
	/// e.g. 'A2B_xxx'
	/// </summary>
	public string ApplicationReference => $"A2B_{ApplicationId}";

	public ApplicationTypes ApplicationType { get; set; }
}