
import Header from "../page-objects/components/Header";
import CookieHeaderModal from "../page-objects/components/CookieHeaderModal";
import A2BHome from "../page-objects/pages/A2BHome";
import A2BLogin from "../page-objects/pages/A2BLogin";
import A2BYourApplications from "../page-objects/pages/A2BYourApplications";
import A2BYourApplication from "../page-objects/pages/A2BYourApplication";
import A2BInviteContributor from "../page-objects/pages/A2BInviteContributor";
import A2BConfirmInviteContributorDelete from "../page-objects/pages/A2BConfirmInviteContributorDelete";
import Footer from "../page-objects/components/Footer";

describe("Invite / Remove Contributor", () => {
    beforeEach(function () {
        cy.visit(Cypress.env('URL'));

        Header.govUkHeaderVisible();
        Header.applyToBecomeAnAcademyHeaderLinkVisible();

        A2BHome.homePageElementsVisible();

        Footer.checkFooterLinksVisible();

        CookieHeaderModal.clickAcceptAnalyticsCookies();
        A2BHome.clickStartNow();
  });

  it("should add and remove a contributor to a JAM application", () => {
        
        A2BLogin.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'));

        A2BYourApplications.selectApplicationForInviteContributor()

        A2BYourApplication.selectInviteContributorLink()

        A2BInviteContributor.fillDetailsAndSubmit()

        A2BInviteContributor.verifySuccessBannerAndContributorList()

        A2BInviteContributor.selectRemoveContributorLink()

        A2BConfirmInviteContributorDelete.confirmRemoveContributor()

        A2BInviteContributor.verifyContributorRemovedAndSuccessRemoved()
  })
})


