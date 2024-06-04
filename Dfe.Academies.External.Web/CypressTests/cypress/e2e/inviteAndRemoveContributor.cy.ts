import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import yourApplication from '../page-objects/pages/yourApplication'
import inviteContributor from '../page-objects/pages/inviteContributor'
import confirmInviteContributorDelete from '../page-objects/pages/confirmInviteContributorDelete'
import footer from '../page-objects/components/footer'

describe('Invite/remove contributor', () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()
  })

  it('should add and remove a contributor from an application', () => {
    login.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'))

    yourApplications.selectApplicationForInviteContributor()

    yourApplication.selectInviteContributorLink()

    inviteContributor.fillDetailsAndSubmit()
      .verifySuccessBannerAndContributorList()
      .selectRemoveContributorLink()

    confirmInviteContributorDelete.confirmRemoveContributor()

    inviteContributor.verifyContributorRemovedAndSuccessRemoved()
  })
})
