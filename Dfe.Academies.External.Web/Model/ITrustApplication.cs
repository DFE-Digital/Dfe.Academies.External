namespace Dfe.Academies.External.Web.Model
{
    public interface ITrustApplication
    {
        string CreateNewApplication(TrustApplication trustApplication);
        List<TrustApplication> GetCompletedApplications(string username);
        List<TrustApplication> GetPendingApplications(string username);
    }
}