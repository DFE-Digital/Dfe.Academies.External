import { url, login_username, login_password } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import Footer from '../page-objects/components/Footer'
import A2BWhichTrustIsSchoolJoining from '../page-objects/pages/A2BWhichTrustIsSchoolJoining'
import A2BWhatIsTheNameOfTheSchool from '../page-objects/pages/A2BWhatIsTheNameOfTheSchool'

describe('Second Half Create New JAM App', () => {

    beforeEach(function() {
        cy.visit(url)

        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BHome.h1ApplyToBecomeAnAcademyVisible()
        A2BHome.p3Visible()
        A2BHome.p4Visible()
        A2BHome.checkSpecialSchoolsLinkVisible()
        A2BHome.checkPupilReferralUnitsLinkVisible()
        A2BHome.p6Visible()
        A2BHome.h2Visible()
        A2BHome.p7Visible()
        A2BHome.completeAnEqualityImpactAssessmentVisible()
        A2BHome.consultationWithStakeholdersLinkVisible()
        A2BHome.p8Visible()
        A2BHome.contactYourRegionalDirectorLinkVisible()
        A2BHome.allInformationAndEvidenceYouWillNeedLinkVisible()
        
        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()
        
        CookieHeaderModal.clickAcceptAnalyticsCookies()
        A2BHome.StartNowVisible()
        A2BHome.clickStartNow()
      })

    it('should be able to add a school and a trust', () => {
        A2BLogin.login(login_username, login_password)

        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BYourApplications.yourApplicationsElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        A2BYourApplications.selectTempSecondHalfCreateNewJAMApplication()

        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BYourApplication.yourApplicationNotStartedElementsVisible()
   
        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // Click Add a Trust
        A2BYourApplication.selectAddATrust()

        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BWhichTrustIsSchoolJoining.whichTrustIsSchoolJoiningElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()
        
        cy.wait(2000)
        A2BWhichTrustIsSchoolJoining.selectTrustName()
        cy.wait(1000)

      // CLICK ADD A SCHOOL
      A2BYourApplication.selectAddASchool()

      // CHECK THE ELEMENTS OF THE ADD A SCHOOL PAGE DISPLAY CORRECTLY
      Header.govUkHeaderVisible()
      Header.applyToBecomeAnAcademyHeaderLinkVisible()

      A2BWhatIsTheNameOfTheSchool.whatIsTheNameOfTheSchoolElementsVisible()

      Footer.accessibilityStatementLinkVisible()
      Footer.cookiesLinkVisible()
      Footer.termsAndConditionsLinkVisible()
      Footer.privacyLinkVisible()
      Footer.oglLogoVisible()
      Footer.allContentTextVisible()
      Footer.openGovernmentLicence3LinkVisible()
      Footer.crownCopyrightLinkVisible()
       
      cy.wait(2000)
      A2BWhatIsTheNameOfTheSchool.selectSchoolName()
      cy.wait(1000)
       


       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BYourApplication.yourApplicationNotStartedButSchoolAddedElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()

       // PROCEED TO TRUST DETAILS SECTION
       A2BYourApplication.selectTrustDetails()

       // CHECK TRUST DETAILS SUMMARY PAGE
       

    })
})