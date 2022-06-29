namespace Dfe.Academies.External.Web.Model
{
    public interface IConversionApplication
    {
        string CreateNewApplication(ConversionApplication trustApplication);
        List<ConversionApplication> GetCompletedApplications(string username);
        List<ConversionApplication> GetPendingApplications(string username);
    }
}