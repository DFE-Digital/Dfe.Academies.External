import { url, login_username, login_password } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import Footer from '../page-objects/components/Footer'
import A2BJAMTrustDetailsSummary from '../page-objects/pages/A2BJAMTrustDetailsSummary'
import A2BJAMTrustConsent from '../page-objects/pages/A2BJAMTrustConsent'
import A2BChangesToTheTrust from '../page-objects/pages/A2BChangesToTheTrust'
import A2BLocalGovernanceArrangements from '../page-objects/pages/A2BLocalGovernanceArrangements'


describe('Third Part Create New JAM App - Complete Trust Details', () => {

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

      it('Navigate to JAM App With Trust Name and School Name Added And Complete Trust details Section', () => {
        A2BLogin.login(login_username, login_password)

        cy.wait(5000) // Adding wait as Cypress is too quick for the app here sometimes

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
    
        A2BYourApplications.selectJAMNotStartedApplicationButSchoolAdded()
        
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

        // PROCEED TO TRUST DETAILS SUMMARY PAGE!!!!
        // -----------------------------------------
        // PROCEED TO TRUST DETAILS SECTION
        A2BYourApplication.selectTrustDetails()

       // CHECK TRUST DETAILS SUMMARY PAGE
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BJAMTrustDetailsSummary.JAMTrustDetailsSummaryElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()

       // CLICK ON START SECTION
       A2BJAMTrustDetailsSummary.JAMTrustDetailsSummarySelectStartSection()

       // CHECK ELEMENTS VISIBLE ON JAM TRUST CONSENT PAGE
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BJAMTrustConsent.JAMTrustConsentElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()

       // ATTEMPT TO UPLOAD A FILE
       A2BJAMTrustConsent.JAMTrustConsentFileUpload()

       // ATTEMPT TO SUBMIT TRUST CONSENT FORM
       A2BJAMTrustConsent.JAMTrustConsentSubmit()

       // VERIFY THE CHANGES TO THE TRUST PAGE DISPLAYS CORRECTLY
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BChangesToTheTrust.changesToTheTrustElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()


       // CLICK YES TO CHANGES TO THE TRUST, ENTER REASONS WHY, AND CLICK ON THE SUBMIT BUTTON
       A2BChangesToTheTrust.changesToTheTrustClickYes()

       A2BChangesToTheTrust.enterChangesToTheTrust()

       A2BChangesToTheTrust.changesToTheTrustSubmit()

       // CHECK ELEMENTS VISIBLE ON LOCAL GOVERNANCE ARRANGEMENTS PAGE
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BLocalGovernanceArrangements.localGovernanceArrangementsElementsVisible()


       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()


       // CLICK YES
       A2BLocalGovernanceArrangements.localGovernanceArrangementsClickYes()

       // ENTER REASONS
       A2BLocalGovernanceArrangements.enterlocalGovernanceArrangementsChanges()

       // SUBMIT LOCAL GOVERNACE ARRANGEMENTS FORM
       A2BLocalGovernanceArrangements.localGovernanceArrangementsSubmit()
    })
      
})