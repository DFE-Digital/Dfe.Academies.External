import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BInviteContributor from '../page-objects/pages/A2BInviteContributor'
import A2BConfirmInviteContributorDelete from '../page-objects/pages/A2BConfirmInviteContributorDelete'
import footer from '../page-objects/components/footer'

describe('Invite/remove contributor', () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    A2BHome.homePageElementsVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    A2BHome.clickStartNow()
  })

  it('should add and remove a contributor from an application', () => {
    A2BLogin.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'))

    A2BYourApplications.selectApplicationForInviteContributor()

    A2BYourApplication.selectInviteContributorLink()

    A2BInviteContributor.fillDetailsAndSubmit()

    A2BInviteContributor.verifySuccessBannerAndContributorList()

    A2BInviteContributor.selectRemoveContributorLink()

    A2BConfirmInviteContributorDelete.confirmRemoveContributor()

    A2BInviteContributor.verifyContributorRemovedAndSuccessRemoved()
  })
})
