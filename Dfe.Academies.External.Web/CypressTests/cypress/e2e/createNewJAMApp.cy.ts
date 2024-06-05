import header from '../page-objects/components/header'
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
import { faker } from '@faker-js/faker'

describe('Create a JAM application', () => {
  const approverName = faker.person.fullName()
  const approverEmail = faker.internet.email()

  const headTeacherName = faker.person.fullName()
  const headTeacherEmail = faker.internet.email()

  const chairName = faker.person.fullName()
  const chairEmail = faker.internet.email()

  const applicationId = '100080'

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()
  })

  it('should be able to create a new application', () => {
    login.login()

    yourApplications.selectStartANewApplication()

    whatAreYouApplyingToDo.selectJAMRadioButtonVerifyAndSubmit()

    whatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit()

    yourApplication.yourApplicationNotStartedElementsVisible()
      .selectAddATrust()

    whichTrustIsSchoolJoining.selectConfirmAndSubmitTrust('Plym')

    yourApplication.selectAddASchool()

    whatIsTheNameOfTheSchool.selectSchoolName('Plym')

    yourApplication.selectTrustDetails(applicationId)

    trustDetailsSummary.JAMTrustDetailsSummarySelectStartSection()

    trustConsent.JAMTrustConsentFileUploadAndSubmit()

    changesToTheTrust.changesToTheTrustClickYesEnterChangesAndSubmit()

    localGovernanceArrangements.localGovernanceArrangementsClickYes()
      .enterlocalGovernanceArrangementsChanges()
      .localGovernanceArrangementsSubmit()

    trustDetailsSummary.JAMTrustDetailsSummarySaveAndReturnToApp(applicationId)

    yourApplication.yourApplicationNotStartedButTrustSectionCompleteElementsVisible(applicationId)
      .selectAboutTheConversion()

    aboutTheConversion.selectContactDetailsStartSection()

    mainContacts.fillMainContactDetailsAndSubmit(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)

    conversionTargetDate.selectConversionTargetDateOptionNo()
      .conversionTargetDateSubmit()

    reasonsForJoining.reasonsForJoiningInputAndSubmit()

    changingTheNameOfTheSchool.changingTheNameOfTheSchoolSelectOptionNo()
      .submitChangingTheNameOfTheSchool()

    aboutTheConversion.aboutTheConversionCompleteElementsVisible(headTeacherName, headTeacherEmail, chairName, chairEmail, approverName, approverEmail)
      .submitAboutTheConversion()

    yourApplication.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible(applicationId)
      .selectFurtherInformation()

    additionalDetailsSummaryPage.selectAdditionalDetailsStartSection()

    additionalDetailsDetails.fillAdditionalDetailsDetailsAndSubmit()

    additionalDetailsSummaryPage.additionalDetailsSummaryCompleteElementsVisible()
      .submitAdditionalDetailsSummary()

    yourApplication.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible(applicationId)
      .selectFinances()

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

    yourApplication.financeCompleteElementsVisible(applicationId)
      .selectFuturePupilNumbers()

    futurePupilNumbersSummary.selectFuturePupilNumbersStartSection()

    futurePupilNumbersDetails.fillFuturePupilNumbersDetails()
      .submitFuturePupilNumbersDetails()

    futurePupilNumbersSummary.futurePupilNumbersSummaryCompleteElementsVisible()
      .submitFuturePupilNumbersSummary()

    yourApplication.futurePupilNumbersCompleteElementsVisible(applicationId)
      .selectLandAndBuildings()

    landAndBuildingsSummary.selectLandAndBuildingsStartSection()

    landAndBuildingsDetails.fillLandAndBuildingsDetailsDataAndSubmit()

    landAndBuildingsSummary.landAndBuildingsSummaryCompleteElementsVisible()
      .submitLandAndBuildingsSummary()

    yourApplication.landAndBuildingsCompleteElementsVisible(applicationId)
      .selectConsultation()

    consultationSummary.selectConsultationStartSection()

    consultationDetails.selectHasGovBodyConsultedStakeholdersOptionNo()
      .fillConsultationDetails()
      .submitConsultationDetails()

    consultationSummary.consultationSummaryCompleteElementsVisible()
      .submitConsultationSummary()

    yourApplication.consultationCompleteElementsVisible(applicationId)
      .selectPreopeningSupportGrant()

    preOpeningSupportGrantSummary.selectPreopeningSupportGrantStartSection()

    preopeningSupportGrantDetails.selectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails()

    preOpeningSupportGrantSummary.preopeningSupportGrantSummaryCompleteElementsVisible()
      .submitPreopeningSupportGrantSummary()

    yourApplication.preopeningSupportGrantCompleteElementsVisible(applicationId)
      .selectDeclaration()

    declarationSummary.declarationStartSection()

    declaration.selectAgreementsVerifyAndSubmit()

    declarationSummary.declarationSummaryCompleteElementsVisible()
      .submitDeclarationSummary()

    yourApplication.declarationCompleteElementsVisible(applicationId)
      .submitApplication()

    successfulApplicationSubmitted.applicationSubmittedSuccessfullyElementsVisible(applicationId)
  })
})
