using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationCacheValuesViewModel
{
	public ApplicationCacheValuesViewModel()
	{

	}

	public ApplicationCacheValuesViewModel(int applicationId, ApplicationTypes applicationType, string applicationReference)
	{
		ApplicationId = applicationId;
		ApplicationType = applicationType;
		ApplicationReference = applicationReference;
	}

	public int ApplicationId { get; set; }

	/// <summary>
	/// e.g. 'A2B_xxx'
	/// </summary>
	public string ApplicationReference { get; set; }

	public ApplicationTypes ApplicationType { get; set; }
}