using System.Collections;
using System.Text;

namespace Dfe.Academies.External.Web.Model
{
    public class ConversionApplication : IConversionApplication
    {
        public int? Id { get; set; }
        public string? UserEmail { get; set; }
        public string? Application { get; set; }
        public string? TrustName { get; set; }
        public List<SchoolOrSchoolsApplyingToConvert>? SchoolOrSchoolsApplyingToConvert { get; set; }       

        public List<ConversionApplication> GetPendingApplications(string username)
        {
            try
            {

                // TODO: Get data from Academisation API
                // TODO: filter by useremail

                List<ConversionApplication> existingApplications = // Mock Demo Data
                new List<ConversionApplication>()
                { 
                    new ConversionApplication() { Id = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "Cambridge Regional college" } } },
                    new ConversionApplication() { Id = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() { Id = 3, SchoolOrSchoolsApplyingToConvertProperty = "Fen Ditton primary school" }, new SchoolOrSchoolsApplyingToConvert() {Id  = 3, SchoolOrSchoolsApplyingToConvertProperty = "Chesterton primary school" }, new SchoolOrSchoolsApplyingToConvert() {Id  = 3, SchoolOrSchoolsApplyingToConvertProperty = "North Cambridge academy"} } },
                    new ConversionApplication() { Id = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "King’s College London Maths school" } } }
                };

                return existingApplications;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CreateNewApplication(ConversionApplication trustApplication)
        {
            try
            {
                string resultOfSave = "";

                // Awaiting design doc
                // TODO: Save to datastore
                // TODO: Check if save successfull            

                return resultOfSave;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ConversionApplication> GetCompletedApplications(string username)
        {
            try
            {  

                // TODO: Get data from Dynamics 365 
                // TODO: filter by useremail

                List<ConversionApplication> existingApplications = // Mock Demo Data
                new List<ConversionApplication>()
                {
                    new ConversionApplication() { Id = 1, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "Harpenden Academy trust", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "St George’s school" } } }
                };

                return existingApplications;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
