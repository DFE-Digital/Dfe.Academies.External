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
// SELECT FINANCES SECTION *******

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

      
      // SELECT PREVIOUS FINANCIAL YR START SECTION
      A2BFinanceSummary.selectPreviousFinancialYrStartSection()

      // CHECK PREVIOUS FINANCIAL YR PAGE DISPLAYS CORRECTLY
      
      Header.govUkHeaderVisible()
      Header.applyToBecomeAnAcademyHeaderLinkVisible()

      A2BPreviousFinancialYear.previousFinancialYrElementsVisible()

      Footer.accessibilityStatementLinkVisible()
      Footer.cookiesLinkVisible()
      Footer.termsAndConditionsLinkVisible()
      Footer.privacyLinkVisible()
      Footer.oglLogoVisible()
      Footer.allContentTextVisible()
      Footer.openGovernmentLicence3LinkVisible()
      Footer.crownCopyrightLinkVisible()

     // FILL OUT PREVIOUS FINANCIAL YR DETAILS
     A2BPreviousFinancialYear.inputPreviousFinancialYrDate()

     A2BPreviousFinancialYear.inputPreviousFinancialYrRevenueCarryForward()
     A2BPreviousFinancialYear.selectRevenueCarryForwardSurplus()
     A2BPreviousFinancialYear.verifyRevenueCarryForwardSurplusSelected()


     A2BPreviousFinancialYear.inputPreviousFinancialYrCapitalCarryForward()
     A2BPreviousFinancialYear.selectCapitalCarryForwardSurplus()
     A2BPreviousFinancialYear.verifyCapitalCarryForwardSurplusSelected

     // SUBMIT PREVIOUS FINANCIAL YR
     A2BPreviousFinancialYear.submitPreviousFinancialYr()




      // CHECK CURRENT FINANCIAL YR SECTION CORRECTLY
      A2BCurrentFinancialYear.currentFinancialYrElementsVisible()


      // FILL OUT CURRENT FINANCIAL YR DETAILS
     A2BCurrentFinancialYear.inputCurrentFinancialYrDate()

     A2BCurrentFinancialYear.inputCurrentFinancialYrRevenueCarryForward()
     A2BCurrentFinancialYear.selectRevenueCarryForwardDeficit()
     A2BCurrentFinancialYear.verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays()

     A2BCurrentFinancialYear.inputReasonsForCurrentRevenueCarryForwardDeficit()
     A2BCurrentFinancialYear.uploadFileForCurrentRevenueCarryForwardDeficit()


     A2BCurrentFinancialYear.inputCurrentFinancialYrCapitalCarryForward()
     A2BCurrentFinancialYear.selectCurrentCapitalCarryForwardDeficit()
     A2BCurrentFinancialYear.verifyCurrentCapitalCarryForwardDeficitSelectedSectionDisplays()

     A2BCurrentFinancialYear.inputReasonsForCurrentCapitalCarryForwardDeficit()
     A2BCurrentFinancialYear.uploadFileForCurrentCapitalCarryForwardDeficit()

     // SUBMIT CURRENT FINANCIAL YR
     A2BCurrentFinancialYear.submitCurrentFinancialYr()



     // CHECK NEXT FINANCIAL YR SECTION CORRECTLY
     A2BNextFinancialYear.nextFinancialYrElementsVisible()


     // FILL OUT NEXT FINANCIAL YR DETAILS
     A2BNextFinancialYear.inputNextFinancialYrDate()

     A2BNextFinancialYear.inputNextFinancialYrRevenueCarryForward()
     A2BNextFinancialYear.selectRevenueCarryForwardDeficit()
     A2BNextFinancialYear.verifyNextRevenueCarryForwardDeficitSelectedSectionDisplays()

     A2BNextFinancialYear.inputReasonsForNextRevenueCarryForwardDeficit()
     A2BNextFinancialYear.uploadFileForNextRevenueCarryForwardDeficit()


     A2BNextFinancialYear.inputNextFinancialYrCapitalCarryForward()
     A2BNextFinancialYear.selectNextCapitalCarryForwardDeficit()
     A2BNextFinancialYear.verifyNextCapitalCarryForwardDeficitSelectedSectionDisplays()

     A2BNextFinancialYear.inputReasonsForNextCapitalCarryForwardDeficit()
     A2BNextFinancialYear.uploadFileForNextCapitalCarryForwardDeficit()

     // SUBMIT NEXT FINANCIAL YR
     A2BNextFinancialYear.submitNextFinancialYr()





      // CHECK LOANS SUMMARY PAGE DISPLAYS CORRECTLY
      Header.govUkHeaderVisible()
      Header.applyToBecomeAnAcademyHeaderLinkVisible()

      A2BLoansSummary.loansSummaryElementsVisible()

      Footer.accessibilityStatementLinkVisible()
      Footer.cookiesLinkVisible()
      Footer.termsAndConditionsLinkVisible()
      Footer.privacyLinkVisible()
      Footer.oglLogoVisible()
      Footer.allContentTextVisible()
      Footer.openGovernmentLicence3LinkVisible()
      Footer.crownCopyrightLinkVisible()
     
      //SUBMIT LOANS
     A2BLoansSummary.submitLoansSummary()

       // CHECK LEASES SUMMARY PAGE DISPLAYS CORRECTLY
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BLeasesSummary.leasesSummaryElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()
      

 
       // SUBMIT LEASES
       A2BLeasesSummary.submitLeasesSummary()

       // CHECK FINANCIAL INVESTIGATIONS PAGE DISPLAYS CORRECTLY
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BFinancialInvestigations.financialInvestigationsElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()

       // SUBMIT FINANCIAL INVESTIGATIONS
       A2BFinancialInvestigations.submitFinancialInvestigations()

       // CHECK COMPLETED FINANCE SUMMARY PAGE
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BFinanceSummary.financeSummaryCompleteElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()

       // SUBMIT FINANCE SUMMARY PAGE
       A2BFinanceSummary.submitFinanceSummary()

       // CHECK FINANCE MARKED COMPLETE ON JAM APP OVERVIEW PAGE
       Header.govUkHeaderVisible()
       Header.applyToBecomeAnAcademyHeaderLinkVisible()

       A2BYourApplication.financeCompleteElementsVisible()

       Footer.accessibilityStatementLinkVisible()
       Footer.cookiesLinkVisible()
       Footer.termsAndConditionsLinkVisible()
       Footer.privacyLinkVisible()
       Footer.oglLogoVisible()
       Footer.allContentTextVisible()
       Footer.openGovernmentLicence3LinkVisible()
       Footer.crownCopyrightLinkVisible()
// SELECT FUTURE PUPILS SECTION
    A2BYourApplication.selectFuturePupilNumbers()

    // ADDITIONAL DETAILS SUMMARY PAGE CHECK ELEMENTS
    Header.govUkHeaderVisible()
    Header.applyToBecomeAnAcademyHeaderLinkVisible()

    A2BFuturePupilNumbersSummary.futurePupilNumbersSummaryElementsVisible()

    Footer.accessibilityStatementLinkVisible()
    Footer.cookiesLinkVisible()
    Footer.termsAndConditionsLinkVisible()
    Footer.privacyLinkVisible()
    Footer.oglLogoVisible()
    Footer.allContentTextVisible()
    Footer.openGovernmentLicence3LinkVisible()
    Footer.crownCopyrightLinkVisible()

    // CLICK START SECTION
    A2BFuturePupilNumbersSummary.selectFuturePupilNumbersStartSection()

    Header.govUkHeaderVisible()
    Header.applyToBecomeAnAcademyHeaderLinkVisible()

    A2BFuturePupilNumbersDetails.futurePupilNumbersDetailsElementsVisible()

    Footer.accessibilityStatementLinkVisible()
    Footer.cookiesLinkVisible()
    Footer.termsAndConditionsLinkVisible()
    Footer.privacyLinkVisible()
    Footer.oglLogoVisible()
    Footer.allContentTextVisible()
    Footer.openGovernmentLicence3LinkVisible()
    Footer.crownCopyrightLinkVisible()

    // FILL OUT FUTURE PUPIL NUMBERS DETAILS
    A2BFuturePupilNumbersDetails.fillFuturePupilNumbersDetails()

    // SUBMIT FUTURE PUPIL NUMBERS DETAILS
    A2BFuturePupilNumbersDetails.submitFuturePupilNumbersDetails()

    // CHECK COMPLETED FUTURE PUPIL NUMBERS SUMMARY DISPLAYS CORRECTLY
    Header.govUkHeaderVisible()
    Header.applyToBecomeAnAcademyHeaderLinkVisible()

    A2BFuturePupilNumbersSummary.futurePupilNumbersSummaryCompleteElementsVisible()

    Footer.accessibilityStatementLinkVisible()
    Footer.cookiesLinkVisible()
    Footer.termsAndConditionsLinkVisible()
    Footer.privacyLinkVisible()
    Footer.oglLogoVisible()
    Footer.allContentTextVisible()
    Footer.openGovernmentLicence3LinkVisible()
    Footer.crownCopyrightLinkVisible()

    // SUBMIT FUTURE PUPIL NUMBERS SUMMARY
    A2BFuturePupilNumbersSummary.submitFuturePupilNumbersSummary()

    // VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
    A2BYourApplication.futurePupilNumbersCompleteElementsVisible()
// SELECT LAND AND BUILDINGS SECTION
A2BYourApplication.selectLandAndBuildings()

// ADDITIONAL DETAILS SUMMARY PAGE CHECK ELEMENTS
Header.govUkHeaderVisible()
Header.applyToBecomeAnAcademyHeaderLinkVisible()

A2BLandAndBuildingsSummary.landAndBuildingsSummaryElementsVisible()

Footer.accessibilityStatementLinkVisible()
Footer.cookiesLinkVisible()
Footer.termsAndConditionsLinkVisible()
Footer.privacyLinkVisible()
Footer.oglLogoVisible()
Footer.allContentTextVisible()
Footer.openGovernmentLicence3LinkVisible()
Footer.crownCopyrightLinkVisible()

// CLICK START SECTION
A2BLandAndBuildingsSummary.selectLandAndBuildingsStartSection()

// CHECK LAND AND BUILDINGS DETAILS DISPLAY CORRECTLY

Header.govUkHeaderVisible()
Header.applyToBecomeAnAcademyHeaderLinkVisible()

A2BLandAndBuildingsDetails.landAndBuildingsDetailsElementsVisible()

Footer.accessibilityStatementLinkVisible()
Footer.cookiesLinkVisible()
Footer.termsAndConditionsLinkVisible()
Footer.privacyLinkVisible()
Footer.oglLogoVisible()
Footer.allContentTextVisible()
Footer.openGovernmentLicence3LinkVisible()
Footer.crownCopyrightLinkVisible()


// FILL OUT LAND AND BUILDINGS DETAILS
A2BLandAndBuildingsDetails.fillLandAndBuildingsDetails()

// SUBMIT LAND AND BUILDINGS DETAILS
A2BLandAndBuildingsDetails.submitLandAndBuildingsDetails()

// CHECK COMPLETED FUTURE PUPIL NUMBERS SUMMARY DISPLAYS CORRECTLY
Header.govUkHeaderVisible()
Header.applyToBecomeAnAcademyHeaderLinkVisible()

A2BLandAndBuildingsSummary.landAndBuildingsSummaryCompleteElementsVisible()

Footer.accessibilityStatementLinkVisible()
Footer.cookiesLinkVisible()
Footer.termsAndConditionsLinkVisible()
Footer.privacyLinkVisible()
Footer.oglLogoVisible()
Footer.allContentTextVisible()
Footer.openGovernmentLicence3LinkVisible()
Footer.crownCopyrightLinkVisible()

// SUBMIT LAND AND BUILDINGS SUMMARY
A2BLandAndBuildingsSummary.submitLandAndBuildingsSummary()

// VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
A2BYourApplication.landAndBuildingsCompleteElementsVisible()
// SELECT CONSULTATION SECTION
A2BYourApplication.selectConsultation()

// ADDITIONAL DETAILS SUMMARY PAGE CHECK ELEMENTS
Header.govUkHeaderVisible()
Header.applyToBecomeAnAcademyHeaderLinkVisible()

A2BConsultationSummary.consultationSummaryElementsVisible()

Footer.accessibilityStatementLinkVisible()
Footer.cookiesLinkVisible()
Footer.termsAndConditionsLinkVisible()
Footer.privacyLinkVisible()
Footer.oglLogoVisible()
Footer.allContentTextVisible()
Footer.openGovernmentLicence3LinkVisible()
Footer.crownCopyrightLinkVisible()

// CLICK START SECTION
A2BConsultationSummary.selectConsultationStartSection()

Header.govUkHeaderVisible()
Header.applyToBecomeAnAcademyHeaderLinkVisible()

A2BConsultationDetails.consultationDetailsElementsVisible()

Footer.accessibilityStatementLinkVisible()
Footer.cookiesLinkVisible()
Footer.termsAndConditionsLinkVisible()
Footer.privacyLinkVisible()
Footer.oglLogoVisible()
Footer.allContentTextVisible()
Footer.openGovernmentLicence3LinkVisible()
Footer.crownCopyrightLinkVisible()

// FILL OUT FUTURE PUPIL NUMBERS DETAILS
A2BConsultationDetails.fillConsultationDetails()

// SUBMIT CONSULTATION DETAILS
A2BConsultationDetails.submitConsultationDetails()

// CHECK COMPLETED CONSULTATION SUMMARY DISPLAYS CORRECTLY
Header.govUkHeaderVisible()
Header.applyToBecomeAnAcademyHeaderLinkVisible()

A2BConsultationSummary.consultationSummaryCompleteElementsVisible()

Footer.accessibilityStatementLinkVisible()
Footer.cookiesLinkVisible()
Footer.termsAndConditionsLinkVisible()
Footer.privacyLinkVisible()
Footer.oglLogoVisible()
Footer.allContentTextVisible()
Footer.openGovernmentLicence3LinkVisible()
Footer.crownCopyrightLinkVisible()

// SUBMIT CONSULTATION SUMMARY
A2BConsultationSummary.submitConsultationSummary()

// VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
A2BYourApplication.consultationCompleteElementsVisible()
    })
})