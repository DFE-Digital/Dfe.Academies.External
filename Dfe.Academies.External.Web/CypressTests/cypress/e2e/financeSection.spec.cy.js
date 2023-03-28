import { url, login_username, login_password } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import A2BAboutTheConversion from '../page-objects/pages/A2BAboutTheConversion'
import Footer from '../page-objects/components/Footer'
import A2BMainContacts from '../page-objects/pages/A2BMainContacts'
import A2BConversionTargetDate from '../page-objects/pages/A2BConversionTargetDate'
import A2BReasonsForJoining from '../page-objects/pages/A2BReasonsForJoining'
import A2BChangingTheNameOfTheSchool from '../page-objects/pages/A2BChangingTheNameOfTheSchool'
import A2BAdditionalDetailsSummaryPage from '../page-objects/pages/A2BAdditionalDetailsSummaryPage'
import A2BAdditionalDetailsDetails from '../page-objects/pages/A2BAdditionalDetailsDetails'
import A2BFinanceSummary from '../page-objects/pages/A2BFinanceSummary'

describe('Complete School Overview Section - Finance Section', () => {

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

      it('FINANCE SECTION', () => {

        A2BLogin.login(login_username, login_password)

        // CHECK YOUR APPLICATIONS PAGE DISPLAYS CORRECTLY
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

    
        // SELECT APPLICATION FROM LIST
        A2BYourApplications.selectTempSecondHalfCreateNewJAMApplication()


         // CHECK JAM APPLICATION OVERVIEW PAGE DISPLAYS CORRECTLY WITH FURTHER INFORMATION SECTION MARKED AS COMPLETE
         Header.govUkHeaderVisible()
         Header.applyToBecomeAnAcademyHeaderLinkVisible()
 
         A2BYourApplication.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()
 
         Footer.accessibilityStatementLinkVisible()
         Footer.cookiesLinkVisible()
         Footer.termsAndConditionsLinkVisible()
         Footer.privacyLinkVisible()
         Footer.oglLogoVisible()
         Footer.allContentTextVisible()
         Footer.openGovernmentLicence3LinkVisible()
         Footer.crownCopyrightLinkVisible()

         // SELECT FINANCES LINK
         A2BYourApplication.selectFinances()

         // CHECK FINANCE SUMMARY PAGE DISPLAYS CORRECTLY
         Header.govUkHeaderVisible()
         Header.applyToBecomeAnAcademyHeaderLinkVisible()

         A2BFinanceSummary.financeSummaryNotStartedElementsVisible()

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