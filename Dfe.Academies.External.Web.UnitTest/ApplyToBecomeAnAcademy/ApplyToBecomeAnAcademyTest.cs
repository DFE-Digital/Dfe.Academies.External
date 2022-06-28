using Dfe.Academies.External.Web.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace DfE.Academies.External.Web.UnitTest.Routing
{
	public class ApplyToBecomeAnAcademyTest
	{
       
        [Test]
		public void GetPendingApplications()
		{

            TrustApplication trustApplication = new TrustApplication();

            // Mock data
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

            // Mock data
            List<TrustApplication> existingApplicationsTestData =
            new List<TrustApplication>()
            {
                new TrustApplication() { Id = 2, Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust", SchoolOrSchoolsApplyingToConvert = li.ToString()},
                new TrustApplication() { Id = 3, Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust", SchoolOrSchoolsApplyingToConvert = li2.ToString()},
                new TrustApplication() { Id = 4, Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example", SchoolOrSchoolsApplyingToConvert = li3.ToString()},
            };

            Assert.AreEqual(existingApplicationsTestData.Count, trustApplication.GetPendingApplications("Username").Count, "Count is not correct");
            Assert.AreEqual(existingApplicationsTestData.ToArray()[0].Application, trustApplication.GetPendingApplications("Username").ToArray()[0].Application, "Pending data not found");
            Assert.AreEqual(existingApplicationsTestData.ToArray()[0].TrustName, trustApplication.GetPendingApplications("Username").ToArray()[0].TrustName, "Pending data not found");
            Assert.AreEqual(existingApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert, trustApplication.GetPendingApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert, "Pending data not found");

        }

        [Test]
        public void GetCompletedApplications()
        {

            TrustApplication trustApplication = new TrustApplication();

            // Mock data
            StringBuilder li = new StringBuilder();
            li.Append(@"<ul>");
            li.Append(@"<li>St George’s school</li>");
            li.Append(@"</ul>");

            // Mock Demo Data
            List<TrustApplication> completedApplicationsTestData = 
            new List<TrustApplication>()
            {
                new TrustApplication() { Id = 1, Application = "Join a multi-academy trust A2B_2549", TrustName = "Harpenden Academy trust", SchoolOrSchoolsApplyingToConvert = li.ToString() }
            };

            Assert.AreEqual(completedApplicationsTestData.Count, trustApplication.GetCompletedApplications("Username").Count, "Count is not correct");
            Assert.AreEqual(completedApplicationsTestData.ToArray()[0].Application, trustApplication.GetPendingApplications("Username").ToArray()[0].Application, "Completed data not found");
            Assert.AreEqual(completedApplicationsTestData.ToArray()[0].TrustName, trustApplication.GetPendingApplications("Username").ToArray()[0].TrustName, "Completed data not found");
            Assert.AreEqual(completedApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert, trustApplication.GetPendingApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert, "Completed data not found");

        }
    }
}
