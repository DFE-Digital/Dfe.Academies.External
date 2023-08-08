
import Header from "../page-objects/components/Header";
import CookieHeaderModal from "../page-objects/components/CookieHeaderModal";
import A2BHome from "../page-objects/pages/A2BHome";
import A2BLogin from "../page-objects/pages/A2BLogin";
import A2BYourApplications from "../page-objects/pages/A2BYourApplications";
import A2BWhatAreYouApplyingToDo from "../page-objects/pages/A2BWhatAreYouApplyingToDo";
import A2BWhatIsYourRole from "../page-objects/pages/A2BWhatIsYourRole";
import A2BWhatIsTheNameOfTheSchool from "../page-objects/pages/A2BWhatIsTheNameOfTheSchool";
import A2BWhichTrustIsSchoolJoining from "../page-objects/pages/A2BWhichTrustIsSchoolJoining";
import A2BJAMTrustDetailsSummary from "../page-objects/pages/A2BJAMTrustDetailsSummary";
import A2BJAMTrustConsent from "../page-objects/pages/A2BJAMTrustConsent";
import A2BChangesToTheTrust from "../page-objects/pages/A2BChangesToTheTrust";
import A2BLocalGovernanceArrangements from "../page-objects/pages/A2BLocalGovernanceArrangements";
import A2BYourApplication from "../page-objects/pages/A2BYourApplication";
import A2BAboutTheConversion from "../page-objects/pages/A2BAboutTheConversion";
import A2BMainContacts from "../page-objects/pages/A2BMainContacts";
import A2BConversionTargetDate from "../page-objects/pages/A2BConversionTargetDate";
import A2BReasonsForJoining from "../page-objects/pages/A2BReasonsForJoining";
import A2BChangingTheNameOfTheSchool from "../page-objects/pages/A2BChangingTheNameOfTheSchool";
import A2BAdditionalDetailsSummaryPage from "../page-objects/pages/A2BAdditionalDetailsSummaryPage";
import A2BAdditionalDetailsDetails from "../page-objects/pages/A2BAdditionalDetailsDetails";
import A2BFinanceSummary from "../page-objects/pages/A2BFinanceSummary";
import A2BPreviousFinancialYear from "../page-objects/pages/A2BPreviousFinancialYear";
import A2BCurrentFinancialYear from "../page-objects/pages/A2BCurrentFinancialYear";
import A2BNextFinancialYear from "../page-objects/pages/A2BNextFinancialYear";
import A2BLoansSummary from "../page-objects/pages/A2BLoansSummary";
import A2BLeasesSummary from "../page-objects/pages/A2BLeasesSummary";
import A2BFinancialInvestigations from "../page-objects/pages/A2BFinancialInvestigations";
import A2BFuturePupilNumbersSummary from "../page-objects/pages/A2BFuturePupilNumbersSummary";
import A2BFuturePupilNumbersDetails from "../page-objects/pages/A2BFuturePupilNumbersDetails";
import A2BLandAndBuildingsSummary from "../page-objects/pages/A2BLandAndBuildingsSummary";
import A2BLandAndBuildingsDetails from "../page-objects/pages/A2BLandAndBuildingsDetails";
import A2BConsultationSummary from "../page-objects/pages/A2BConsultationSummary";
import A2BConsultationDetails from "../page-objects/pages/A2BConsultationDetails";
import A2BPreOpeningSupportGrantSummary from "../page-objects/pages/A2BPre-openingSupportGrantSummary";
import A2BPreopeningSupportGrantDetails from "../page-objects/pages/A2BPre-openingSupportGrantDetails";
import A2BDeclarationSummary from "../page-objects/pages/A2BDeclarationSummary";
import A2BDeclaration from "../page-objects/pages/A2BDeclaration";
import A2BSuccessfulApplicationSubmitted from "../page-objects/pages/A2BSuccessfulApplicationSubmitted";
import Footer from "../page-objects/components/Footer";

describe("View Application Tests", () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'));
    cy.injectAxe();
    cy.checkA11y();

    Header.govUkHeaderVisible();
    Header.applyToBecomeAnAcademyHeaderLinkVisible();

    A2BHome.homePageElementsVisible();

    Footer.checkFooterLinksVisible();

    CookieHeaderModal.clickAcceptAnalyticsCookies();
    A2BHome.clickStartNow();
  });

  it("should be able to create a New Application", () => {
    A2BLogin.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'));
    
    A2BYourApplications.selectStartANewApplication();

    A2BWhatAreYouApplyingToDo.selectJAMRadioButtonVerifyAndSubmit();

    A2BWhatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit();

    A2BYourApplication.yourApplicationNotStartedElementsVisible();

    A2BYourApplication.selectAddATrust();

    A2BWhichTrustIsSchoolJoining.selectConfirmAndSubmitTrust();

    A2BYourApplication.selectAddASchool();

    A2BWhatIsTheNameOfTheSchool.selectSchoolName();

    A2BYourApplication.selectTrustDetails();

    A2BJAMTrustDetailsSummary.JAMTrustDetailsSummarySelectStartSection();

    A2BJAMTrustConsent.JAMTrustConsentFileUploadAndSubmit();

    A2BChangesToTheTrust.changesToTheTrustClickYesEnterChangesAndSubmit();

    A2BLocalGovernanceArrangements.localGovernanceArrangementsClickYes();

    A2BLocalGovernanceArrangements.enterlocalGovernanceArrangementsChanges();

    A2BLocalGovernanceArrangements.localGovernanceArrangementsSubmit();

    A2BJAMTrustDetailsSummary.JAMTrustDetailsSummarySaveAndReturnToApp();

    A2BYourApplication.yourApplicationNotStartedButTrustSectionCompleteElementsVisible();

    A2BYourApplication.selectAboutTheConversion();

    A2BAboutTheConversion.selectContactDetailsStartSection();

    A2BMainContacts.fillMainContactDetailsAndSubmit();

    A2BConversionTargetDate.selectConversionTargetDateOptionNo();

    A2BConversionTargetDate.conversionTargetDateSubmit();

    A2BReasonsForJoining.reasonsForJoiningInputAndSubmit();

    A2BChangingTheNameOfTheSchool.changingTheNameOfTheSchoolSelectOptionNo();

    A2BChangingTheNameOfTheSchool.submitChangingTheNameOfTheSchool();

    A2BAboutTheConversion.aboutTheConversionCompleteElementsVisible();

    A2BAboutTheConversion.submitAboutTheConversion();

    A2BYourApplication.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible();

    A2BYourApplication.selectFurtherInformation();

    A2BAdditionalDetailsSummaryPage.selectAdditionalDetailsStartSection();

    A2BAdditionalDetailsDetails.fillAdditionalDetailsDetailsAndSubmit();

    A2BAdditionalDetailsSummaryPage.additionalDetailsSummaryCompleteElementsVisible();

    A2BAdditionalDetailsSummaryPage.submitAdditionalDetailsSummary();

    A2BYourApplication.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible();

    A2BYourApplication.selectFinances();

    A2BFinanceSummary.selectPreviousFinancialYrStartSection();

    A2BPreviousFinancialYear.inputPreviousFinancialYrDataAndSubmit();

    A2BCurrentFinancialYear.inputCurrentFinancialYrDataAndSubmit();

    A2BNextFinancialYear.inputNextFinancialYrDataAndSubmit();

    A2BLoansSummary.selectLoansOptionNo();

    A2BLoansSummary.submitLoansSummary();

    A2BLeasesSummary.leasesSelectOptionNo();

    A2BLeasesSummary.submitLeasesSummary();

    A2BFinancialInvestigations.selectFinancialInvestigationsOptionNo();

    A2BFinancialInvestigations.submitFinancialInvestigations();

    A2BFinanceSummary.financeSummaryCompleteElementsVisible();

    A2BFinanceSummary.submitFinanceSummary();

    A2BYourApplication.financeCompleteElementsVisible();

    A2BYourApplication.selectFuturePupilNumbers();

    A2BFuturePupilNumbersSummary.selectFuturePupilNumbersStartSection();

    A2BFuturePupilNumbersDetails.fillFuturePupilNumbersDetails();

    A2BFuturePupilNumbersDetails.submitFuturePupilNumbersDetails();

    A2BFuturePupilNumbersSummary.futurePupilNumbersSummaryCompleteElementsVisible();

    A2BFuturePupilNumbersSummary.submitFuturePupilNumbersSummary();

    A2BYourApplication.futurePupilNumbersCompleteElementsVisible();

    A2BYourApplication.selectLandAndBuildings();

    A2BLandAndBuildingsSummary.selectLandAndBuildingsStartSection();

    A2BLandAndBuildingsDetails.fillLandAndBuildingsDetailsDataAndSubmit();

    A2BLandAndBuildingsSummary.landAndBuildingsSummaryCompleteElementsVisible();

    A2BLandAndBuildingsSummary.submitLandAndBuildingsSummary();

    A2BYourApplication.landAndBuildingsCompleteElementsVisible();

    A2BYourApplication.selectConsultation();

    A2BConsultationSummary.selectConsultationStartSection();

    A2BConsultationDetails.selectHasGovBodyConsultedStakeholdersOptionNo();

    A2BConsultationDetails.fillConsultationDetails();

    A2BConsultationDetails.submitConsultationDetails();

    A2BConsultationSummary.consultationSummaryCompleteElementsVisible();

    A2BConsultationSummary.submitConsultationSummary();

    A2BYourApplication.consultationCompleteElementsVisible();

    A2BYourApplication.selectPreopeningSupportGrant();

    A2BPreOpeningSupportGrantSummary.selectPreopeningSupportGrantStartSection();

    A2BPreopeningSupportGrantDetails.selectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails();

    A2BPreOpeningSupportGrantSummary.preopeningSupportGrantSummaryCompleteElementsVisible();

    A2BPreOpeningSupportGrantSummary.submitPreopeningSupportGrantSummary();

    A2BYourApplication.preopeningSupportGrantCompleteElementsVisible();

    A2BYourApplication.selectDeclaration();

    A2BDeclarationSummary.declarationStartSection();

    A2BDeclaration.selectAgreementsVerifyAndSubmit();

    A2BDeclarationSummary.declarationSummaryCompleteElementsVisible();

    A2BDeclarationSummary.submitDeclarationSummary();

    A2BYourApplication.declarationCompleteElementsVisible();

    A2BYourApplication.submitApplication();

    A2BSuccessfulApplicationSubmitted.applicationSubmittedSuccessfullyElementsVisible();
  });
});
