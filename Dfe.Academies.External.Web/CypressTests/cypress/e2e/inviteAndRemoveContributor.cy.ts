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

  const home = {
    warningIcon: () => cy.get('span[class="govuk-warning-text__icon"]'),
    warningText: () => cy.get('strong[class="govuk-warning-text__text"]'),
    start: () => cy.get('[data-cy="startNowButton"]').click()
  }

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.acceptAnalyticsCookies()

    home.warningIcon().should('not.exist')

    home.warningText().should('not.exist')

    home.start()

    login.login()
  })

  it('should add and remove a contributor from an application', () => {

    cy.executeAccessibilityTests()

    yourApplications.selectApplication(applicationId)

    cy.executeAccessibilityTests()

    application.inviteContributor()

    cy.executeAccessibilityTests()

    inviteContributor.inviteContributor(contributorFirstName, contributorEmail)
      .hasSuccessBanner(contributorFirstName)
      .hasContributor(contributorFirstName)
      .selectRemoveContributorLink()

    cy.executeAccessibilityTests()

    confirmInviteContributorDelete.confirmRemoveContributor()

    cy.executeAccessibilityTests()

    inviteContributor.contributorRemoved(contributorFirstName)
  })
})
