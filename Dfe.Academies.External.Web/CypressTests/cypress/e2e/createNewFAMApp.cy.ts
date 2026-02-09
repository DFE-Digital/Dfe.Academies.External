import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import whatIsTheNameOfTheSchool from '../page-objects/pages/whatIsTheNameOfTheSchool'
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
import declarationSummary from '../page-objects/pages/declarationSummary'
import declaration from '../page-objects/pages/declaration'
import successfulApplicationSubmitted from '../page-objects/pages/successfulApplicationSubmitted'
import schoolOverview from '../page-objects/pages/fam/schoolOverview'
import trustname from '../page-objects/pages/fam/trustName'
import trustOverview from '../page-objects/pages/fam/trustOverview'
import trustOpeningDateSummary from '../page-objects/pages/fam/trustOpeningDateSummary'
import trustOpeningDateDetails from '../page-objects/pages/fam/trustOpeningDateDetails'
import reasonsForFormingTrustSummary from '../page-objects/pages/fam/reasonsForFormingTrustSummary'
import reasonsForFormingTrustDetails from '../page-objects/pages/fam/reasonsForFormingTrustDetails'
import trustPlansForGrowthSummary from '../page-objects/pages/fam/plansForGrowthSummary'
import trustPlansForGrowthDetails from '../page-objects/pages/fam/plansForGrowthDetails'
import schoolImprovementStrategySummary from '../page-objects/pages/fam/schoolImprovementStrategySummary'
import schoolImprovementStrategyDetails from '../page-objects/pages/fam/schoolImprovementStrategyDetails'
import governanceStructureSummary from '../page-objects/pages/fam/governanceStructureSummary'
import governanceStructureDetails from '../page-objects/pages/fam/governanceStructureDetails'
import keyPeopleSummary from '../page-objects/pages/fam/keyPeopleSummary'
import keyPersonDetails from '../page-objects/pages/fam/keyPersonDetails'
import { faker } from '@faker-js/faker'

describe('Create a FAM application', () => {
  const approverName = `${faker.person.firstName()} ${faker.person.lastName()}`
  const approverEmail = faker.internet.email()

  const headTeacherName = `${faker.person.firstName()} ${faker.person.lastName()}`
  const headTeacherEmail = faker.internet.email()

  const chairName = `${faker.person.firstName()} ${faker.person.lastName()}`
  const chairEmail = faker.internet.email()

  const trustOpeningYear = `${new Date().getFullYear() + 1}`

  const homeAssertions = {
    warningIcon: () => cy.get('span[class="govuk-warning-text__icon"]'),
    warningText: () => cy.get('strong[class="govuk-warning-text__text"]'),
  }

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.acceptAnalyticsCookies()

    homeAssertions.warningIcon().should('not.exist')

    homeAssertions.warningText().should('not.exist')

    home.start()

    login.login()
  })

  it('should be able to create a new application', () => {
  
    const applicationAssertionCheck = {
      applicationOverviewBanner: () => cy.get('.govuk-notification-banner')
      
    }

    const famSchoolOverviewAssertionCheck = {
      conversionSupportGrantLink: () => cy.contains('Conversion support grant')
    }
    
    cy.executeAccessibilityTests()

    yourApplications.startNewApplication()

    cy.executeAccessibilityTests()

    whatAreYouApplyingToDo.startApplication('Form A MAT')

    cy.executeAccessibilityTests()

    whatIsYourRole.chooseRole('Governor')

    cy.executeAccessibilityTests()

    applicationAssertionCheck.applicationOverviewBanner().should('not.exist')

    application.addSchool()

    cy.executeAccessibilityTests()

    whatIsTheNameOfTheSchool.selectSchoolName('Plymstock')
   
    cy.executeAccessibilityTests()

    application.checkSchoolAdded()
      .selectFAMSchool()
    
    cy.executeAccessibilityTests()

    famSchoolOverviewAssertionCheck.conversionSupportGrantLink().should('not.exist')

    application.startAboutTheConversion()

    cy.executeAccessibilityTests()

    aboutTheConversion.startContactDetails()

    cy.executeAccessibilityTests()

    mainContacts.enterMainContactDetails(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)

    cy.executeAccessibilityTests()

    conversionTargetDate.enterConversionTargetDate('No')

    cy.executeAccessibilityTests()

    reasonsForJoining.enterReasonsForJoining()

    cy.executeAccessibilityTests()

    changingTheNameOfTheSchool.enterChangingTheNameOfTheSchool('No')

    cy.executeAccessibilityTests()

    aboutTheConversion.checkAboutTheConversion(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    schoolOverview.checkSectionCompleted('About the conversion')

    application.startFurtherInformation()

    cy.executeAccessibilityTests()

    additionalDetailsSummaryPage.startAdditionalDetails()

    cy.executeAccessibilityTests()

    additionalDetailsDetails.enterAdditionalDetails()

    cy.executeAccessibilityTests()

    additionalDetailsSummaryPage.checkAdditionalDetails()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    schoolOverview.checkSectionCompleted('Further information')

    application.startFinances()

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

    schoolOverview.checkSectionCompleted('Finances')

    application.startFuturePupilNumbers()

    cy.executeAccessibilityTests()

    futurePupilNumbersSummary.startFuturePupilNumbers()

    cy.executeAccessibilityTests()

    futurePupilNumbersDetails.enterFuturePupilNumbersDetails()

    cy.executeAccessibilityTests()

    futurePupilNumbersSummary.checkFuturePupilNumbersSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    application.checkSchoolStatus()
      .selectFAMSchool()

    cy.executeAccessibilityTests()

    schoolOverview.checkSectionCompleted('Future pupil numbers')

    application.startLandAndBuildings()

    cy.executeAccessibilityTests()

    landAndBuildingsSummary.startLandAndBuildings()

    cy.executeAccessibilityTests()

    landAndBuildingsDetails.enterLandAndBuildingsDetailsDetails()

    cy.executeAccessibilityTests()

    landAndBuildingsSummary.checkLandAndBuildingsSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    schoolOverview.checkSectionCompleted('Land and buildings')

    application.startConsultation()

    cy.executeAccessibilityTests()

    consultationSummary.startConsultation()

    cy.executeAccessibilityTests()

    consultationDetails.enterConsultationDetails()

    cy.executeAccessibilityTests()

    consultationSummary.checkConsultationSummaryCompleted()
      .saveAndReturnToApp()
    
    cy.executeAccessibilityTests()

    schoolOverview.checkSectionCompleted('Consultation')

    application.startDeclaration()

    cy.executeAccessibilityTests()

    declarationSummary.startDeclaration()

    cy.executeAccessibilityTests()

    declaration.selectAgreements()

    cy.executeAccessibilityTests()

    declarationSummary.checkDeclarationSummaryCompleted()
      .saveAndReturnToApp()

    cy.executeAccessibilityTests()

    schoolOverview.checkSectionCompleted('Declaration')
      .saveAndReturn()

    cy.executeAccessibilityTests()

    application.checkSchoolCompleted()
      .addFAMTrust()
    
    cy.executeAccessibilityTests()

    trustname.enterTrustName()

    cy.executeAccessibilityTests()

    application.checkTrustAdded()
      .selectFAMTrust()

    cy.executeAccessibilityTests()

    trustOverview.checkSectionCompleted('Trust name')
      .selectOpeningDate()

    cy.executeAccessibilityTests()

    trustOpeningDateSummary.selectStartSection()

    cy.executeAccessibilityTests()

    trustOpeningDateDetails.enterOpeningDateDetails(trustOpeningYear, approverName, approverEmail)

    cy.executeAccessibilityTests()

    trustOpeningDateSummary.checkOpeningDateSummaryCompleted(trustOpeningYear, approverName, approverEmail)

    trustOverview.checkSectionCompleted('Opening date')
      .selectReasonsForFormingTheTrust()
    
    cy.executeAccessibilityTests()

    reasonsForFormingTrustSummary.selectStartSection()

    cy.executeAccessibilityTests()

    reasonsForFormingTrustDetails.enterReasonsForFormingTrust()

    cy.executeAccessibilityTests()

    reasonsForFormingTrustSummary.checkReasonsForFormingTrustSummaryCompleted()

    trustOverview.checkSectionCompleted('Reasons for forming the trust')
      .selectPlansForGrowth()

    cy.executeAccessibilityTests()

    trustPlansForGrowthSummary.selectStartSection()

    cy.executeAccessibilityTests()

    trustPlansForGrowthDetails.inputPlansForGrowthAndSubmit()

    cy.executeAccessibilityTests()

    trustPlansForGrowthSummary.checkPlansForGrowthSummaryCompleted()

    cy.executeAccessibilityTests()

    trustOverview.checkSectionCompleted('Plans for growth')

    cy.executeAccessibilityTests()

    trustOverview.selectSchoolImprovementStrategy()

    cy.executeAccessibilityTests()

    schoolImprovementStrategySummary.selectStartSection()

    cy.executeAccessibilityTests()

    schoolImprovementStrategyDetails.fillSchoolImprovementStrategyAndSubmit()

    cy.executeAccessibilityTests()

    schoolImprovementStrategySummary.schoolImprovementStrategyCompleteElementsVisibleAndSubmit()

    cy.executeAccessibilityTests()

    trustOverview.checkSectionCompleted('School improvement strategy')

    trustOverview.selectGovernanceStructure()

    cy.executeAccessibilityTests()

    governanceStructureSummary.selectStartSection()

    cy.executeAccessibilityTests()

    governanceStructureDetails.uploadFileAndSubmit()

    cy.executeAccessibilityTests()

    governanceStructureSummary.checkGovernanceStructureSummaryCompleted()

    trustOverview.checkSectionCompleted('Governance structure')

    cy.executeAccessibilityTests()

    trustOverview.selectKeyPeople()

    cy.executeAccessibilityTests()

    keyPeopleSummary.selectAddKeyPerson()

    cy.executeAccessibilityTests()

    keyPersonDetails.fillKeyPersonDetailsAndSubmit(approverName)

    cy.executeAccessibilityTests()

    keyPeopleSummary.checkKeyPeopleSummaryCompleted(approverName)

    trustOverview.checkSectionCompleted('Key people')
      .selectReturnToYourApplication()

    cy.executeAccessibilityTests()

    application.checkApplicationOverviewCompleted()
      .submitApplication()

    cy.executeAccessibilityTests()

    successfulApplicationSubmitted.checkApplicationSubmitted()
  })
})
