import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import whatIsTheNameOfTheSchool from '../page-objects/pages/whatIsTheNameOfTheSchool'
import yourApplication from '../page-objects/pages/yourApplication'
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
import footer from '../page-objects/components/footer'
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
  const approverName = faker.person.fullName()
  const approverEmail = faker.internet.email()
  const trustOpeningYear = `${new Date().getFullYear() + 1}`

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()
  })

  it('should be able to create a new application', () => {
    login.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'))

    yourApplications.selectStartANewApplication()

    whatAreYouApplyingToDo.selectFAMRadioButtonVerifyAndSubmit()

    whatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit()

    yourApplication.FAMApplicationNotStartedElementsVisible()
      .selectAddASchool()

    whatIsTheNameOfTheSchool.selectSchoolName()

    yourApplication.FAMApplicationNotStartedSchoolAddedElementsVisible()
      .selectFAMSchool()

    schoolOverview.FAMSchoolOverviewPageNotStartedElementsVisible()

    yourApplication.selectAboutTheConversion()

    aboutTheConversion.selectContactDetailsStartSection()

    mainContacts.fillMainContactDetailsAndSubmit()

    conversionTargetDate.selectConversionTargetDateOptionNo()
      .conversionTargetDateSubmit()

    reasonsForJoining.reasonsForJoiningInputAndSubmit()

    changingTheNameOfTheSchool.changingTheNameOfTheSchoolSelectOptionNo()
      .submitChangingTheNameOfTheSchool()

    aboutTheConversion.aboutTheConversionCompleteElementsVisible()
      .submitAboutTheConversion()

    schoolOverview.FAMSchoolOverviewPageAboutConversionCompleteElementsVisible()

    yourApplication.selectFurtherInformation()

    additionalDetailsSummaryPage.selectAdditionalDetailsStartSection()

    additionalDetailsDetails.fillAdditionalDetailsDetailsAndSubmit()

    additionalDetailsSummaryPage.additionalDetailsSummaryCompleteElementsVisible()
      .submitAdditionalDetailsSummary()

    schoolOverview.FAMSchoolOverviewPageFurtherInformationCompleteElementsVisible()

    yourApplication.selectFinances()

    financeSummary.selectPreviousFinancialYrStartSection()

    previousFinancialYear.inputPreviousFinancialYrDataAndSubmit()

    currentFinancialYear.inputCurrentFinancialYrDataAndSubmit()

    nextFinancialYear.inputNextFinancialYrDataAndSubmit()

    loansSummary.selectLoansOptionNo()
      .submitLoansSummary()

    leasesSummary.leasesSelectOptionNo()
      .submitLeasesSummary()

    financialInvestigations.selectFinancialInvestigationsOptionNo()
      .submitFinancialInvestigations()

    financeSummary.financeSummaryCompleteElementsVisible()
      .submitFinanceSummary()

    schoolOverview.FAMSchoolOverviewPageFinancesCompleteElementsVisible()

    yourApplication.selectFuturePupilNumbers()

    futurePupilNumbersSummary.selectFuturePupilNumbersStartSection()

    futurePupilNumbersDetails.fillFuturePupilNumbersDetails()
      .submitFuturePupilNumbersDetails()

    futurePupilNumbersSummary.futurePupilNumbersSummaryCompleteElementsVisible()
      .submitFuturePupilNumbersSummary()

    yourApplication.FAMApplicationFuturePupilNumbersSubmittedElementsVisible()

    yourApplication.selectFAMSchool()

    schoolOverview.FAMSchoolOverviewPageFuturePupilNumbersCompleteElementsVisible()

    yourApplication.selectLandAndBuildings()

    landAndBuildingsSummary.selectLandAndBuildingsStartSection()

    landAndBuildingsDetails.fillLandAndBuildingsDetailsDataAndSubmit()

    landAndBuildingsSummary.landAndBuildingsSummaryCompleteElementsVisible()
      .submitLandAndBuildingsSummary()

    schoolOverview.FAMSchoolOverviewPageLandAndBuildingsCompleteElementsVisible()

    yourApplication.selectConsultation()

    consultationSummary.selectConsultationStartSection()

    consultationDetails.selectHasGovBodyConsultedStakeholdersOptionNo()
      .fillConsultationDetails()
      .submitConsultationDetails()

    consultationSummary.consultationSummaryCompleteElementsVisible()
      .submitConsultationSummary()

    schoolOverview.FAMSchoolOverviewPageConsultationCompleteElementsVisible()

    yourApplication.selectPreopeningSupportGrant()

    preOpeningSupportGrantSummary.selectPreopeningSupportGrantStartSection()

    preopeningSupportGrantDetails.FAMSelectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails()

    preOpeningSupportGrantSummary.preopeningSupportGrantSummaryCompleteElementsVisible()
      .submitPreopeningSupportGrantSummary()

    schoolOverview.FAMSchoolOverviewPagePreopeningSupportGrantCompleteElementsVisible()

    yourApplication.selectDeclaration()

    declarationSummary.declarationStartSection()

    declaration.selectAgreementsVerifyAndSubmit()

    declarationSummary.declarationSummaryCompleteElementsVisible()
      .submitDeclarationSummary()

    schoolOverview.FAMSchoolOverviewPageDeclarationCompleteElementsVisible()

    schoolOverview.selectSaveAndReturn()

    yourApplication.FAMApplicationSchoolCompleteElementsVisible()
      .selectFAMAddTheTrust()

    trustname.FAMEnterTrustnameAndSubmit()

    yourApplication.FAMApplicationTrustNameComplete()
      .selectFAMTrustDetails()

    trustOverview.FAMTrustOverviewTrustNameCompleteElementsVisible()
      .selectOpeningDate()

    trustOpeningDateSummary.selectStartSection()

    trustOpeningDateDetails.selectDayAndInput()
      .selectMonthAndInput()
      .selectYearAndInput(trustOpeningYear)
      .FAMTrustOpeningDateInputApproverDetailsAndSubmit(approverName, approverEmail)

    trustOpeningDateSummary.FAMOpeningDateSummaryCompleteElementsVisibleAndSubmit(trustOpeningYear, approverName, approverEmail)

    trustOverview.FAMTrustOverviewOpeningDateCompleteElementsVisible()
      .selectReasonsForFormingTheTrust()

    reasonsForFormingTrustSummary.selectStartSection()

    reasonsForFormingTrustDetails.FAMFillReasonsForFormingTrustAndSubmit()

    reasonsForFormingTrustSummary.FAMReasonsForFormingTrustSummaryCompleteElementsVisibleAndSubmit()

    trustOverview.FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible()
      .selectPlansForGrowth()

    trustPlansForGrowthSummary.selectStartSection()

    trustPlansForGrowthDetails.inputPlansForGrowthAndSubmit()

    trustPlansForGrowthSummary.FAMPlansForGrowthSummaryCompleteElementsVisibleAndSubmit()

    trustOverview.FAMTrustOverviewPlansForGrowthCompleteElementsVisible()

    trustOverview.selectSchoolImprovementStrategy()

    schoolImprovementStrategySummary.selectStartSection()

    schoolImprovementStrategyDetails.fillSchoolImprovementStrategyAndSubmit()

    schoolImprovementStrategySummary.schoolImprovementStrategyCompleteElementsVisibleAndSubmit()

    trustOverview.FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible()

    trustOverview.selectGovernanceStructure()

    governanceStructureSummary.selectStartSection()

    governanceStructureDetails.uploadFileAndSubmit()

    governanceStructureSummary.FAMGovernanceStructureSummaryCompleteElementsVisibleAndSubmit()

    trustOverview.FAMTrustOverviewGovernanceStructureCompleteElementsVisible()

    trustOverview.selectKeyPeople()

    keyPeopleSummary.selectAddKeyPerson()

    keyPersonDetails.fillKeyPersonDetailsAndSubmit(approverName)

    keyPeopleSummary.FAMKeyPeopleSummaryCompleteElementsVisibleAndSubmit(approverName)

    trustOverview.FAMTrustOverviewKeyPeopleCompleteElementsVisible()
      .selectReturnToYourApplication()

    yourApplication.FAMApplicationOverviewCompleteElementsVisible()
      .submitApplication()

    successfulApplicationSubmitted.applicationSubmittedSuccessfullyElementsVisible()
  })
})
