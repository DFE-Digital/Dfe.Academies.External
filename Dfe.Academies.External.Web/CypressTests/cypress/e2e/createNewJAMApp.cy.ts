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

    cy.executeAccessibilityTests()

    yourApplications.startNewApplication()

    cy.executeAccessibilityTests()

    whatAreYouApplyingToDo.startApplication('Join A MAT')

    cy.executeAccessibilityTests()

    whatIsYourRole.chooseRole('Governor')

    cy.executeAccessibilityTests()

    application.addTrust()

    cy.executeAccessibilityTests()

    whichTrustIsSchoolJoining.selectTrustName('Plym')

    cy.executeAccessibilityTests()

    application.addSchool()

    cy.executeAccessibilityTests()

    whatIsTheNameOfTheSchool.selectSchoolName('Plym')

    cy.executeAccessibilityTests()

    application.selectTrustDetails()

    cy.executeAccessibilityTests()

    trustDetailsSummary.startTrustDetails()

    cy.executeAccessibilityTests()

    trustConsent.uploadConsentDoc()

    cy.executeAccessibilityTests()

    changesToTheTrust.enterChangesToTheTrust()

    cy.executeAccessibilityTests()

    localGovernanceArrangements.addGovernanceArragements()

    cy.executeAccessibilityTests()

    trustDetailsSummary.saveAndReturnToApp()

    cy.executeAccessibilityTests()

    application.checkTrustSectionComplete()
      .startAboutTheConversion()

    cy.executeAccessibilityTests()

    aboutTheConversion.startContactDetails()

    cy.executeAccessibilityTests()

    mainContacts.enterMainContactDetails(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)

    conversionTargetDate.enterConversionTargetDate('No')

    cy.executeAccessibilityTests()

    reasonsForJoining.enterReasonsForJoining()

    cy.executeAccessibilityTests()

    changingTheNameOfTheSchool.enterChangingTheNameOfTheSchool('No')

    cy.executeAccessibilityTests()

    aboutTheConversion.checkAboutTheConversion(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)
      .saveAndReturnToApp()

    cy.executeAccessibilityTests()

    application.checkSectionComplete('About the conversion')
      .startFurtherInformation()
    
    cy.executeAccessibilityTests()

    additionalDetailsSummaryPage.startAdditionalDetails()

    cy.executeAccessibilityTests()

    additionalDetailsDetails.enterAdditionalDetails()

    cy.executeAccessibilityTests()

    additionalDetailsSummaryPage.checkAdditionalDetails()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSectionComplete('Further information')
      .startFinances()
    
    cy.executeAccessibilityTests()

    financeSummary.startPreviousFinancialYear()

    cy.executeAccessibilityTests()

    previousFinancialYear.enterPreviousFinancialYearDetails()

    cy.executeAccessibilityTests()

    currentFinancialYear.enterCurrentFinancialYearDetails()

    cy.executeAccessibilityTests()

    nextFinancialYear.enterNextFinancialYearDetails()

    cy.executeAccessibilityTests()

    loansSummary.enterLoansDetails()

    cy.executeAccessibilityTests()

    leasesSummary.enterLeasesDetails()

    cy.executeAccessibilityTests()

    financialInvestigations.enterFinancialInvestigationsDetails()

    cy.executeAccessibilityTests()

    financeSummary.checkFinanceSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSectionComplete('Finances')
      .startFuturePupilNumbers()
    
    cy.executeAccessibilityTests()

    futurePupilNumbersSummary.startFuturePupilNumbers()

    cy.executeAccessibilityTests()

    futurePupilNumbersDetails.enterFuturePupilNumbersDetails()

    cy.executeAccessibilityTests()

    futurePupilNumbersSummary.checkFuturePupilNumbersSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSectionComplete('Future pupil numbers')
      .startLandAndBuildings()
    
    cy.executeAccessibilityTests()

    landAndBuildingsSummary.startLandAndBuildings()

    cy.executeAccessibilityTests()

    landAndBuildingsDetails.enterLandAndBuildingsDetailsDetails()

    cy.executeAccessibilityTests()

    landAndBuildingsSummary.checkLandAndBuildingsSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSectionComplete('Land and buildings')
      .startConsultation()
    
    cy.executeAccessibilityTests()

    consultationSummary.startConsultation()

    cy.executeAccessibilityTests()

    consultationDetails.enterConsultationDetails()

    cy.executeAccessibilityTests()

    consultationSummary.checkConsultationSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSectionComplete('Consultation')

	  application.startDeclaration()
    
    cy.executeAccessibilityTests()

    declarationSummary.startDeclaration()

    cy.executeAccessibilityTests

    declaration.selectAgreements()

    cy.executeAccessibilityTests()

    declarationSummary.checkDeclarationSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSectionComplete('Declaration')
      .submitApplication()
    
    cy.executeAccessibilityTests()

    successfulApplicationSubmitted.checkApplicationSubmitted()
  })
})
