
import Header from "../page-objects/components/Header";
import CookieHeaderModal from "../page-objects/components/CookieHeaderModal";
import A2BHome from "../page-objects/pages/A2BHome";
import A2BLogin from "../page-objects/pages/A2BLogin";
import A2BYourApplications from "../page-objects/pages/A2BYourApplications";
import A2BWhatAreYouApplyingToDo from "../page-objects/pages/A2BWhatAreYouApplyingToDo";
import A2BWhatIsYourRole from "../page-objects/pages/A2BWhatIsYourRole";
import A2BYourApplication from "../page-objects/pages/A2BYourApplication";
import A2BConfirmApplicationDelete from "../page-objects/pages/A2BConfirmApplicationDelete";
import Footer from "../page-objects/components/Footer";

describe("Delete Application Tests", () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'));

    Header.govUkHeaderVisible();
    Header.applyToBecomeAnAcademyHeaderLinkVisible();

    A2BHome.homePageElementsVisible();

    Footer.checkFooterLinksVisible();

    CookieHeaderModal.clickAcceptAnalyticsCookies();
    A2BHome.clickStartNow();
  });

  it("should be able to DELETE a New JAM Application from MAIN APPLICATION OVERVIEW page", () => {
    A2BLogin.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'));

    A2BYourApplications.selectStartANewApplication();

    A2BWhatAreYouApplyingToDo.selectJAMRadioButtonVerifyAndSubmit();

    A2BWhatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit();

    A2BYourApplication.yourApplicationNotStartedElementsVisible();

    // CLICK CANCEL APPLICATION LINK
    A2BYourApplication.selectCancelApplication();

    //VERIFY CONFIRM DELETE APPLICATION PAGE DISPLAYS CORRECTLY
    A2BConfirmApplicationDelete.checkAppIDIsCorrectAndselectConfirmDelete()

    // VERIFY CONFIRMATION OF DELETION BANNER DISPLAYS ON YOUR APPLICATIONS PAGE
     A2BYourApplications.verifyApplicationDeleted();

  })
})