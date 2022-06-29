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

            ConversionApplication actualTrustApplication = new ConversionApplication();
            string UserEmail = ""; // TODO: filter by useremail


             // Mock data
            List<ConversionApplication> expectedExistingApplicationsTestData =
            new List<ConversionApplication>()
            {
                    new ConversionApplication() { Id = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "Cambridge Regional college" } } },
                    new ConversionApplication() { Id = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() { Id = 3, SchoolOrSchoolsApplyingToConvertProperty = "Fen Ditton primary school" }, new SchoolOrSchoolsApplyingToConvert() {Id  = 3, SchoolOrSchoolsApplyingToConvertProperty = "Chesterton primary school" }, new SchoolOrSchoolsApplyingToConvert() {Id  = 3, SchoolOrSchoolsApplyingToConvertProperty = "North Cambridge academy"} } },
                    new ConversionApplication() { Id = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "King’s College London Maths school" } } }
            };

            Assert.AreEqual(expectedExistingApplicationsTestData.Count, actualTrustApplication.GetPendingApplications("Username").Count, "Count is not correct");
            Assert.AreEqual(expectedExistingApplicationsTestData.ToArray()[0].Application, actualTrustApplication.GetPendingApplications("Username").ToArray()[0].Application, "Pending data not found");
            Assert.AreEqual(expectedExistingApplicationsTestData.ToArray()[0].TrustName, actualTrustApplication.GetPendingApplications("Username").ToArray()[0].TrustName, "Pending data not found");
            Assert.AreEqual(expectedExistingApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert?.ToArray()[0].SchoolOrSchoolsApplyingToConvertProperty, actualTrustApplication.GetPendingApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert?.ToArray()[0].SchoolOrSchoolsApplyingToConvertProperty, "Pending data not found");

        }

        [Test]
        public void GetCompletedApplications()
        {

            ConversionApplication actualTrustApplication = new ConversionApplication();

            // Mock Demo Data
            List<ConversionApplication> expectedCompletedApplicationsTestData =
            new List<ConversionApplication>()
            {
                new ConversionApplication() { Id = 1, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "Harpenden Academy trust", SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(){ new SchoolOrSchoolsApplyingToConvert() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "St George’s school" } } }
            };

            Assert.AreEqual(expectedCompletedApplicationsTestData.Count, actualTrustApplication.GetCompletedApplications("Username").Count, "Count is not correct");
            Assert.AreEqual(expectedCompletedApplicationsTestData.ToArray()[0].Application, actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].Application, "Completed data not found");
            Assert.AreEqual(expectedCompletedApplicationsTestData.ToArray()[0].TrustName, actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].TrustName, "Completed data not found");

            if (expectedCompletedApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert?.ToArray()[0].SchoolOrSchoolsApplyingToConvertProperty != null && actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert?.ToArray()[0].SchoolOrSchoolsApplyingToConvertProperty != null)
            {
                Assert.AreEqual(expectedCompletedApplicationsTestData.ToArray()[0].SchoolOrSchoolsApplyingToConvert?.ToArray()[0].SchoolOrSchoolsApplyingToConvertProperty, actualTrustApplication.GetCompletedApplications("Username").ToArray()[0].SchoolOrSchoolsApplyingToConvert?.ToArray()[0].SchoolOrSchoolsApplyingToConvertProperty, "Completed data not found");

            } }
    }
}
