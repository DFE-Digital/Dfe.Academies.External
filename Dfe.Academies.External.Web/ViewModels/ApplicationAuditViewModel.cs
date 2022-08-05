namespace Dfe.Academies.External.Web.ViewModels;

/// <summary>
/// Just a plain old view model for the 'Recent Activity' partial.
/// Just a who / what / when
/// </summary>
public sealed class ApplicationAuditViewModel
{
	public ApplicationAuditViewModel(string who, string what)
	{
		Who = who;
		What = what;
	}

    public DateTime When { get; set; }

    public string Who { get; set; }

    /// <summary>
    /// This is a full friendly description for UI
    /// e.g. Richard Dickenson started the application.
    /// There maybe other detailed logging in the data store
    /// </summary>
    public string What { get; set; }
}