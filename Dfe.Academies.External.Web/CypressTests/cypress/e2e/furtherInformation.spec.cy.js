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

describe('Complete School Overview Section', () => {

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

      it('should complete further information section', () => {
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

        // SELECT APPLICATION FROM LIST
        A2BYourApplications.selectTempSecondHalfCreateNewJAMApplication()

        // SELECT FURTHER INFORMATION SECTION
        A2BYourApplication.selectFurtherInformation()

        // ADDITIONAL DETAILS SUMMARY PAGE CHECK ELEMENTS
        A2BAdditionalDetailsSummaryPage.additionalDetailsSummaryNotStartedElementsVisible()

        // SELECT START SECTION ON ADDITIONAL DETAILS SUMMARY PAGE
        A2BAdditionalDetailsSummaryPage.selectAdditionalDetailsStartSection()

        // CHECK ELEMENTS ON ADDITIONAL DETAILS DETAILS PAGE
        A2BAdditionalDetailsDetails.additionalDetailsDetailsNotStartedElementsVisible()

        // FILL SCHOOL CONTRIBUTION
        A2BAdditionalDetailsDetails.fillSchoolContribution()

        //DIOCESE STUFF
        A2BAdditionalDetailsDetails.selectYesIsSchoolLinkedToDiocese()

        // CHECK DIOCESE SECTION ELEMENTS DISPLAY CORRECTLY
        A2BAdditionalDetailsDetails.dioceseSectionElementsVisible()

        // INPUT DIOCESE NAME
        A2BAdditionalDetailsDetails.inputDioceseName()

        // UPLOAD DIOCESE LETTER
        A2BAdditionalDetailsDetails.dioceseFileUpload()

        // SELECT YES ON IS SCHOOL SUPPORTED BY FOUNDATION
        A2BAdditionalDetailsDetails.selectYesSchoolSupportedByTrustOrFoundation()

        // CHECK SCHOOL SUPPORTED BY TRUST OR FOUNDATION DISPLAYS CORRECTLY
        A2BAdditionalDetailsDetails.schoolSupportedByElementsVisible()

        // INPUT NAME OF BODY
        A2BAdditionalDetailsDetails.inputBodyName()

        // UPLOAD FOUNDATION TRUST OR BODY CONSENT
        A2BAdditionalDetailsDetails.uploadSchoolSupportedByTrustOrBody()

        // INPUT LIST OF FEEDER SCHOOLS
        A2BAdditionalDetailsDetails.inputListOfFeederSchools()

        // UPLOAD SCHOOL LETTER OF CONSENT
        A2BAdditionalDetailsDetails.uploadSchoolLetterOfConsent()

        // SUBMIT ADDITIONAL DETAILS DETAILS PAGE
        A2BAdditionalDetailsDetails.submitAdditionalDetailsDetails()

        // CHECK COMPLETED ADDITIONAL DETAILS SUMMARY PAGE DISPLAYS CORRECTLY
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BAdditionalDetailsSummaryPage.additionalDetailsSummaryCompleteElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // SUBMIT ADDITIONAL DETAIL SUMMARY PAGE
        A2BAdditionalDetailsSummaryPage.submitAdditionalDetailsSummary()

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


    })


})
