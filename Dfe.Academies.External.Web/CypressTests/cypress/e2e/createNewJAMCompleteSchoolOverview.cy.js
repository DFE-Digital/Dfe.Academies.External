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

    it('should be able to complete JAM School Overview Section', () => {
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

        A2BYourApplication.yourApplicationNotStartedButTrustSectionCompleteElementsVisible()
   
        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        //  OK - Let's Start By Clicking On About the Conversion Section
        A2BYourApplication.selectAboutTheConversion()

        // OK we're Now on About the Conversion Page - Let's check all elements display correctly
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BAboutTheConversion.aboutTheConversionNotStartedElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // OK now we want to click on Start section for main contacts
        A2BAboutTheConversion.selectContactDetailsStartSection()

        // OK so now we need to check ContactDetails / Main Contacts page displays correctly
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BMainContacts.mainContactsNotStartedElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()


        // OK - LET'S POPULATE THE MAIN CONTACTS FORM
        A2BMainContacts.fillHeadTeacherDetails()
        A2BMainContacts.fillChairDetails()
        A2BMainContacts.selectMainContactAsChair()
        A2BMainContacts.fillApproverDetails()

        // SUBMIT MAINCONTACTS FORM
        A2BMainContacts.submitMainContactsForm()

        // A2B ABOUT THE CONVERSION ELEMENTS VISIBLE WITH MAIN CONTACTS SECTION COMPLETE
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BAboutTheConversion.aboutTheConversionMainContactsCompleteElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // OK SO MAIN CONTACTS COMPLETE AND INFO CORRECT ON ABOUT THE CONVERSION
        // OK now we want to click on Start section for main contacts
        A2BAboutTheConversion.selectDateForConversionStartSection()

        // OK - NOW WE'RE ON THE DATE OF CONVERSION PAGE WE NEED TO CHECK PAGE ELEMENTS
        // DISPLAY CORRECTLY
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BConversionTargetDate.conversionTargetDateElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // COMPLETE DATE CONVERSION PAGE AND SUBMIT
        A2BConversionTargetDate.conversionTargetDateSubmit()


        // CHECK REASONS FOR JOINING PAGE DISPLAYS CORRECTLY
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BReasonsForJoining.reasonsForJoiningElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // COMPLETE REASONS FOR JOINING PAGE AND SUBMIT
        A2BReasonsForJoining.reasonsForJoiningInput()

        A2BReasonsForJoining.submitReasonsForJoining()

        // CHECK CHANGING THE NAME OF THE SCHOOL DISPLAYS CORRECTLY
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BChangingTheNameOfTheSchool.changingTheNameOfTheSchoolElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // COMPLETE CHANGING THE NAME OF THE SCHOOL PAGE AND SUBMIT
        A2BChangingTheNameOfTheSchool.submitChangingTheNameOfTheSchool()

        // NOW CHECK ABOUT THE CONVERSION PAGE DISPLAYS CORRECTLY WITH ALL DATA
        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BAboutTheConversion.aboutTheConversionCompleteElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        // NOW SAVE COMPLETED ABOUT THE CONVERSION SUMMARY PAGE
        A2BAboutTheConversion.submitAboutTheConversion()

        // NOW CHECK THE JAM APPLICATION OVERVIEW PAGE DISPLAYS CORRECTLY WITH
        // ABOUT THE CONVERSION SECTION MARKED AS COMPLETED
        A2BYourApplication.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible()
        
    })



})