import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import application from '../page-objects/pages/application'
import inviteContributor from '../page-objects/pages/inviteContributor'
import confirmInviteContributorDelete from '../page-objects/pages/confirmInviteContributorDelete'
import { faker } from '@faker-js/faker'

describe('Invite/remove contributor', () => {
  const contributorFirstName = faker.person.firstName()
  const contributorEmail = faker.internet.email()
  const applicationId = '10280'

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.acceptAnalyticsCookies()

    home.start()

    login.login()
  })

  it('should add and remove a contributor from an application', () => {
    yourApplications.selectApplication(applicationId)

    application.inviteContributor()

    inviteContributor.inviteContributor(contributorFirstName, contributorEmail)
      .hasSuccessBanner(contributorFirstName)
      .hasContributor(contributorFirstName)
      .selectRemoveContributorLink()

    confirmInviteContributorDelete.confirmRemoveContributor()

    inviteContributor.contributorRemoved(contributorFirstName)
  })
})
