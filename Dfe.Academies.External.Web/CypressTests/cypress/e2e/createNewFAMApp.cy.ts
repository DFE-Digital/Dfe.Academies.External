import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import A2BWhatAreYouApplyingToDo from '../page-objects/pages/A2BWhatAreYouApplyingToDo'
import A2BWhatIsYourRole from '../page-objects/pages/A2BWhatIsYourRole'
import A2BWhatIsTheNameOfTheSchool from '../page-objects/pages/A2BWhatIsTheNameOfTheSchool'
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
import footer from '../page-objects/components/footer'
import A2BFAMSchoolOverview from '../page-objects/pages/A2BFAMSchoolOverview'
import A2BFAMTrustname from '../page-objects/pages/A2BFAMTrustname'
import A2BFAMTrustOverview from '../page-objects/pages/A2BFAMTrustOverview'
import A2BFAMTrustOpeningDateSummary from '../page-objects/pages/A2BFAMTrustOpeningDateSummary'
import A2BFAMTrustOpeningDateDetails from '../page-objects/pages/A2BFAMTrustOpeningDateDetails'
import A2BFAMReasonsForFormingTrustSummary from '../page-objects/pages/A2BFAMReasonsForFormingTrustSummary'
import A2BFAMReasonsForFormingTrustDetails from '../page-objects/pages/A2BFAMReasonsForFormingTrustDetails'
import A2BFAMTrustPlansForGrowthSummary from '../page-objects/pages/A2BFAMPlansForGrowthSummary'
import A2BFAMTrustPlansForGrowthDetails from '../page-objects/pages/A2BFAMPlansForGrowthDetails'
import A2BFAMSchoolImprovementStrategySummary from '../page-objects/pages/A2BFAMSchoolImprovementStrategySummary'
import A2BFAMSchoolImprovementStrategyDetails from '../page-objects/pages/A2BFAMSchoolImprovementStrategyDetails.ts'
import A2BFAMGovernanceStructureSummary from '../page-objects/pages/A2BFAMGovernanceStructureSummary'
import A2BFAMGovernanceStructureDetails from '../page-objects/pages/A2BFAMGovernanceStructureDetails'
import A2BFAMKeyPeopleSummary from '../page-objects/pages/A2BFAMKeyPeopleSummary'
import A2BFAMKeyPersonDetails from '../page-objects/pages/A2BFAMKeyPersonDetails'

describe('Create a FAM application', () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    A2BHome.homePageElementsVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    A2BHome.clickStartNow()
  })

  it('should be able to create a new application', () => {
    A2BLogin.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'))

    A2BYourApplications.selectStartANewApplication()

    A2BWhatAreYouApplyingToDo.selectFAMRadioButtonVerifyAndSubmit()

    A2BWhatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit()

    A2BYourApplication.FAMApplicationNotStartedElementsVisible()

    A2BYourApplication.selectAddASchool()

    A2BWhatIsTheNameOfTheSchool.selectSchoolName()

    A2BYourApplication.FAMApplicationNotStartedSchoolAddedElementsVisible()

    A2BYourApplication.selectFAMSchool()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageNotStartedElementsVisible()

    A2BYourApplication.selectAboutTheConversion()

    A2BAboutTheConversion.selectContactDetailsStartSection()

    A2BMainContacts.fillMainContactDetailsAndSubmit()

    A2BConversionTargetDate.selectConversionTargetDateOptionNo()

    A2BConversionTargetDate.conversionTargetDateSubmit()

    A2BReasonsForJoining.reasonsForJoiningInputAndSubmit()

    A2BChangingTheNameOfTheSchool.changingTheNameOfTheSchoolSelectOptionNo()

    A2BChangingTheNameOfTheSchool.submitChangingTheNameOfTheSchool()

    A2BAboutTheConversion.aboutTheConversionCompleteElementsVisible()

    A2BAboutTheConversion.submitAboutTheConversion()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageAboutConversionCompleteElementsVisible()

    A2BYourApplication.selectFurtherInformation()

    A2BAdditionalDetailsSummaryPage.selectAdditionalDetailsStartSection()

    A2BAdditionalDetailsDetails.fillAdditionalDetailsDetailsAndSubmit()

    A2BAdditionalDetailsSummaryPage.additionalDetailsSummaryCompleteElementsVisible()

    A2BAdditionalDetailsSummaryPage.submitAdditionalDetailsSummary()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageFurtherInformationCompleteElementsVisible()

    A2BYourApplication.selectFinances()

    A2BFinanceSummary.selectPreviousFinancialYrStartSection()

    A2BPreviousFinancialYear.inputPreviousFinancialYrDataAndSubmit()

    A2BCurrentFinancialYear.inputCurrentFinancialYrDataAndSubmit()

    A2BNextFinancialYear.inputNextFinancialYrDataAndSubmit()

    A2BLoansSummary.selectLoansOptionNo()

    A2BLoansSummary.submitLoansSummary()

    A2BLeasesSummary.leasesSelectOptionNo()

    A2BLeasesSummary.submitLeasesSummary()

    A2BFinancialInvestigations.selectFinancialInvestigationsOptionNo()

    A2BFinancialInvestigations.submitFinancialInvestigations()

    A2BFinanceSummary.financeSummaryCompleteElementsVisible()

    A2BFinanceSummary.submitFinanceSummary()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageFinancesCompleteElementsVisible()

    A2BYourApplication.selectFuturePupilNumbers()

    A2BFuturePupilNumbersSummary.selectFuturePupilNumbersStartSection()

    A2BFuturePupilNumbersDetails.fillFuturePupilNumbersDetails()

    A2BFuturePupilNumbersDetails.submitFuturePupilNumbersDetails()

    A2BFuturePupilNumbersSummary.futurePupilNumbersSummaryCompleteElementsVisible()

    A2BFuturePupilNumbersSummary.submitFuturePupilNumbersSummary()

    A2BYourApplication.FAMApplicationFuturePupilNumbersSubmittedElementsVisible()

    A2BYourApplication.selectFAMSchool()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageFuturePupilNumbersCompleteElementsVisible()

    A2BYourApplication.selectLandAndBuildings()

    A2BLandAndBuildingsSummary.selectLandAndBuildingsStartSection()

    A2BLandAndBuildingsDetails.fillLandAndBuildingsDetailsDataAndSubmit()

    A2BLandAndBuildingsSummary.landAndBuildingsSummaryCompleteElementsVisible()

    A2BLandAndBuildingsSummary.submitLandAndBuildingsSummary()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageLandAndBuildingsCompleteElementsVisible()

    A2BYourApplication.selectConsultation()

    A2BConsultationSummary.selectConsultationStartSection()

    A2BConsultationDetails.selectHasGovBodyConsultedStakeholdersOptionNo()

    A2BConsultationDetails.fillConsultationDetails()

    A2BConsultationDetails.submitConsultationDetails()

    A2BConsultationSummary.consultationSummaryCompleteElementsVisible()

    A2BConsultationSummary.submitConsultationSummary()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageConsultationCompleteElementsVisible()

    A2BYourApplication.selectPreopeningSupportGrant()

    A2BPreOpeningSupportGrantSummary.selectPreopeningSupportGrantStartSection()

    A2BPreopeningSupportGrantDetails.FAMSelectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails()

    A2BPreOpeningSupportGrantSummary.preopeningSupportGrantSummaryCompleteElementsVisible()

    A2BPreOpeningSupportGrantSummary.submitPreopeningSupportGrantSummary()

    A2BFAMSchoolOverview.FAMSchoolOverviewPagePreopeningSupportGrantCompleteElementsVisible()

    A2BYourApplication.selectDeclaration()

    A2BDeclarationSummary.declarationStartSection()

    A2BDeclaration.selectAgreementsVerifyAndSubmit()

    A2BDeclarationSummary.declarationSummaryCompleteElementsVisible()

    A2BDeclarationSummary.submitDeclarationSummary()

    A2BFAMSchoolOverview.FAMSchoolOverviewPageDeclarationCompleteElementsVisible()

    A2BFAMSchoolOverview.selectSaveAndReturn()

    A2BYourApplication.FAMApplicationSchoolCompleteElementsVisible()

    A2BYourApplication.selectFAMAddTheTrust()

    A2BFAMTrustname.FAMEnterTrustnameAndSubmit()

    A2BYourApplication.FAMApplicationTrustNameComplete()

    A2BYourApplication.selectFAMTrustDetails()

    A2BFAMTrustOverview.FAMTrustOverviewTrustNameCompleteElementsVisible()

    A2BFAMTrustOverview.selectOpeningDate()

    A2BFAMTrustOpeningDateSummary.selectStartSection()

    A2BFAMTrustOpeningDateDetails.selectDayAndInput()

    A2BFAMTrustOpeningDateDetails.selectMonthAndInput()

    A2BFAMTrustOpeningDateDetails.selectYearAndInput()

    A2BFAMTrustOpeningDateDetails.FAMTrustOpeningDateInputApproverDetailsAndSubmit()

    A2BFAMTrustOpeningDateSummary.FAMOpeningDateSummaryCompleteElementsVisibleAndSubmit()

    A2BFAMTrustOverview.FAMTrustOverviewOpeningDateCompleteElementsVisible()

    A2BFAMTrustOverview.selectReasonsForFormingTheTrust()

    A2BFAMReasonsForFormingTrustSummary.selectStartSection()

    A2BFAMReasonsForFormingTrustDetails.FAMFillReasonsForFormingTrustAndSubmit()

    A2BFAMReasonsForFormingTrustSummary.FAMReasonsForFormingTrustSummaryCompleteElementsVisibleAndSubmit()

    A2BFAMTrustOverview.FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible()

    A2BFAMTrustOverview.selectPlansForGrowth()

    A2BFAMTrustPlansForGrowthSummary.selectStartSection()

    A2BFAMTrustPlansForGrowthDetails.inputPlansForGrowthAndSubmit()

    A2BFAMTrustPlansForGrowthSummary.FAMPlansForGrowthSummaryCompleteElementsVisibleAndSubmit()

    A2BFAMTrustOverview.FAMTrustOverviewPlansForGrowthCompleteElementsVisible()

    A2BFAMTrustOverview.selectSchoolImprovementStrategy()

    A2BFAMSchoolImprovementStrategySummary.selectStartSection()

    A2BFAMSchoolImprovementStrategyDetails.fillSchoolImprovementStrategyAndSubmit()

    A2BFAMSchoolImprovementStrategySummary.schoolImprovementStrategyCompleteElementsVisibleAndSubmit()

    A2BFAMTrustOverview.FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible()

    A2BFAMTrustOverview.selectGovernanceStructure()

    A2BFAMGovernanceStructureSummary.selectStartSection()

    A2BFAMGovernanceStructureDetails.uploadFileAndSubmit()

    A2BFAMGovernanceStructureSummary.FAMGovernanceStructureSummaryCompleteElementsVisibleAndSubmit()

    A2BFAMTrustOverview.FAMTrustOverviewGovernanceStructureCompleteElementsVisible()

    A2BFAMTrustOverview.selectKeyPeople()

    A2BFAMKeyPeopleSummary.selectAddKeyPerson()

    A2BFAMKeyPersonDetails.fillKeyPersonDetailsAndSubmit()

    A2BFAMKeyPeopleSummary.FAMKeyPeopleSummaryCompleteElementsVisibleAndSubmit()

    A2BFAMTrustOverview.FAMTrustOverviewKeyPeopleCompleteElementsVisible()

    A2BFAMTrustOverview.selectReturnToYourApplication()

    A2BYourApplication.FAMApplicationOverviewCompleteElementsVisible()

    A2BYourApplication.submitApplication()

    A2BSuccessfulApplicationSubmitted.applicationSubmittedSuccessfullyElementsVisible()
  })
})
