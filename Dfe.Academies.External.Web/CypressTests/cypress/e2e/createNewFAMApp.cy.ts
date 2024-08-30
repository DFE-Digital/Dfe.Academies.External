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
import preOpeningSupportGrantSummary from '../page-objects/pages/preOpeningSupportGrantSummary'
import preopeningSupportGrantDetails from '../page-objects/pages/preOpeningSupportGrantDetails'
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

  beforeEach(function () {
    cy.visit(Cypress.env('url'))

    cookieHeaderModal.acceptAnalyticsCookies()

    home.start()

    login.login()
  })

  it('should be able to create a new application', () => {
    yourApplications.startNewApplication()

    whatAreYouApplyingToDo.startApplication('Form A MAT')

    whatIsYourRole.chooseRole('Governor')

    application.addSchool()

    whatIsTheNameOfTheSchool.selectSchoolName('Plym')

    application.checkSchoolAdded()
      .selectFAMSchool()

    application.startAboutTheConversion()

    aboutTheConversion.startContactDetails()

    mainContacts.enterMainContactDetails(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)

    conversionTargetDate.enterConversionTargetDate('No')

    reasonsForJoining.enterReasonsForJoining()

    changingTheNameOfTheSchool.enterChangingTheNameOfTheSchool('No')

    aboutTheConversion.checkAboutTheConversion(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)
      .saveAndReturnToApp()

    schoolOverview.checkSectionCompleted('About the conversion')

    application.startFurtherInformation()

    additionalDetailsSummaryPage.startAdditionalDetails()

    additionalDetailsDetails.enterAdditionalDetails()

    additionalDetailsSummaryPage.checkAdditionalDetails()
      .saveAndReturnToApp()

    schoolOverview.checkSectionCompleted('Further information')

    application.startFinances()

    financeSummary.startPreviousFinancialYear()

    previousFinancialYear.enterPreviousFinancialYearDetails()

    currentFinancialYear.enterCurrentFinancialYearDetails()

    nextFinancialYear.enterNextFinancialYearDetails()

    loansSummary.enterLoansDetails()

    leasesSummary.enterLeasesDetails()

    financialInvestigations.enterFinancialInvestigationsDetails()

    financeSummary.checkFinanceSummaryCompleted()
      .saveAndReturnToApp()

    schoolOverview.checkSectionCompleted('Finances')

    application.startFuturePupilNumbers()

    futurePupilNumbersSummary.startFuturePupilNumbers()

    futurePupilNumbersDetails.enterFuturePupilNumbersDetails()

    futurePupilNumbersSummary.checkFuturePupilNumbersSummaryCompleted()
      .saveAndReturnToApp()

    application.checkSchoolStatus()
      .selectFAMSchool()

    schoolOverview.checkSectionCompleted('Future pupil numbers')

    application.startLandAndBuildings()

    landAndBuildingsSummary.startLandAndBuildings()

    landAndBuildingsDetails.enterLandAndBuildingsDetailsDetails()

    landAndBuildingsSummary.checkLandAndBuildingsSummaryCompleted()
      .saveAndReturnToApp()

    schoolOverview.checkSectionCompleted('Land and buildings')

    application.startConsultation()

    consultationSummary.startConsultation()

    consultationDetails.enterConsultationDetails()

    consultationSummary.checkConsultationSummaryCompleted()
      .saveAndReturnToApp()

    schoolOverview.checkSectionCompleted('Consultation')

    application.startPreopeningSupportGrant()

    preOpeningSupportGrantSummary.startPreopeningSupportGrant()

    preopeningSupportGrantDetails.confirmPreopeningSupportGrantDetails()

    preOpeningSupportGrantSummary.checkPreopeningSupportGrantSummaryCompleted()
      .saveAndReturnToApp()

	schoolOverview.checkSectionCompleted('Conversion support grant')

    application.startDeclaration()

    declarationSummary.startDeclaration()

    declaration.selectAgreements()

    declarationSummary.checkDeclarationSummaryCompleted()
      .saveAndReturnToApp()

    schoolOverview.checkSectionCompleted('Declaration')
      .saveAndReturn()

    application.checkSchoolCompleted()
      .addFAMTrust()

    trustname.enterTrustName()

    application.checkTrustAdded()
      .selectFAMTrust()

    trustOverview.checkSectionCompleted('Trust name')
      .selectOpeningDate()

    trustOpeningDateSummary.selectStartSection()

    trustOpeningDateDetails.enterOpeningDateDetails(trustOpeningYear, approverName, approverEmail)

    trustOpeningDateSummary.checkOpeningDateSummaryCompleted(trustOpeningYear, approverName, approverEmail)

    trustOverview.checkSectionCompleted('Opening date')
      .selectReasonsForFormingTheTrust()

    reasonsForFormingTrustSummary.selectStartSection()

    reasonsForFormingTrustDetails.enterReasonsForFormingTrust()

    reasonsForFormingTrustSummary.checkReasonsForFormingTrustSummaryCompleted()

    trustOverview.checkSectionCompleted('Reasons for forming the trust')
      .selectPlansForGrowth()

    trustPlansForGrowthSummary.selectStartSection()

    trustPlansForGrowthDetails.inputPlansForGrowthAndSubmit()

    trustPlansForGrowthSummary.checkPlansForGrowthSummaryCompleted()

    trustOverview.checkSectionCompleted('Plans for growth')

    trustOverview.selectSchoolImprovementStrategy()

    schoolImprovementStrategySummary.selectStartSection()

    schoolImprovementStrategyDetails.fillSchoolImprovementStrategyAndSubmit()

    schoolImprovementStrategySummary.schoolImprovementStrategyCompleteElementsVisibleAndSubmit()

    trustOverview.checkSectionCompleted('School improvement strategy')

    trustOverview.selectGovernanceStructure()

    governanceStructureSummary.selectStartSection()

    governanceStructureDetails.uploadFileAndSubmit()

    governanceStructureSummary.checkGovernanceStructureSummaryCompleted()

    trustOverview.checkSectionCompleted('Governance structure')

    trustOverview.selectKeyPeople()

    keyPeopleSummary.selectAddKeyPerson()

    keyPersonDetails.fillKeyPersonDetailsAndSubmit(approverName)

    keyPeopleSummary.checkKeyPeopleSummaryCompleted(approverName)

    trustOverview.checkSectionCompleted('Key people')
      .selectReturnToYourApplication()

    application.checkApplicationOverviewCompleted()
      .submitApplication()

    successfulApplicationSubmitted.checkApplicationSubmitted()
  })
})
