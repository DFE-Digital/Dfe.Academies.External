namespace Dfe.Academies.External.Web.Model
{
    public interface ITrustApplication
    {
        string Application { get; set; }

        string CreateNewApplication(TrustApplication trustApplication);
        List<TrustApplication> GetCompletedtingApplications(string username);
        List<TrustApplication> GetPendingApplications(string username);
    }
}