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

        A2BHome.homePageElementsVisible()
        
        Footer.checkFooterLinksVisible()
        
        CookieHeaderModal.clickAcceptAnalyticsCookies()
        A2BHome.clickStartNow()
      })

    it('should be able to create a New Application', () => {
        A2BLogin.login(login_username, login_password)

        A2BYourApplications.selectStartANewApplication()

        A2BWhatAreYouApplyingToDo.selectJAMRadioButton()
        A2BWhatAreYouApplyingToDo.verifyJAMRadioButtonChecked()

        A2BWhatAreYouApplyingToDo.selectApplyingToDoSaveAndContinue()

        A2BWhatIsYourRole.selectChairOfGovernorsRadioButton()
        A2BWhatIsYourRole.verifyChairOfGovernorsRadioButtonChecked()

        A2BWhatIsYourRole.selectWhatIsYourRoleSaveAndContinue()

        // VERIFY JAM YOUR APPLICATION OVERVIEW PAGE DISPLAYS CORRECTLY

        A2BYourApplication.yourApplicationNotStartedElementsVisible()

        //Click Add a Trust
        A2BYourApplication.selectAddATrust()

       A2BWhichTrustIsSchoolJoining.whichTrustIsSchoolJoiningElementsVisible()

       cy.wait(2000)

       A2BWhichTrustIsSchoolJoining.selectTrustName()
       cy.wait(1000)
	A2BWhichTrustIsSchoolJoining.selectConfirmTrust()
       A2BWhichTrustIsSchoolJoining.submitTrustName()


      // CLICK ADD A SCHOOL
      A2BYourApplication.selectAddASchool()

      cy.wait(2000)

      A2BWhatIsTheNameOfTheSchool.selectSchoolName()
       cy.wait(1000)

      // OK SO TRUST AND SCHOOL HAVE BEEN ADDED LETS CHECK THE JAM APPLICATION OVERVIEW PAGE

// PROCEED TO TRUST DETAILS SUMMARY PAGE!!!!
        // -----------------------------------------
        // PROCEED TO TRUST DETAILS SECTION
        A2BYourApplication.selectTrustDetails()

       // CHECK TRUST DETAILS SUMMARY PAGE

       // CLICK ON START SECTION
       A2BJAMTrustDetailsSummary.JAMTrustDetailsSummarySelectStartSection()

       // ATTEMPT TO UPLOAD A FILE
       A2BJAMTrustConsent.JAMTrustConsentFileUpload()

       // ATTEMPT TO SUBMIT TRUST CONSENT FORM
       A2BJAMTrustConsent.JAMTrustConsentSubmit()


       // CLICK YES TO CHANGES TO THE TRUST, ENTER REASONS WHY, AND CLICK ON THE SUBMIT BUTTON
       A2BChangesToTheTrust.changesToTheTrustClickYes()

       A2BChangesToTheTrust.enterChangesToTheTrust()

       A2BChangesToTheTrust.changesToTheTrustSubmit()
       
       // CLICK YES
       A2BLocalGovernanceArrangements.localGovernanceArrangementsClickYes()

       // ENTER REASONS
       A2BLocalGovernanceArrangements.enterlocalGovernanceArrangementsChanges()

       // SUBMIT LOCAL GOVERNACE ARRANGEMENTS FORM
       A2BLocalGovernanceArrangements.localGovernanceArrangementsSubmit()

      // SUBMIT TRUST DETAILS SUMMARY SECTION
      A2BJAMTrustDetailsSummary.JAMTrustDetailsSummarySaveAndReturnToApp()

      // CHECK YOUR APPLICATION PAGE DISPLAYS CORRECTLY AND TRUST SECTION IS MARKED AS COMPLETE

      A2BYourApplication.yourApplicationNotStartedButTrustSectionCompleteElementsVisible()

      // ADDING BEGINNING OF FILLING OUT JAM SCHOOL OVERVIEW SECTION
      //  OK - Let's Start By Clicking On About the Conversion Section
      A2BYourApplication.selectAboutTheConversion()

      // OK we're Now on About the Conversion Page - Let's check all elements display correctly

      // OK now we want to click on Start section for main contacts
      A2BAboutTheConversion.selectContactDetailsStartSection()

      // OK - LET'S POPULATE THE MAIN CONTACTS FORM
      // MAKE THIS ONE FUNCTION ALONG WITH THE SUBMIT
      A2BMainContacts.fillHeadTeacherDetails()
      A2BMainContacts.fillChairDetails()
      A2BMainContacts.selectMainContactAsChair()
      A2BMainContacts.fillApproverDetails()

      // SUBMIT MAINCONTACTS FORM
      A2BMainContacts.submitMainContactsForm()
  
  // CLICK AN OPTION
      A2BConversionTargetDate.selectConversionTargetDateOptionNo()

      // COMPLETE DATE CONVERSION PAGE AND SUBMIT
      A2BConversionTargetDate.conversionTargetDateSubmit()


      // COMPLETE REASONS FOR JOINING PAGE AND SUBMIT
      A2BReasonsForJoining.reasonsForJoiningInput()
      A2BReasonsForJoining.submitReasonsForJoining()
      // SELECT OPTION NO
      A2BChangingTheNameOfTheSchool.changingTheNameOfTheSchoolSelectOptionNo()

      // COMPLETE CHANGING THE NAME OF THE SCHOOL PAGE AND SUBMIT
      A2BChangingTheNameOfTheSchool.submitChangingTheNameOfTheSchool()

      // NOW CHECK ABOUT THE CONVERSION PAGE DISPLAYS CORRECTLY WITH ALL DATA

      A2BAboutTheConversion.aboutTheConversionCompleteElementsVisible()

      // NOW SAVE COMPLETED ABOUT THE CONVERSION SUMMARY PAGE
      A2BAboutTheConversion.submitAboutTheConversion()

      // NOW CHECK THE JAM APPLICATION OVERVIEW PAGE DISPLAYS CORRECTLY WITH
      // ABOUT THE CONVERSION SECTION MARKED AS COMPLETED
      A2BYourApplication.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible()
      
      // SELECT FURTHER INFORMATION SECTION
      A2BYourApplication.selectFurtherInformation()

      // SELECT START SECTION ON ADDITIONAL DETAILS SUMMARY PAGE
      A2BAdditionalDetailsSummaryPage.selectAdditionalDetailsStartSection()

      // FILL SCHOOL CONTRIBUTION
      A2BAdditionalDetailsDetails.fillAdditionalDetailsDetailsAndSubmit()

      // CHECK COMPLETED ADDITIONAL DETAILS SUMMARY PAGE DISPLAYS CORRECTLY
      A2BAdditionalDetailsSummaryPage.additionalDetailsSummaryCompleteElementsVisible()

      // SUBMIT ADDITIONAL DETAIL SUMMARY PAGE
      A2BAdditionalDetailsSummaryPage.submitAdditionalDetailsSummary()

      // CHECK JAM APPLICATION OVERVIEW PAGE DISPLAYS CORRECTLY WITH FURTHER INFORMATION SECTION MARKED AS COMPLETE
      A2BYourApplication.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()

      // SELECT FINANCES SECTION *******

      // SELECT FINANCES LINK
      A2BYourApplication.selectFinances()

      
      // SELECT PREVIOUS FINANCIAL YR START SECTION
      A2BFinanceSummary.selectPreviousFinancialYrStartSection()

     // FILL OUT PREVIOUS FINANCIAL YR DETAILS AND SUBMIT
     A2BPreviousFinancialYear.inputPreviousFinancialYrDataAndSubmit()



    
// FILL OUT CURRENT FINANCIAL YR DETAILS AND SUBMIT
     A2BCurrentFinancialYear.inputCurrentFinancialYrDataAndSubmit()

     // FILL OUT NEXT FINANCIAL YR DETAILS
     A2BNextFinancialYear.inputNextFinancialYrDataAndSubmit()


     
// SELECT NO LOANS
      A2BLoansSummary.selectLoansOptionNo()
      //SUBMIT LOANS
     A2BLoansSummary.submitLoansSummary()
// SELECT LEASES OPTION NO
      A2BLeasesSummary.leasesSelectOptionNo()
      
       // SUBMIT LEASES
       A2BLeasesSummary.submitLeasesSummary()

       // CHECK FINANCIAL INVESTIGATIONS PAGE DISPLAYS CORRECTLY

       A2BFinancialInvestigations.financialInvestigationsElementsVisible()
       A2BFinancialInvestigations.selectFinancialInvestigationsOptionNo()

       // SUBMIT FINANCIAL INVESTIGATIONS
       A2BFinancialInvestigations.submitFinancialInvestigations()

       // CHECK COMPLETED FINANCE SUMMARY PAGE

       A2BFinanceSummary.financeSummaryCompleteElementsVisible()

       // SUBMIT FINANCE SUMMARY PAGE
       A2BFinanceSummary.submitFinanceSummary()

       // CHECK FINANCE MARKED COMPLETE ON JAM APP OVERVIEW PAGE

       A2BYourApplication.financeCompleteElementsVisible()

      // SELECT FUTURE PUPILS SECTION
      A2BYourApplication.selectFuturePupilNumbers()

    // CLICK START SECTION
    A2BFuturePupilNumbersSummary.selectFuturePupilNumbersStartSection()

    // FILL OUT FUTURE PUPIL NUMBERS DETAILS
    A2BFuturePupilNumbersDetails.fillFuturePupilNumbersDetails()

    // SUBMIT FUTURE PUPIL NUMBERS DETAILS
    A2BFuturePupilNumbersDetails.submitFuturePupilNumbersDetails()

    // CHECK COMPLETED FUTURE PUPIL NUMBERS SUMMARY DISPLAYS CORRECTLY
    A2BFuturePupilNumbersSummary.futurePupilNumbersSummaryCompleteElementsVisible()

    // SUBMIT FUTURE PUPIL NUMBERS SUMMARY
    A2BFuturePupilNumbersSummary.submitFuturePupilNumbersSummary()

    // VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
    A2BYourApplication.futurePupilNumbersCompleteElementsVisible()

   // SELECT LAND AND BUILDINGS SECTION
   A2BYourApplication.selectLandAndBuildings()



// CLICK START SECTION
A2BLandAndBuildingsSummary.selectLandAndBuildingsStartSection()


      // FILL OUT LAND AND BUILDINGS DETAILS AND SUBMIT
      A2BLandAndBuildingsDetails.fillLandAndBuildingsDetailsDataAndSubmit()



// CHECK COMPLETED LAND AND BUILDINGS SUMMARY DISPLAYS CORRECTLY

A2BLandAndBuildingsSummary.landAndBuildingsSummaryCompleteElementsVisible()

// SUBMIT LAND AND BUILDINGS SUMMARY
A2BLandAndBuildingsSummary.submitLandAndBuildingsSummary()

// VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
A2BYourApplication.landAndBuildingsCompleteElementsVisible()
// SELECT CONSULTATION SECTION
A2BYourApplication.selectConsultation()

// CLICK START SECTION
A2BConsultationSummary.selectConsultationStartSection()
// SELECT CONSULTATION DETAILS NO
A2BConsultationDetails.selectHasGovBodyConsultedStakeholdersOptionNo()

// FILL OUT CONSULTATION DETAILS
A2BConsultationDetails.fillConsultationDetails()

// SUBMIT CONSULTATION DETAILS
A2BConsultationDetails.submitConsultationDetails()

// CHECK COMPLETED CONSULTATION SUMMARY DISPLAYS CORRECTLY

A2BConsultationSummary.consultationSummaryCompleteElementsVisible()

// SUBMIT CONSULTATION SUMMARY
A2BConsultationSummary.submitConsultationSummary()

// VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
A2BYourApplication.consultationCompleteElementsVisible()
  // SELECT PRE-OPENING SUPPORT GRANT SECTION
  A2BYourApplication.selectPreopeningSupportGrant()

  // CLICK START SECTION
  A2BPreOpeningSupportGrantSummary.selectPreopeningSupportGrantStartSection()

  // FILL OUT FUTURE PUPIL NUMBERS DETAILS
  A2BPreopeningSupportGrantDetails.selectToTheSchoolPreopeningSupportGrantDetails()

  // SUBMIT CONSULTATION DETAILS
  A2BPreopeningSupportGrantDetails.submitPreopeningSupportGrantDetails()

  // CHECK COMPLETED CONSULTATION SUMMARY DISPLAYS CORRECTLY
  A2BPreOpeningSupportGrantSummary.preopeningSupportGrantSummaryCompleteElementsVisible()

  // SUBMIT CONSULTATION SUMMARY
  A2BPreOpeningSupportGrantSummary.submitPreopeningSupportGrantSummary()

  // VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
  A2BYourApplication.preopeningSupportGrantCompleteElementsVisible()

  // SELECT PRE-OPENING SUPPORT GRANT SECTION
  A2BYourApplication.selectDeclaration()

 // CLICK START SECTION
 A2BDeclarationSummary.declarationStartSection()

 A2BDeclaration.declarationElementsVisible()

 // FILL OUT DECLARATION DETAILS
 A2BDeclaration.selectAgreements()

 // CHECK AGREEMENTS SELECTED CORRECTLY
 A2BDeclaration.verifyAgreementsSelected()

 A2BDeclaration.submitDeclaration()


    // CHECK COMPLETED DECLARATION SUMMARY DISPLAYS CORRECTLY
    A2BDeclarationSummary.declarationSummaryCompleteElementsVisible()

    // SUBMIT DECLARATION SUMMARY
    A2BDeclarationSummary.submitDeclarationSummary()

    // VERIFY JAM APP OVERVIEW PAGE DISPLAYS CORRECTLY WITH COMPLETED FUTURE PUPIL NUMBERS SUMMARY
    A2BYourApplication.declarationCompleteElementsVisible()

    // SUBMIT APPLICATION
    A2BYourApplication.submitApplication()

    // SUCCESS PAGE DISPLAYS CORRECTLY
    A2BSuccessfulApplicationSubmitted.applicationSubmittedSuccessfullyElementsVisible()
    })
})