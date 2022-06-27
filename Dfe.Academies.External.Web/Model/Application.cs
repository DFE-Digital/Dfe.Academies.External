using System.Text;

namespace Dfe.Academies.External.Web.Model
{
    public class TrustApplication
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public string TrustName { get; set; }
        public string SchoolOrSchoolsApplyingToConvert { get; set; }

        public List<TrustApplication> GetPendingApplications(string username)
        {
            // TODO: Get data from Dynamics 365

            // Use for SchoolOrSchoolsApplyingToConvert
            StringBuilder li = new StringBuilder();
            li.Append(@"<ul>");
            li.Append(@"<li>Cambridge Regional college</li>");
            li.Append(@"</ul>");          

            StringBuilder li2 = new StringBuilder();
            li2.Append(@"<ul>");
            li2.Append(@"<li>Fen Ditton primary school</li>");
            li2.Append(@"<li>Chesterton primary school</li>");
            li2.Append(@"<li>North Cambridge academy</li>");
            li2.Append(@"</ul>");

            StringBuilder li3 = new StringBuilder();
            li3.Append(@"<ul>");
            li3.Append(@"<li>King’s College London Maths school</li>");
            li3.Append(@"</ul>");

            List<TrustApplication> existingApplications = // Mock Demo Data
            new List<TrustApplication>()
            {
                new TrustApplication() { Id = 2, Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust", SchoolOrSchoolsApplyingToConvert = li.ToString()},
                new TrustApplication() { Id = 3, Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust", SchoolOrSchoolsApplyingToConvert = li2.ToString()},
                new TrustApplication() { Id = 4, Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example", SchoolOrSchoolsApplyingToConvert = li3.ToString()},
            };

            return existingApplications;
        }

        public string CreateNewApplication(TrustApplication trustApplication)
        {
            string resultOfSave = "";
            
            // Awaiting design doc
            // TODO: Save to datastore
            // TODO: Check if save successfull            

            return resultOfSave; 
        }

        public List<TrustApplication> GetCompletedtingApplications()
        {
            // TODO: Get data from Dynamics 365 
            List<TrustApplication> completedApplications = // Mock Demo Data
            new List<TrustApplication>()
            {
                new TrustApplication() { Id = 1, Application = "J", TrustName = ""},
            };

            return completedApplications;
        }
    }
}
