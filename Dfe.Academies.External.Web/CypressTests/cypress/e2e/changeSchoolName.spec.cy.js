import { url, login_username, login_password } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import A2BWhatIsTheNameOfTheSchool from '../page-objects/pages/A2BWhatIsTheNameOfTheSchool'
import Footer from '../page-objects/components/Footer'

describe('Change School Name From JAM App Overview Page', () => {

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

    it('should be able to change the name of a school from JAM App Overview Page - (School Previously Added)', () => {
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
    
        // NAVIGATE TO APPLICATION
        A2BYourApplications.selectJAMNotStartedApplicationButSchoolAdded()
        
        // CHECK JAM APPLICATION OVERVIEW ELEMENTS VISIBLE
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

       
        // SELECT CHANGE SCHOOL FROM APP OVERVIEW PAGE
        A2BYourApplication.selectChangeSchool()

        // CHECK ELEMENTS ON WHATISTHENAMEOFTHESCHOOL PAGE
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

        //CHANGE THE NAME OF THE SCHOOL AND SUBMIT THE FORM
        A2BWhatIsTheNameOfTheSchool.changeSchoolName()

        //CHECK THAT THE APPLICATION OVERVIEW PAGE IS CORRECT
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



    })
})