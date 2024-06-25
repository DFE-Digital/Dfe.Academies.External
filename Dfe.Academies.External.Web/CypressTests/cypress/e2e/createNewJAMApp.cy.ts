import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import whatIsTheNameOfTheSchool from '../page-objects/pages/whatIsTheNameOfTheSchool'
import whichTrustIsSchoolJoining from '../page-objects/pages/whichTrustIsSchoolJoining'
import trustDetailsSummary from '../page-objects/pages/jam/trustDetailsSummary'
import trustConsent from '../page-objects/pages/jam/trustConsent'
import changesToTheTrust from '../page-objects/pages/changesToTheTrust'
import localGovernanceArrangements from '../page-objects/pages/localGovernanceArrangements'
import application from '../page-objects/pages/application'
import aboutTheConversion from '../page-objects/pages/aboutTheConversion'
import mainContacts from '../page-objects/pages/mainContacts'
import conversionTargetDate from '../page-objects/pages/conversionTargetDate'
import reasonsForJoining from '../page-objects/pages/reasonsForJoining'
import changingTheNameOfTheSchool from '../page-objects/pages/changingTheNameOfTheSchool'
import additionalDetailsSummaryPage from '../page-objects/pages/additionalDetailsSummaryPage'
import additionalDetailsDetails from '../page-objects/pages/additionalDetailsDetails'
import financeSummary from '../page-objects/pages/financeSummary'
import previousFinancialYear from '../page-objects/pages/previousFinancialYear'
import currentFinancialYear from '../page-objects/pages/currentFinancialYear'
import nextFinancialYear from '../page-objects/pages/nextFinancialYear'
import loansSummary from '../page-objects/pages/loansSummary'
import leasesSummary from '../page-objects/pages/leasesSummary'
import financialInvestigations from '../page-objects/pages/financialInvestigations'
import futurePupilNumbersSummary from '../page-objects/pages/futurePupilNumbersSummary'
import futurePupilNumbersDetails from '../page-objects/pages/futurePupilNumbersDetails'
import landAndBuildingsSummary from '../page-objects/pages/landAndBuildingsSummary'
import landAndBuildingsDetails from '../page-objects/pages/landAndBuildingsDetails'
import consultationSummary from '../page-objects/pages/consultationSummary'
import consultationDetails from '../page-objects/pages/consultationDetails'
import preOpeningSupportGrantSummary from '../page-objects/pages/preOpeningSupportGrantSummary'
import preopeningSupportGrantDetails from '../page-objects/pages/preOpeningSupportGrantDetails'
import declarationSummary from '../page-objects/pages/declarationSummary'
import declaration from '../page-objects/pages/declaration'
import successfulApplicationSubmitted from '../page-objects/pages/successfulApplicationSubmitted'
import { faker } from '@faker-js/faker'

describe('Create a JAM application', () => {
  const approverName = `${faker.person.firstName()} ${faker.person.lastName()}`
  const approverEmail = faker.internet.email()

  const headTeacherName = `${faker.person.firstName()} ${faker.person.lastName()}`
  const headTeacherEmail = faker.internet.email()

  const chairName = `${faker.person.firstName()} ${faker.person.lastName()}`
  const chairEmail = faker.internet.email()

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.acceptAnalyticsCookies()

    home.start()

    login.login()
  })

  it('should be able to create a new application', () => {
    yourApplications.startNewApplication()

    whatAreYouApplyingToDo.startApplication('Join A MAT')

    whatIsYourRole.chooseRole('Governor')

    application.addTrust()

    whichTrustIsSchoolJoining.selectConfirmAndSubmitTrust('Plym')

    application.addSchool()

    whatIsTheNameOfTheSchool.selectSchoolName('Plym')

    application.selectTrustDetails()

    trustDetailsSummary.startTrustDetails()

    trustConsent.uploadConsentDoc()

    changesToTheTrust.enterChangesToTheTrust()

    localGovernanceArrangements.addGovernanceArragements()

    trustDetailsSummary.saveAndReturnToApp()

    application.checkTrustSectionComplete()
      .startAboutTheConversion()

    aboutTheConversion.startContactDetails()

    mainContacts.enterMainContactDetails(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)

    conversionTargetDate.enterConversionTargetDate('No')

    reasonsForJoining.enterReasonsForJoining()

    changingTheNameOfTheSchool.enterChangingTheNameOfTheSchool('No')

    aboutTheConversion.checkAboutTheConversion(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)
      .saveAndReturnToApp()

    application.checkAboutConversionCompleted()
      .startFurtherInformation()

    additionalDetailsSummaryPage.startAdditionalDetails()

    additionalDetailsDetails.enterAdditionalDetails()

    additionalDetailsSummaryPage.checkAdditionalDetails()
      .saveAndReturnToApp()

    application.checkFurtherInformationCompleted()
      .startFinances()

    financeSummary.startPreviousFinancialYear()

    previousFinancialYear.enterPreviousFinancialYearDetails()

    currentFinancialYear.enterCurrentFinancialYearDetails()

    nextFinancialYear.enterNextFinancialYearDetails()

    loansSummary.enterLoansDetails()

    leasesSummary.enterLeasesDetails()

    financialInvestigations.enterFinancialInvestigationsDetails()

    financeSummary.checkFinanceSummaryCompleted()
      .saveAndReturnToApp()

    application.checkFinanceCompleted()
      .startFuturePupilNumbers()

    futurePupilNumbersSummary.startFuturePupilNumbers()

    futurePupilNumbersDetails.enterFuturePupilNumbersDetails()

    futurePupilNumbersSummary.checkFuturePupilNumbersSummaryCompleted()
      .saveAndReturnToApp()

    application.checkFuturePupilNumbersCompleted()
      .startLandAndBuildings()

    landAndBuildingsSummary.startLandAndBuildings()

    landAndBuildingsDetails.enterLandAndBuildingsDetailsDetails()

    landAndBuildingsSummary.checkLandAndBuildingsSummaryCompleted()
      .saveAndReturnToApp()

    application.checkLandAndBuildingsCompleted()
      .startConsultation()

    consultationSummary.startConsultation()

    consultationDetails.enterConsultationDetails()

    consultationSummary.checkConsultationSummaryCompleted()
      .saveAndReturnToApp()

    application.checkConsultationCompleted()
      .startPreopeningSupportGrant()

    preOpeningSupportGrantSummary.startPreopeningSupportGrant()

    preopeningSupportGrantDetails.enterPreopeningSupportGrantDetails()

    preOpeningSupportGrantSummary.checkPreopeningSupportGrantSummaryCompleted()
      .saveAndReturnToApp()

    application.checkPreopeningSupportGrantCompleted()
      .startDeclaration()

    declarationSummary.startDeclaration()

    declaration.selectAgreements()

    declarationSummary.checkDeclarationSummaryCompleted()
      .saveAndReturnToApp()

    application.checkDeclarationCompleted()
      .submitApplication()

    successfulApplicationSubmitted.checkApplicationSubmitted()
  })
})
