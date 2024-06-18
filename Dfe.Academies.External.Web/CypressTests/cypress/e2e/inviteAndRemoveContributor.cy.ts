import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import yourApplication from '../page-objects/pages/yourApplication'
import inviteContributor from '../page-objects/pages/inviteContributor'
import confirmInviteContributorDelete from '../page-objects/pages/confirmInviteContributorDelete'
import footer from '../page-objects/components/footer'
import { faker } from '@faker-js/faker'

describe('Invite/remove contributor', () => {
  const contributorFirstName = faker.person.firstName()
  const contributorEmail = faker.internet.email()

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()
  })

  it('should add and remove a contributor from an application', () => {
    login.login()

    yourApplications.selectApplicationForInviteContributor()

    yourApplication.selectInviteContributorLink()

    inviteContributor.fillDetailsAndSubmit(contributorFirstName, contributorEmail)
      .verifySuccessBannerAndContributorList(contributorFirstName)
      .selectRemoveContributorLink()

    confirmInviteContributorDelete.confirmRemoveContributor()

    inviteContributor.verifyContributorRemovedAndSuccessRemoved(contributorFirstName)
  })
})
