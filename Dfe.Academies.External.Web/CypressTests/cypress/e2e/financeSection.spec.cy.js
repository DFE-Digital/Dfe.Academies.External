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
import A2BPreviousFinancialYear from '../page-objects/pages/A2BPreviousFinancialYear'
import A2BCurrentFinancialYear from '../page-objects/pages/A2BCurrentFinancialYear'
import A2BNextFinancialYear from '../page-objects/pages/A2BNextFinancialYear'
import A2BLoansSummary from '../page-objects/pages/A2BLoansSummary'
import A2BLeasesSummary from '../page-objects/pages/A2BLeasesSummary'
import A2BFinancialInvestigations from '../page-objects/pages/A2BFinancialInvestigations'

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
/*
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
        //A2BPreviousFinancialYear.submitPreviousFinancialYr()

*/


     /*    
         // SELECT CURRENT FINANCIAL YR START SECTION
        A2BFinanceSummary.selectCurrentFinancialYrStartSection()

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
        //A2BCurrentFinancialYear.submitCurrentFinancialYr()
*/

 // SELECT NEXT FINANCIAL YR START SECTION
 A2BFinanceSummary.selectNextFinancialYrStartSection()

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
//A2BNextFinancialYear.submitNextFinancialYr()


/*
         // SELECT LOANS START SECTION
         A2BFinanceSummary.selectLoansStartSection()

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

          // SELECT LEASES START SECTION
          A2BFinanceSummary.selectLeasesStartSection()

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
         

    
       // SELECT FINANCIAL INVESTIGATIONS START SECTION
       A2BFinanceSummary.selectFinancialInvestigationsStartSection()

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
*/











      })
    })