import { url, login_username, login_password } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import A2BWhatAreYouApplyingToDo from '../page-objects/pages/A2BWhatAreYouApplyingToDo'
import A2BWhatIsYourRole from '../page-objects/pages/A2BWhatIsYourRole'
import A2BWhatIsTheNameOfTheSchool from '../page-objects/pages/A2BWhatIsTheNameOfTheSchool'
import A2BWhichTrustIsSchoolJoining from '../page-objects/pages/A2BWhichTrustIsSchoolJoining'
import A2BJAMTrustDetailsSummary from '../page-objects/pages/A2BJAMTrustDetailsSummary'
import A2BJAMTrustConsent from '../page-objects/pages/A2BJAMTrustConsent'
import A2BChangesToTheTrust from '../page-objects/pages/A2BChangesToTheTrust'
import A2BLocalGovernanceArrangements from '../page-objects/pages/A2BLocalGovernanceArrangements'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BAboutTheConversion from '../page-objects/pages/A2BAboutTheConversion'
import A2BMainContacts from '../page-objects/pages/A2BMainContacts'
import A2BConversionTargetDate from '../page-objects/pages/A2BConversionTargetDate'
import A2BReasonsForJoining from '../page-objects/pages/A2BReasonsForJoining'
import A2BChangingTheNameOfTheSchool from '../page-objects/pages/A2BChangingTheNameOfTheSchool'

import A2BAdditionalDetailsSummaryPage from '../page-objects/pages/A2BAdditionalDetailsSummaryPage'
import A2BAdditionalDetailsDetails from '../page-objects/pages/A2BAdditionalDetailsDetails'

import A2BFinanceSummary from '../page-objects/pages/A2BFinanceSummary'
import A2BPreviousFinancialYear from '../page-objects/pages/A2BPreviousFinancialYear'
import A2BCurrentFinancialYear from '../page-objects/pages/A2BCurrentFinancialYear'
import A2BNextFinancialYear from '../page-objects/pages/A2BNextFinancialYear'

import A2BLoansSummary from '../page-objects/pages/A2BLoansSummary'
import A2BLeasesSummary from '../page-objects/pages/A2BLeasesSummary'
import A2BFinancialInvestigations from '../page-objects/pages/A2BFinancialInvestigations'

import A2BFuturePupilNumbersSummary from '../page-objects/pages/A2BFuturePupilNumbersSummary'
import A2BFuturePupilNumbersDetails from '../page-objects/pages/A2BFuturePupilNumbersDetails'

import A2BLandAndBuildingsSummary from '../page-objects/pages/A2BLandAndBuildingsSummary'
import A2BLandAndBuildingsDetails from '../page-objects/pages/A2BLandAndBuildingsDetails'

import A2BConsultationSummary from '../page-objects/pages/A2BConsultationSummary'
import A2BConsultationDetails from '../page-objects/pages/A2BConsultationDetails'

import A2BPreOpeningSupportGrantSummary from '../page-objects/pages/A2BPre-openingSupportGrantSummary'
import A2BPreopeningSupportGrantDetails from '../page-objects/pages/A2BPre-openingSupportGrantDetails'

import A2BDeclarationSummary from '../page-objects/pages/A2BDeclarationSummary'
import A2BDeclaration from '../page-objects/pages/A2BDeclaration'

import A2BSuccessfulApplicationSubmitted from '../page-objects/pages/A2BSuccessfulApplicationSubmitted'
import Footer from '../page-objects/components/Footer'

describe('View Application Tests', () => {

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

    it('should be able to create a New Application', () => {
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

        A2BYourApplications.selectStartANewApplication()

        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BWhatAreYouApplyingToDo.whatAreYouApplyingToDoElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        A2BWhatAreYouApplyingToDo.selectJAMRadioButton()
        A2BWhatAreYouApplyingToDo.verifyJAMRadioButtonChecked()

        A2BWhatAreYouApplyingToDo.selectApplyingToDoSaveAndContinue()

        Header.govUkHeaderVisible()
        Header.applyToBecomeAnAcademyHeaderLinkVisible()

        A2BWhatIsYourRole.whatIsYourRoleElementsVisible()

        Footer.accessibilityStatementLinkVisible()
        Footer.cookiesLinkVisible()
        Footer.termsAndConditionsLinkVisible()
        Footer.privacyLinkVisible()
        Footer.oglLogoVisible()
        Footer.allContentTextVisible()
        Footer.openGovernmentLicence3LinkVisible()
        Footer.crownCopyrightLinkVisible()

        A2BWhatIsYourRole.selectChairOfGovernorsRadioButton()
        A2BWhatIsYourRole.verifyChairOfGovernorsRadioButtonChecked()

        A2BWhatIsYourRole.selectWhatIsYourRoleSaveAndContinue()

        // VERIFY JAM YOUR APPLICATION OVERVIEW PAGE DISPLAYS CORRECTLY
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

        //Click Add a Trust
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

      // OK SO TRUST AND SCHOOL HAVE BEEN ADDED LETS CHECK THE JAM APPLICATION OVERVIEW PAGE

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

      // SUBMIT TRUST DETAILS SUMMARY SECTION
      A2BJAMTrustDetailsSummary.JAMTrustDetailsSummarySaveAndReturnToApp()

      // CHECK YOUR APPLICATION PAGE DISPLAYS CORRECTLY AND TRUST SECTION IS MARKED AS COMPLETE
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

// ADDING BEGINNING OF FILLING OUT JAM SCHOOL OVERVIEW SECTION
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