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

            TrustApplication actualTrustApplication = new TrustApplication();
            string UserEmail = ""; // TODO: filter by useremail

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
            List<TrustApplication> expectedExistingApplicationsTestData =
            new List<TrustApplication>()
            {
                new TrustApplication() { Id = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust", SchoolOrSchoolsApplyingToConvert = li.ToString()},
                new TrustApplication() { Id = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust", SchoolOrSchoolsApplyingToConvert = li2.ToString()},
                new TrustApplication() { Id = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example", SchoolOrSchoolsApplyingToConvert = li3.ToString()},
            };

            Assert.AreEqual(expectedExistingApplicationsTestData.Count, actualTrustApplication.GetPendingApplications("Username").Count, "Count is not correct");
            Assert.AreEqual(expectedExistingApplicationsTestData.ToArray()[0].Application, actualTrustApplication.GetPendingApplications("Username").ToArray()[0].Application, "Pending data not found");
            Assert.AreEqual(expectedExistingApplicationsTestData.ToArray()[0].TrustName, actualTrustApplication.GetPendingApplications("Username").ToArray()[0].TrustName, "Pending data not found");
            Assert.AreEqual(expectedExistingApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert, actualTrustApplication.GetPendingApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert, "Pending data not found");

        }

        [Test]
        public void GetCompletedApplications()
        {

            TrustApplication actualTrustApplication = new TrustApplication();

            // Mock data
            StringBuilder li = new StringBuilder();
            li.Append(@"<ul>");
            li.Append(@"<li>St George’s school</li>");
            li.Append(@"</ul>");

            // Mock Demo Data
            List<TrustApplication> expectedCompletedApplicationsTestData = 
            new List<TrustApplication>()
            {
                new TrustApplication() { Id = 1, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "Harpenden Academy trust", SchoolOrSchoolsApplyingToConvert = li.ToString() }
            };

            Assert.AreEqual(expectedCompletedApplicationsTestData.Count, actualTrustApplication.GetCompletedApplications("Username").Count, "Count is not correct");
            Assert.AreEqual(expectedCompletedApplicationsTestData.ToArray()[0].Application, actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].Application, "Completed data not found");
            Assert.AreEqual(expectedCompletedApplicationsTestData.ToArray()[0].TrustName, actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].TrustName, "Completed data not found");
            Assert.AreEqual(expectedCompletedApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert, actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert, "Completed data not found");

        }
    }
}
